using UnityEngine;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class MonsterController : NetworkObject
    {
        private void Start()
        {
            
        }

        private void Update()
        {
            var pos = Vector3.Lerp(transform.position, ServerPos, 0.05f);
            transform.position = pos;
        }

        private void FixedUpdate()
        {
            
        }
    }
}