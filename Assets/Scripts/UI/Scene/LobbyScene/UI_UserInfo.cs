using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UserInfo : UI_Scene
{
    public List<UI_FrienPanel> Items { get; } = new List<UI_FrienPanel>();
    enum TextMeshProUGUIs
    {
        SendText,
    }
    enum Texts
    {
        IdText,
    }
    enum GameObjects
    {
        Content
    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        GetText((int)Texts.IdText).text = string.Empty;

        GameObject go = Get<GameObject>((int)GameObjects.Content);
        foreach (Transform child in go.transform)
            Destroy(child.gameObject);

    }
    public void IdChange(S_Login packet)
    {
        GetText((int)Texts.IdText).text = packet.Player.Name;
    }

    public void AddFrienPanel(S_Spawn packet)
    {
        try
        {
            GameObject go = Get<GameObject>((int)GameObjects.Content);
            for (int i = 0; i < packet.Objects.Count; i++)
            {
                GameObject item = Managers.Resource.Instantiate("UI/Scene/Lobby/UI_FrienPanel", go.transform);
                //item.transform.position += new Vector3(0f, -70f, 0f);
                UI_FrienPanel friendUI = item.GetOrAddComponent<UI_FrienPanel>();
                friendUI.Name = packet.Objects[i].Name;
                friendUI.ID = packet.Objects[i].ObjectId;
                Items.Add(friendUI);
            }

        }
        catch (System.Exception err)
        {

            Debug.Log(err);
        }
        //RefreshUI();
    }

    public void RemoveFrienPanel(S_Despawn packet)
    {
        try
        {
            UI_FrienPanel friendUI = Items.Find(x=> x.ID == packet.ObjectIds[0]);
            Managers.Resource.Destroy(friendUI.gameObject);
            Items.Remove(friendUI);
        }
        catch (System.Exception err)
        {

            Debug.Log(err);
        }
        //RefreshUI();
    }
    public void RefreshUI()
    {
        if (Items.Count == 0)
            return;

        foreach (UI_FrienPanel item in Items)
        {
            item.RefreshUI();
        }
    }

}
