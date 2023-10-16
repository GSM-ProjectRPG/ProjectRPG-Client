using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace ProjectRPG
{
    public class UI_SelectServerPopup_Item : UI_Base
    {
        public ServerInfo Info { get; set; }

        private enum Buttons
        {
            SelectServerButton
        }

        private enum Texts
        {
            ServerNameText
        }

        public override void Init()
        {
            Bind<Button>(typeof(Buttons));
            Bind<TMP_Text>(typeof(Texts));

            GetButton((int)Buttons.SelectServerButton).gameObject.BindEvent(OnClickButton);
        }

        public void RefreshUI()
        {
            if (Info == null) return;

            GetText((int)Texts.ServerNameText).text = Info.Name;
        }

        private void OnClickButton(PointerEventData evt)
        {
            Managers.Network.Connect(Info);
            Managers.Scene.LoadScene(Define.Scene.Lobby);
            Managers.UI.ClosePopupUI();
        }
    }
}