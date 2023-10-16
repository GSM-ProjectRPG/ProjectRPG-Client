using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Google.Protobuf.Protocol;

namespace ProjectRPG
{
    public class UI_CreateScene : UI_Scene
    {
        private CharacterCustomizer _customizer;

        private int _selectedBodyId = 1;
        private int _selectedFaceId = 1;
        private int _selectedAccessoryId = 1;

        private Material[] _bodyMaterials;
        private Material[] _faceMaterials;

        private enum GameObjects
        {
            PlayerNameField
        }

        private enum Images
        {
            ChangeBodyButton,
            ChangeFaceButton,
            ChangeAccessoryButton,
            CreateButton
        }

        private enum Texts
        {
            ChangeBodyButtonText,
            ChangeFaceButtonText,
            ChangeAccessoryButtonText
        }

        public override void Init()
        {
            base.Init();

            _bodyMaterials = Managers.Resource.LoadAll<Material>("Prefabs/Customize/Body");
            _faceMaterials = Managers.Resource.LoadAll<Material>("Prefabs/Customize/Face");

            var go = Managers.Resource.Instantiate("Customize/Character");
            _customizer = go.GetComponent<CharacterCustomizer>();
            UpdateCharacterModel();

            Bind<GameObject>(typeof(GameObjects));
            Bind<Image>(typeof(Images));
            Bind<TMP_Text>(typeof(Texts));

            GetImage((int)Images.ChangeBodyButton).gameObject.BindEvent(OnClickChangeBodyButton);
            GetImage((int)Images.ChangeFaceButton).gameObject.BindEvent(OnClickChangeFaceButton);
            GetImage((int)Images.ChangeAccessoryButton).gameObject.BindEvent(OnClickChangeAccessoryButton);
            GetImage((int)Images.CreateButton).gameObject.BindEvent(OnClickCreateButton);
        }

        public void OnClickChangeBodyButton(PointerEventData evt)
        {
            _selectedBodyId = (_selectedBodyId + 1) % _bodyMaterials.Length;
            GetText((int)Texts.ChangeBodyButtonText).text = $"Body {_selectedBodyId:00}";
            UpdateCharacterModel();
        }

        public void OnClickChangeFaceButton(PointerEventData evt)
        {
            _selectedFaceId = (_selectedFaceId + 1) % _faceMaterials.Length;
            GetText((int)Texts.ChangeFaceButtonText).text = $"Face {_selectedFaceId:00}";
            UpdateCharacterModel();
        }

        public void OnClickChangeAccessoryButton(PointerEventData evt)
        {
            UpdateCharacterModel();
        }

        public void OnClickCreateButton(PointerEventData evt)
        {
            var createPacket = new C_CreatePlayer()
            {
                Name = GetObject((int)GameObjects.PlayerNameField).GetComponent<TMP_InputField>().text,

            };

            Managers.Network.Send(createPacket);
        }

        public void UpdateCharacterModel()
        {
            _customizer.Customize(_bodyMaterials[_selectedBodyId], _faceMaterials[_selectedFaceId]);
        }
    }
}