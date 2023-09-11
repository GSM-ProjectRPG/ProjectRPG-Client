using System.Net;
using ACore;
using Google.Protobuf;

namespace ProjectRPG
{
    public class NetworkManager
    {
        public int AccountId { get; set; }
        public int Token { get; set; }

        private ServerSession _session = new ServerSession();

        public void Send(IMessage packet)
        {
            _session.Send(packet);
        }

        public void Connect(ServerInfo info)
        {
            var ipAddr = IPAddress.Parse(info.IpAddress);
            var endPoint = new IPEndPoint(ipAddr, info.Port);
            var connector = new Connector();
            connector.Connect(endPoint, () => _session, 1);
        }

        public void Update()
        {
            var list = PacketQueue.Instance.PopAll();
            foreach (var packet in list)
            {
                var handler = PacketManager.Instance.GetPacketHandler(packet.Id);
                handler?.Invoke(_session, packet.Message);
            }
        }
    }
}