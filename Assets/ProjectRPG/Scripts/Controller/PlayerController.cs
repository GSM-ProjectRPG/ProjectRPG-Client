using UnityEngine;
using Google.Protobuf.Protocol;
//using static UnityEditor.PlayerSettings;

namespace ProjectRPG
{
    public class PlayerController : NetworkObject
    {
        private const float tickRate = 1f / 5f;
        private float _speed = 2f;

        private Vector3 _serverPos;
        public override Vector3 ServerPos
        {
            get => _serverPos;
            set
            {
                _localPrevDest = _serverPos + _localMoveVector;
                _serverPos = value;
                _localMoveVector = Vector3.zero;
                _serverTimer = 0;
            }
        }

        private Vector3 _inputVector;
        private Vector3 _localMoveVector;
        private Vector3 _localPrevDest;
        private float _serverTimer;

        private Rigidbody _rigidbody;
        private Animator _animator;
        private Camera _camera;

        private float _cameraRot = 0;
        private float _cameraDistance = 3;
        private float _cameraSensitivity = 1f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _camera = Camera.main;

            _rigidbody.position = ServerPos;
            _localPrevDest = ServerPos;
        }

        private void Update()
        {
            _serverTimer += Time.deltaTime;
            if (IsMine)
            {
                _camera.transform.position = transform.position + Quaternion.Euler(0, _cameraRot, 0) * new Vector3(0, 0.6f, -1) * _cameraDistance;
                _camera.transform.LookAt(transform.position + Vector3.up);

                if (Input.GetMouseButton(1))
                {
                    _cameraRot += Input.GetAxis("Mouse X") * _cameraSensitivity;
                }

                MoveInput();
            }
        }

        private void FixedUpdate()
        {
            if (IsMine)
            {
                Vector3 moveDelta = Quaternion.Euler(0, _cameraRot, 0) * (_speed * Time.fixedDeltaTime * _inputVector);
                SetMoveAnimation(moveDelta);
                _localMoveVector += moveDelta;
            }
            if (!IsMine)
            {
                Vector3 moveDelta = ServerPos - _rigidbody.position;
                SetMoveAnimation(moveDelta.magnitude < 0.05f ? Vector3.zero : moveDelta);
            }

            SyncPosition();
        }

        private void MoveInput()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            var vector = new Vector3(h, 0, v).normalized;

            if (_inputVector != vector)
            {
                _inputVector = vector;
                SendInputDirectionVector(Quaternion.Euler(0, _cameraRot, 0) * vector);
            }
        }

        private void SendInputDirectionVector(Vector3 vector)
        {
            var moveInputPacket = new C_Move() { InputVector = vector.ToVector() };
            Managers.Network.Send(moveInputPacket);
        }

        private void SyncPosition()
        {
            var movePos = _serverPos + _localMoveVector;
            movePos = Vector3.Lerp(_localPrevDest + _localMoveVector, movePos, _serverTimer / tickRate);
            if (!IsMine)
            {
                movePos = Vector3.Lerp(_rigidbody.position, movePos, Time.fixedDeltaTime * 10);
            }

            _rigidbody.MovePosition(movePos);
        }

        private void SetMoveAnimation(Vector3 delta)
        {
            if (delta == Vector3.zero)
            {
                _animator.SetBool("walking", false);
            }
            else
            {
                _animator.SetBool("walking", true);
                Quaternion targetRot = Quaternion.Euler(0, Mathf.Atan2(delta.z, -delta.x) * Mathf.Rad2Deg - 90, 0);
                _rigidbody.rotation = Quaternion.Lerp(targetRot, _rigidbody.rotation, 1 - Time.fixedDeltaTime * 10);
            }
        }
    }
}