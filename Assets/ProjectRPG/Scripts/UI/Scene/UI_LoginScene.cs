using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace ProjectRPG
{
    public class UI_LoginScene : UI_Scene
    {
        private enum GameObjects
        {
            AccountNameField,
            PasswordField,
        }

        private enum Images
        {
            CreateButton,
            LoginButton
        }

        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));
            Bind<Image>(typeof(Images));

            GetImage((int)Images.CreateButton).gameObject.BindEvent(OnClickCreateButton);
            GetImage((int)Images.LoginButton).gameObject.BindEvent(OnClickLoginButton);
        }

        public void OnClickCreateButton(PointerEventData evt)
        {
            string accountName = Get<GameObject>((int)GameObjects.AccountNameField).GetComponent<TMP_InputField>().text;
            string password = Get<GameObject>((int)GameObjects.PasswordField).GetComponent<TMP_InputField>().text;

            var req = new CreateAccountPacketReq()
            {
                AccountName = accountName,
                Password = password
            };

            Managers.Web.SendPostRequest<CreateAccountPacketRes>("account/create", req, (res) =>
            {
                Debug.Log(res.CreateOk);
                Get<GameObject>((int)GameObjects.AccountNameField).GetComponent<TMP_InputField>().text = "";
                Get<GameObject>((int)GameObjects.PasswordField).GetComponent<TMP_InputField>().text = "";
            });
        }

        public void OnClickLoginButton(PointerEventData evt)
        {
            string accountName = Get<GameObject>((int)GameObjects.AccountNameField).GetComponent<TMP_InputField>().text;
            string password = Get<GameObject>((int)GameObjects.PasswordField).GetComponent<TMP_InputField>().text;

            var req = new LoginAccountPacketReq()
            {
                AccountName = accountName,
                Password = password
            };

            Managers.Web.SendPostRequest<LoginAccountPacketRes>("account/login", req, (res) =>
            {
                Debug.Log(res.LoginOk);
                Get<GameObject>((int)GameObjects.AccountNameField).GetComponent<TMP_InputField>().text = "";
                Get<GameObject>((int)GameObjects.PasswordField).GetComponent<TMP_InputField>().text = "";

                if (res.LoginOk)
                {
                    Managers.Network.AccountName = res.AccountName;
                    Managers.Network.Token = res.Token;

                    var popup = Managers.UI.ShowPopupUI<UI_SelectServerPopup>();
                    popup.SetServerInfo(res.ServerList);
                }
            });
        }
    }
}