using System.Collections;
using System.Collections.Generic;
using Cmd.ID;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : SingleWindow<RoomList>
{
    public GameObject RoomPrefab;

    public GameObject RoomParent;

    public override CMD ShowCMD =>  CMD.CMD_ROOM_LIST_RSP;

    protected override void Awake()
    {
        base.Awake();

        this.RoomPrefab.SetActive(false);
    }

    public void Refresh(List<PROTOCOL_COMMON.RoomInfo> room_list)
    {
        Util.DestroyAllChildren(RoomParent);

        foreach(var roomInfo in room_list)
        {
            GameObject prefab = GameObject.Instantiate(RoomPrefab, RoomParent.transform);

            prefab.SetActive(true);

            Text RoomName = prefab.transform.FindNode<Text>("RoomName/RoomName");

            RoomName.text = roomInfo.room_name;
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

    public void OnClickCreateRoom()
    {
        Hide();

        RoomInfo.Instance.Show();
    }
}
