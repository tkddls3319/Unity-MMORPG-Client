using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UI_RoomCreate : UI_Popup
{

    enum Buttons
    {
        btn_Create,
        btn_Cancle
    }

    enum TextMeshProUGUIs
    {
        TitleText,
        UserCountText
    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        GetButton((int)Buttons.btn_Create).gameObject.BindEvent((data) =>
        {
            C_RoomCreate roomCreatePacket = new C_RoomCreate();
            roomCreatePacket.Name = GetTextMeshPro((int)TextMeshProUGUIs.TitleText).text.Replace("\u200B", string.Empty); ;
            roomCreatePacket.UserCount = int.Parse(GetTextMeshPro((int)TextMeshProUGUIs.UserCountText).text.Replace("\u200B", string.Empty));

            Managers.Network.Send(roomCreatePacket);
            Managers.UI.ClosePopupUI(this);

        });
        GetButton((int)Buttons.btn_Cancle).gameObject.BindEvent((data) =>
        {
            Managers.UI.ClosePopupUI(this);
        });
    }

}
