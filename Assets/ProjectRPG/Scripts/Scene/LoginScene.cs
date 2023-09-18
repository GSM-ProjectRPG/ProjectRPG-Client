using UnityEngine;

namespace ProjectRPG
{
    public class LoginScene : BaseScene
    {
        private UI_LoginScene _loginSceneUI;

        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Login;

            Managers.Web.BaseUrl = "https://localhost:5001/api";

            Screen.SetResolution(1280, 720, false);

            _loginSceneUI = Managers.UI.ShowSceneUI<UI_LoginScene>();
        }

        public override void Clear()
        {

        }
    }
}