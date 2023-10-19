using System.Collections.Generic;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class LobbyScene : BaseScene
    {
        private UI_LobbyScene _lobbySceneUI;

        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Lobby;

            _lobbySceneUI = Managers.UI.ShowSceneUI<UI_LobbyScene>();
        }

        public override void Clear()
        {

        }

        public void UpdateLobbyPlayers(List<LobbyPlayerInfo> lobbyPlayers)
        {
            _lobbySceneUI.UpdateLobbyPlayers(lobbyPlayers);
        }
    }
}