using System;
using System.Reflection;
using UnityEngine;

public class GameBindSystem : Singleton<GameBindSystem>
{
    public void BindMessgeHandle()
    {
        System.Type type = typeof(command_rsp);

        MethodInfo[] methodInfo = type.GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var method in methodInfo)
        {
            var attribute = method.GetCustomAttribute<MessageHandleAttribute>();

            if (attribute != null)
            {
                NetProcess.Instance.AddMsgHandle<MsgData>(attribute.msgId, new Action<MsgData>(data =>
                {
                    object[] param = new object[] { data };
                    method.Invoke(this, param);
                }));
            }
        }
    }
}
