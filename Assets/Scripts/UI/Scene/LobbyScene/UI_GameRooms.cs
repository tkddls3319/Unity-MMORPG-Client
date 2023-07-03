using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameRooms : UI_Scene
{
    public List<UI_GameRoom> Items { get; } = new List<UI_GameRoom>();

    enum GameObjects
    {
        UI_Bag
    }
    enum Buttons
    {
        btnCreate,
    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GameObject go = Get<GameObject>((int)GameObjects.UI_Bag);
        foreach (Transform child in go.transform)
            Destroy(child.gameObject);

        Get<Button>((int)Buttons.btnCreate).gameObject.BindEvent((data) =>
        {
            //TODO : °ÔÀÓ·ë »ý¼º
            Managers.UI.ShowPopupUI<UI_RoomCreate>("Lobby");
        });


    }

    public void AddGameRoom(S_GameRoom packet)
    {
        try
        {
            GameObject go = Get<GameObject>((int)GameObjects.UI_Bag);
            for (int i = 0; i < packet.RoomInfo.Count; i++)
            {
                GameObject item = Managers.Resource.Instantiate("UI/Scene/Lobby/UI_GameRoom", go.transform);
                UI_GameRoom gameroomUI = item.GetOrAddComponent<UI_GameRoom>();
                gameroomUI.RoomId = packet.RoomInfo[i].RoomId;
                gameroomUI.Name= packet.RoomInfo[i].Name;
                gameroomUI.MaxUser= packet.RoomInfo[i].UserMax;
                gameroomUI.Master= packet.RoomInfo[i].RoomMaster;
                Items.Add(gameroomUI);
            }
        }
        catch (System.Exception err)
        {
            Debug.Log(err);
        }
        //RefreshUI();
    }

}
