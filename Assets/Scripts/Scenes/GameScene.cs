using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        UI_GameSceneBag screenUI = Managers.UI.ShowSceneUI<UI_GameSceneBag>("Game");
        Managers.Map.LoadMap(1);


        C_EnterGame loginPacket = new C_EnterGame();
        loginPacket.RoomId = Managers.Map.MyRoom.RoomId;
        Managers.Network.Send(loginPacket);
        //Screen.SetResolution(640, 480, false);

        //GameObject myPlayer = Managers.Resource.Instantiate("Creature/MyPlayer");
        //Managers.Object.Add(myPlayer);    

        //for (int i = 1; i < 5; i++)
        //{
        //    GameObject player = Managers.Resource.Instantiate("Creature/Player");
        //    player.name = $"player{i}";

        //    Vector3Int pos = new Vector3Int() 
        //    {
        //        x = Random.Range(-20, 20),
        //        y = Random.Range(-10, 10),
        //    };

        //    player.GetComponent<CreatureController>().CellPos = pos;

        //    Managers.Object.Add(player);
        //}

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //gameObject.GetOrAddComponent<CursorController>();

        //GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        ////Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        //GameObject go = new GameObject { name = "SpawningPool" };
        //SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        //pool.SetKeepMonsterCount(2);
    }

    public override void Clear()
    {

    }
}
