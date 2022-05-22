using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingleWindow<InputManager>
{
    float last_x = 0;

    float last_y = 0;

    public Transform front = null;

    public void Update()
    {
        if (IsVisible == false) return;

        _updateJoystick();
    }

    private void _updateJoystick()
    {
        var mx = Input.GetAxisRaw("Horizontal");

        var my = Input.GetAxisRaw("Vertical");

        if (last_x != mx || last_y != my)
        {
            last_x = mx;

            last_y = my;

            command_req.CMD_WAR_MOVE_REQ(mx, my);
        }
    }
}
