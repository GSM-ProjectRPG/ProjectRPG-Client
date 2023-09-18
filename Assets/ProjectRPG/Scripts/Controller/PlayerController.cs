using UnityEngine;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class PlayerController : MonoBehaviour
    {
        public int Id { get; set; }
        public bool MyPlayer { get; set; }

        private TransformInfo _transform = new TransformInfo();
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
        public StatInfo Stat { get; set; }

        private void Awake()
        {
            _transform.Position = new Vector();
            _transform.Rotation = new Vector();
            _transform.Scale = new Vector();
            Stat = new StatInfo();
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}