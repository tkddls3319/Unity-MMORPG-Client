using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LobbySceneBag : UI_Scene
{
   public UI_UserInfo UserInfoUI { get; private set; }
   public UI_Chatting ChattingUI { get; private set; }
   public UI_GameRooms GameRoomsUI { get; private set; }
    public override void Init()
    {
        base.Init();

        UserInfoUI = GetComponentInChildren<UI_UserInfo>();
        ChattingUI = GetComponentInChildren<UI_Chatting>();
        GameRoomsUI = GetComponentInChildren<UI_GameRooms>();

    }
}
