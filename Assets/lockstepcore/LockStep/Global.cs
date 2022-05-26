using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    private static GlobalSetting setting = null;

    public const string PATH = "GlobleSetting";
 
    public static GlobalSetting Setting
    {
        get
        {
            if(setting == null)
            {
                setting = Resources.Load<GlobalSetting>(PATH);
            }

            return setting;
        }
    }
}
