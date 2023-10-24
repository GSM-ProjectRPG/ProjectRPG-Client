using UnityEngine;
using Google.Protobuf.Protocol;

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
        private Vector3 _inputVector;
        private float _speed = 2f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (IsMine)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                var vector = new Vector3(h, 0, v).normalized;

                if (_inputVector != vector)
                {
                    _inputVector = vector;
                    var moveInputPacket = new C_Move() { InputVector = vector.ToVector() };
                    Managers.Network.Send(moveInputPacket);
                }
            }
        }

        private void FixedUpdate()
        {
            transform.Translate(_speed * Time.fixedDeltaTime * _inputVector);

            if ((IsMine && !Input.anyKey) || !IsMine)
                Move();
        }

        private void Move()
        {
            var movePos = Vector3.MoveTowards(_rigidbody.position, MoveVector, 0.05f);
            _rigidbody.MovePosition(movePos);
        }
    }
}