using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class TimeUtil {

	public static void SetAction(MonoBehaviour mono, float waittime, Action func)
	{
		mono.StartCoroutine(_setaction_action(waittime,func));
	}
	private static IEnumerator _setaction_action(float waittime, Action func)
	{
		yield return new WaitForSeconds(waittime);
		func();
	}
	public static void SetAction_at_time(MonoBehaviour mono, float time, Action func)
	{
		var wait = time - Time.time;
		if (wait <=0)
		{
			func();
			return;
		}

		SetAction(mono,wait,func);
	}

}
