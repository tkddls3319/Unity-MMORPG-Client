using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
        Managers.UI.ShowSceneUI<UI_LobbySceneBag>("Lobby");
        //_screenUI = Managers.UI.ShowSceneUI<UI_GameSceneBag>();
        //Managers.Map.LoadMap(1);

        C_Login loginPacket = new C_Login();
        Managers.Network.Send(loginPacket);
    }
    public override void Clear()
    {
    }

}
