using System.Linq;
using UnityEngine;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ProjectRPG;
using ACore;

public class PacketHandler
{
    public static void S_ConnectedToServerHandler(PacketSession session, IMessage packet)
    {
        Debug.Log("S_ConnectedToServerHandler");
        var loginPacket = new C_Login();
        string path = Application.dataPath;
        loginPacket.UniqueId = path.GetHashCode().ToString();
        Managers.Network.Send(loginPacket);
    }

    public static void S_LoginHandler(PacketSession session, IMessage packet)
    {
        var loginPacket = (S_Login)packet;
        if (loginPacket.LoginOk == 0) return;

        if (loginPacket.Players == null || loginPacket.Players.Count == 0)
        {
            Managers.Scene.LoadScene(Define.Scene.Create);
        }
        else
        {
            Managers.Scene.LoadScene(Define.Scene.Lobby, () =>
            {
                var lobbyScene = (LobbyScene)Managers.Scene.CurrentScene;
                lobbyScene.UpdateLobbyPlayers(loginPacket.Players.ToList());
            });
        }
    }

    public static void S_CreatePlayerHandler(PacketSession session, IMessage packet)
    {
        var createOkPacket = (S_CreatePlayer)packet;

        if (createOkPacket.Player == null)
        {
            Debug.Log("Create Failed");
        }
        else
        {
            Managers.Scene.LoadScene(Define.Scene.Game, () =>
            {
                var enterGamePacket = new C_EnterGame() { Name = createOkPacket.Player.Name };
                Managers.Network.Send(enterGamePacket);
            });
        }
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        var enterGamePacket = (S_EnterGame)packet;

        // TODO : 게임 입장 로직
        Managers.Object.Add(enterGamePacket.Player, myPlayer: true);
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        Managers.Object.Clear();
    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        var spawnPacket = (S_Spawn)packet;
        foreach (var info in spawnPacket.Objects)
            Managers.Object.Add(info, myPlayer: false);
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        var despawnPacket = (S_Despawn)packet;
        foreach (var id in despawnPacket.ObjectIds)
            Managers.Object.Remove(id);
    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_SkillHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_ChangeHpHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_DieHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_ItemListHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_AddItemHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_EquipItemHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_ChangeStatHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_PingHandler(PacketSession session, IMessage packet)
    {

    }
}