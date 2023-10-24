using UnityEngine;
using Google.Protobuf.Protocol;
using static UnityEditor.PlayerSettings;

namespace ProjectRPG
{
    public class PlayerController : NetworkObject
    {
        private TransformInfo _transform = new TransformInfo() { Position = new Vector(), Rotation = new Vector(), Scale = new Vector() };
        public TransformInfo Transform
        {
            get => _transform;
            set
            {
                if (_transform.Equals(value)) return;
                transform.position = value.Position.ToVector3();
                transform.localEulerAngles = value.Rotation.ToVector3();
                transform.localScale = value.Scale.ToVector3();
                _transform.MergeFrom(value);
            }
        }
        public StatInfo Stat { get; set; } = new StatInfo();


        public Vector3 MoveVector { get; set; }

        private Rigidbody _rigidbody;
        private Animator _animator;
        private Camera _camera;
        private float _speed = 2f; // 임시 이동속도

        private Vector3 _inputVector;
        public float _cameraRot = 0;
        public float _cameraDistance = 3;
        private float _cameraSensitivity = 1f; //카메라 감도

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _camera = Camera.main;
            MoveVector = Transform.Position.ToVector3();
        }

        private void Update()
        {
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
                OnMove(_rigidbody.position + Quaternion.Euler(0, _cameraRot, 0) * (_speed * Time.fixedDeltaTime * _inputVector));

            if (!IsMine || (IsMine && !Input.anyKey))
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
                SendPosition(Quaternion.Euler(0, _cameraRot, 0) * vector);
            }
        }

        private void SendPosition(Vector3 vector)
        {
            var moveInputPacket = new C_Move() { InputVector = vector.ToVector() };
            Managers.Network.Send(moveInputPacket);
        }
        
        private void SyncPosition()
        {
            var movePos = Vector3.MoveTowards(_rigidbody.position, MoveVector, 0.05f);
            OnMove(movePos);
        }

        private void OnMove(Vector3 pos)
        {
            Vector3 delta = pos - _rigidbody.position;
            _rigidbody.MovePosition(pos);

            if (delta == Vector3.zero)
            {
                _animator.SetBool("walking", false);
            }
            else
            {
                _animator.SetBool("walking", true);
                Quaternion targetRot = Quaternion.Euler(0, Mathf.Atan2(delta.z, -delta.x) * Mathf.Rad2Deg - 90, 0);
                _rigidbody.rotation = Quaternion.Lerp(targetRot, _rigidbody.rotation, Time.fixedDeltaTime * 25);
            }
        }
    }
}