using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Chatting : UI_Scene
{
    enum TextMeshProUGUIs
    {
        SendText,
    }
    enum Texts
    {
        ChatText,
        SendText,

    }
    enum Buttons
    {
        SendButton
    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        GetText((int)Texts.ChatText).text = string.Empty;

        GetButton((int)Buttons.SendButton).gameObject.BindEvent((data) =>
        {
            Text text = GetText((int)Texts.SendText);
            string sendText = text.text;
            text.text = null;

            C_Chat chat = new C_Chat();
            chat.Chat = sendText;
            Managers.Network.Send(chat);

        });
    }
    public void Write(S_Chat packet)
    {
        Text recvTxt = GetText((int)Texts.ChatText);
        recvTxt.text += $"\n{packet.PlayerId} - {packet.Chat}";
        
        GetText((int)Texts.SendText).text = "";
    }

}
