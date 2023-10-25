namespace ProjectRPG
{
    public class GameScene : BaseScene
    {
        private UI_GameScene _gameSceneUI;

        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Game;

            _gameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
        }

        public override void Clear()
        {

        }
    }
}