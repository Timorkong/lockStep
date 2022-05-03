using System.Collections;
using System.Collections.Generic;
using Cmd.ID;
using UnityEngine;

public class RoomList : SingleWindow<RoomList>
{
    public GameObject RoomPrefab;

    public GameObject RoomParent;

    public override CMD ShowCMD =>  CMD.CMD_ROOM_LIST_RSP;

    public void Refresh(PROTOCOL_ROOM.CMD_ROOM_LIST_RSP rsp)
    {
        Util.DestroyAllChildren(RoomParent);

        foreach(var roomInf in rsp.room_list)
        {
            GameObject prefab = GameObject.Instantiate(RoomPrefab, RoomParent.transform);
        }
    }

    public override void Request()
    {
        base.Request();

        command_req.CMD_ROOM_LIST_REQ();
    }

    public void OnClickClose()
    {
        Hide();

        Login.Instance.Show();
    }
}
