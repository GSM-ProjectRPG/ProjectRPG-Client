using UnityEngine.UI;
using UnityEngine.EventSystems;
using Google.Protobuf.Protocol;
using System.Collections.Generic;

namespace ProjectRPG
{
    public class UI_LobbyScene : UI_Scene
    {
        private List<LobbyPlayerInfo> _lobbyPlayers = new List<LobbyPlayerInfo>();
        private int _seletedLobbyPlayer = 0;
        private CharacterCustomizer _customizer;

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

            var go = Managers.Resource.Instantiate("Customize/Character");
            _customizer = go.GetComponent<CharacterCustomizer>();
            UpdateCharacterModel();

            Bind<Image>(typeof(Images));

            GetImage((int)Images.NextButton).gameObject.BindEvent(OnClickNextButton);
            GetImage((int)Images.PrevButton).gameObject.BindEvent(OnClickPrevButton);
            GetImage((int)Images.NewPlayerButton).gameObject.BindEvent(OnClickNewPlayerButton);
            GetImage((int)Images.StartButton).gameObject.BindEvent(OnClickStartButton);
        }

        public void OnClickNextButton(PointerEventData evt)
        {
            _seletedLobbyPlayer = (_seletedLobbyPlayer + 1) % _lobbyPlayers.Count;
            UpdateCharacterModel();
        }

        public void OnClickPrevButton(PointerEventData evt)
        {
            _seletedLobbyPlayer = (_seletedLobbyPlayer - 1) < 0 ? _lobbyPlayers.Count - 1 : _seletedLobbyPlayer;
            UpdateCharacterModel();
        }

        public void OnClickNewPlayerButton(PointerEventData evt)
        {
            Managers.Scene.LoadScene(Define.Scene.Create);
        }

        public void OnClickStartButton(PointerEventData evt)
        {
            var enterGamePacket = new C_EnterGame() { Name = _lobbyPlayers[_seletedLobbyPlayer].Name };
            Managers.Network.Send(enterGamePacket);
        }
         
        public void UpdateLobbyPlayers(List<LobbyPlayerInfo> lobbyPlayers)
        {
            _lobbyPlayers = lobbyPlayers;
            UpdateCharacterModel();
        }

        public void UpdateCharacterModel()
        {
            if (_lobbyPlayers.Count > 0)
            {
                int customizeInfo = _lobbyPlayers[_seletedLobbyPlayer].CustomizeInfo;
                int bodyId = customizeInfo >> 8;
                int faceId = customizeInfo & 0x8F;
                _customizer.Customize(bodyId, faceId);
            }
        }
    }
}