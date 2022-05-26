using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLogic
{
	private static int _VALUE_1 = 1;
	private static int _VALUE_1000 = (1000 + 3212) << 1;
	public static int VALUE_1000
	{
		get
		{
			return (_VALUE_1000 >> _VALUE_1) - 3212;
		}
	}

	public static float Value_0
    {
        get { return 0.00001f; }
    }
}
