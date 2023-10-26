using UnityEngine;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class MonsterController : NetworkObject
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

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            
        }
    }
}