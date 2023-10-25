using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Google.Protobuf.Protocol;
using ProjectRPG;

public class UI_GameScene : UI_Scene
{
    enum GameObjects
    {
        ChatLog,
        ChatTextInput
    }

    enum Images
    {
        SendBtn
    }
    
    public override void Init()
    {
        base.Init();
        
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        
        GetImage((int)Images.SendBtn).gameObject.BindEvent(SendMessage);
    }

    void SendMessage(PointerEventData evt)
    {
        var inputText = GetObject((int)GameObjects.ChatTextInput).gameObject.GetComponent<TMP_InputField>();
        C_Chat chat = new C_Chat()
        {
            Content = inputText.text
        };
        Managers.Network.Send(chat);
    }

    public void AddChat(string playerName, string chatText)
    {
        var chatLog = GetObject((int)GameObjects.ChatLog).gameObject;
        var newChatText = Managers.Resource.Instantiate("ChatText").GetComponent<TMP_Text>();
        newChatText.text = chatText;
        newChatText.transform.parent = chatLog.transform;
    }
}
