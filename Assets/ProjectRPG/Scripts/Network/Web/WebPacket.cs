using System.Collections.Generic;

namespace ProjectRPG
{
    public class CreateAccountPacketReq
    {
        public string AccountName;
        public string Password;
    }

    public class CreateAccountPacketRes
    {
        public bool CreateOk;
    }

    public class LoginAccountPacketReq
    {
        public string AccountName;
        public string Password;
    }

    public class ServerInfo
    {
        public string Name;
        public string IpAddress;
        public int Port;
        public int BusyScore;
    }

    public class LoginAccountPacketRes
    {
        public bool LoginOk;
        public string AccountName;
        public int Token;
        public List<ServerInfo> ServerList = new List<ServerInfo>();
    }
}