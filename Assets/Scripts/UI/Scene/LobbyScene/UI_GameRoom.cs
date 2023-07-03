using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameRoom : UI_Base
{

    public string Name { get; set; }
    public int RoomId { get; set; }
    public int MaxUser { get; set; }
    public ObjectInfo Master { get; set; }

    enum Texts
    {
        Title,
        UserCount
    }
    enum Buttons
    {
        Button
    }
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.Button).gameObject.BindEvent((data) =>
        {
            Managers.Map.MyRoom.RoomId = RoomId;
            Managers.Scene.LoadScene(Define.Scene.Game);
        });

        Setting();
    }

    public void Setting()
    {
        Text title = GetText((int)Texts.Title);
        title.text = $"{Master.Name}님의 방입니다.";
        Text maxUser = GetText((int)Texts.UserCount);
        maxUser.text = $"입장 정원 {MaxUser}";
    }
}
