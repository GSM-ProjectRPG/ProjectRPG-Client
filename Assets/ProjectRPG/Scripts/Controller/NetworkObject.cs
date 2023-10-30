using UnityEngine;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class NetworkObject : MonoBehaviour
    {
        public int Id { get; set; }
        public bool IsMine { get; set; }

        public virtual Vector3 ServerPos { get; set; }
        public virtual StatInfo Stat { get; set; } = new StatInfo();
    }
}