using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public class PacketHandler
{
    public static void S_LoginHandler(PacketSession session, IMessage packet)
    {
        S_Login loginPacket = (S_Login)packet;
        UI_LobbySceneBag lobbySeen = Managers.UI.SceneUI as UI_LobbySceneBag;
        lobbySeen.UserInfoUI.IdChange(loginPacket);
       //TODO
    }
    public static void S_GameRoomHandler(PacketSession session, IMessage packet)
    {
        S_GameRoom roomPacket = (S_GameRoom)packet;
        if (Managers.UI.SceneUI is UI_LobbySceneBag)
        {
            UI_LobbySceneBag lobbySeen = Managers.UI.SceneUI as UI_LobbySceneBag;
            lobbySeen.GameRoomsUI.AddGameRoom(roomPacket);
        }
    }

    public static void S_RoomCreateHandler(PacketSession session, IMessage packet)
    {
        S_RoomCreate roomPacket = (S_RoomCreate)packet;
        if (Managers.UI.SceneUI is UI_LobbySceneBag)
        {
            Managers.Map.MyRoom.RoomId = roomPacket.RoomId;
            Managers.Scene.LoadScene(Define.Scene.Game);

        }
    }

    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {
        S_Chat chatPacket = (S_Chat)packet;

        if(Managers.UI.SceneUI is UI_GameSceneBag)
        {
            UI_GameSceneBag sceneUI = Managers.UI.SceneUI as UI_GameSceneBag;
            sceneUI.MessageUI.Write(chatPacket);
        }
        else if (Managers.UI.SceneUI is UI_LobbySceneBag)
        {
            UI_LobbySceneBag sceneUI = Managers.UI.SceneUI as UI_LobbySceneBag;
            sceneUI.ChattingUI.Write(chatPacket);
        }
    }
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterPacket = (S_EnterGame)packet;

            Managers.Object.Add(enterPacket.Player, myPlayer: true);
    }
    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LeaveGame leavePacket = (S_LeaveGame)packet;
        Managers.Object.RemoveMyPlayer();

        //TODO : 서버는 완성됐음 클라에서 제거하는거 해줘야됨. 로비에서 친구창제거
    }
    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = (S_Spawn)packet;

        if (Managers.UI.SceneUI is UI_GameSceneBag)
        {
            foreach (ObjectInfo p in spawnPacket.Objects)
            {
                Managers.Object.Add(p, myPlayer: false);
            }
        }
        else if (Managers.UI.SceneUI is UI_LobbySceneBag)
        {
            UI_LobbySceneBag lobbySeen = Managers.UI.SceneUI as UI_LobbySceneBag;
            lobbySeen.UserInfoUI.AddFrienPanel(spawnPacket);
        }

    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn dspawnPacket = (S_Despawn)packet;

        if (Managers.UI.SceneUI is UI_GameSceneBag)
        {
            foreach (int id in dspawnPacket.ObjectIds)
            {
                Managers.Object.Remove(id);
            }
        }
        else if (Managers.UI.SceneUI is UI_LobbySceneBag)
        {
            UI_LobbySceneBag lobbySeen = Managers.UI.SceneUI as UI_LobbySceneBag;
            lobbySeen.UserInfoUI.RemoveFrienPanel(dspawnPacket);
        }
    }
    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = (S_Move)packet;

        GameObject go = Managers.Object.FindById(movePacket.ObjectId);
        if (go == null)
            return;

        //클라이언트가 먼저움직이고 서버에서 보내준 패킷으로 좌표를 덮어 쓰기때문에
        //가위눌리는 현상이 생길 수 있다. 
        //결국 내가 조정하는 플레이어는 나만 서버 응답 떄는 처리하지 않아야 한다.
        if (Managers.Object.MyPlayer.Id == movePacket.ObjectId)
            return;

        CreatureController cc = go.GetComponent<CreatureController>();
        if (cc == null)
            return;

        cc.PosInfo = movePacket.PosInfo;
    }
    public static void S_SkillHandler(PacketSession session, IMessage packet)
    {
        S_Skill skillPacket = (S_Skill)packet;

        GameObject go = Managers.Object.FindById(skillPacket.ObjectId);
        if (go == null)
            return;

        PlayerControllers pc = go.GetComponent<PlayerControllers>();
        if (pc != null)
        {
            pc.UseSkill(skillPacket.Info.SkillId);
        }
    }
    public static void S_ChangeHpHandler(PacketSession session, IMessage packet)
    {
        S_ChangeHp changHpPacket = (S_ChangeHp)packet;

        GameObject go = Managers.Object.FindById(changHpPacket.ObjectId);
        if (go == null)
            return;

        CreatureController cc = go.GetComponent<CreatureController>();
        if (cc != null)
        {
            cc.Stat.Hp = changHpPacket.Hp;
            //TODO : UI 변경
        }
    }



}
