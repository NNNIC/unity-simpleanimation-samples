using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtil {

    /// <summary>
    /// 直下の子ノード(注：孫は含まず)のローカルTRSをリセット
    /// </summary>
    public static void ResetChildrenLocals(Transform t)
    {
        for(var i = 0; i<t.childCount; i++)
        {
            var c=t.GetChild(i);
            c.localPosition = Vector3.zero;
            c.localRotation = Quaternion.identity;
            c.localScale = Vector3.one;
        }
    }

    public static GameObject[] GetChildrenGameObject(Transform t)
    {
        var list = new List<GameObject>();
        for(var i = 0; i<t.childCount;i++)
        {
            list.Add(t.GetChild(i).gameObject);
        }
        return list.ToArray();
    }


}
