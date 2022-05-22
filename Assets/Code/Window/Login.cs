using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : SingleWindow<Login>
{
    private void Start()
    {
        Show();
    }

    public void OnClickEnterRoom()
    {
        Hide();

        RoomList.Instance.Show();
    }
}
