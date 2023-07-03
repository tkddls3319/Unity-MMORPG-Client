using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public MyPlayerControllers MyPlayer { get; set; }
    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    public static GameObjectType GetObjectTypeById(int id)
    {
        int type = id >> 24 & 0x7F;
        return (GameObjectType)type;
    }

    public void Add(ObjectInfo info, bool myPlayer = false)
    {
        GameObjectType objectType = GetObjectTypeById(info.ObjectId);

        if (objectType == GameObjectType.Player)
        {
            if (myPlayer)
            {
                GameObject player = Managers.Resource.Instantiate("Creature/MyPlayer");
                player.name = info.Name;
                _objects.Add(info.ObjectId, player);

                MyPlayer = player.GetOrAddComponent<MyPlayerControllers>();
                MyPlayer.Id = info.ObjectId;
                MyPlayer.PosInfo = info.PosInfo;
                MyPlayer.Stat = info.StatInfo;
                MyPlayer.SyncPos();
            }
            else
            {
                GameObject player = Managers.Resource.Instantiate("Creature/Player");
                player.name = info.Name;
                _objects.Add(info.ObjectId, player);

                PlayerControllers pc = player.GetOrAddComponent<PlayerControllers>();
                pc.Id = info.ObjectId;
                pc.PosInfo = info.PosInfo;
                pc.Stat = info.StatInfo;
                MyPlayer.SyncPos();
            }
        }else if(objectType == GameObjectType.Projectile)
        {
            //GameObject go = Managers.Resource.Instantiate("Creature/Arrow");
            //go.name = "Arrow";
            //_objects.Add(info.ObjectId, go);
        }
    }
    public void Add(int id, GameObject go)
    {
        _objects.Add(id, go);
    }

    public void RemoveMyPlayer()
    {
        if (MyPlayer == null)
            return;

        Remove(MyPlayer.Id);
        MyPlayer = null;
    }

    public void Remove(int id)
    {
        GameObject go = FindById(id);
        if (go == null)
            return;

        _objects.Remove(id);
        Managers.Resource.Destroy(go);
    }
    public GameObject FindById(int id)
    {
        GameObject go = null;
        _objects.TryGetValue(id, out go);
        return go;
    }
    public GameObject Find(Vector3Int cellPos)
    {
        foreach (GameObject obj in _objects.Values)
        {
            CreatureController creature = obj.GetComponent<CreatureController>();

            if (creature == null)
                continue;
            if (creature.CellPos == cellPos)
                return obj;
        }

        return null;
    }
    public void Clear()
    {
        _objects.Clear();

        foreach (GameObject go in _objects.Values)
            Managers.Resource.Destroy(go);
    }
}
