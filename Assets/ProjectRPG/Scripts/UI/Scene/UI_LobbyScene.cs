using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class UI_LobbyScene : UI_Scene
    {
        private enum Images
        {
            NextButton,
            PrevButton,
            NewPlayerButton,
            StartButton
        }

        public override void Init()
        {
            base.Init();

            Bind<Image>(typeof(Images));

            GetImage((int)Images.NextButton).gameObject.BindEvent(OnClickNextButton);
            GetImage((int)Images.PrevButton).gameObject.BindEvent(OnClickPrevButton);
            GetImage((int)Images.NewPlayerButton).gameObject.BindEvent(OnClickNewPlayerButton);
            GetImage((int)Images.StartButton).gameObject.BindEvent(OnClickStartButton);
        }

        public void OnClickNextButton(PointerEventData evt)
        {

        }

        public void OnClickPrevButton(PointerEventData evt)
        {

        }

        public void OnClickNewPlayerButton(PointerEventData evt)
        {

        }

        public void OnClickStartButton(PointerEventData evt)
        {

        }
    }
}