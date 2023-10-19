namespace ProjectRPG
{
    public class CreateScene : BaseScene
    {
        private UI_CreateScene _createSceneUI;

        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Create;

            _createSceneUI = Managers.UI.ShowSceneUI<UI_CreateScene>();
        }

        public override void Clear()
        {

        }
    }
}