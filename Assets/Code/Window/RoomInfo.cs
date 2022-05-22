using System.Collections;
using System.Collections.Generic;
using Cmd.ID;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfo : SingleWindow<RoomInfo>
{
    public GameObject UserPrefab;

    public GameObject UserParent;

    public Text RoomName;

    public override CMD ShowCMD =>  CMD.CMD_CREATE_ROOM_RSP;

    protected override void Awake()
    {
        base.Awake();

        UserPrefab.SetActive(false);
    }

    public void Refresh(PROTOCOL_COMMON.RoomInfo room_info)
    {
        Util.DestroyAllChildren(UserParent);

        RoomName.text = room_info.room_name;

        foreach (var userInfo in room_info.user_list)
        {
            GameObject prefab = GameObject.Instantiate(UserPrefab, UserParent.transform);

            prefab.SetActive(true);

            Text UserName = prefab.transform.FindNode<Text>("UserName/UserName");

            UserName.text = userInfo.user_name;
        }
    }

    public override void Request()
    {
        base.Request();

        command_req.CMD_CREATE_ROOM_REQ();
    }

    public void OnClickClose()
    {
        Hide();

        command_req.CMD_LEAVE_ROOM_REQ();

        RoomList.Instance.Show();
    }

    public void OnClickEnterGame()
    {
        command_req.CMD_ENTER_GAME_REQ();

        Hide();

        Loading.Instance.Show();
    }
}
