using UnityEngine;

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
    }
}