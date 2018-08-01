using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesUtil {

    /// <summary>
    /// リソースのプレハブからコンポーネントを取得する便利関数
    /// </summary>
    public static  T LoadComponent<T>(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        if (prefab==null)
        {
            return default(T);
        }

        return prefab.GetComponent<T>();
    }

    public static GameObject InstantiatePrefab(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        if (prefab==null) return null;
        var go = (GameObject)GameObject.Instantiate(prefab);
        return go;
    }

    // spriteのロードは、Resourcesからのみ。 Editorでは別 AssetDataBaseによる取得が可能。
    // ref https://answers.unity.com/questions/591677/how-to-get-child-sprites-from-a-multiple-sprite-te.html
    public static Sprite[] LoadAllSprites(string path)
    {
        return Resources.LoadAll<Sprite>(path);
    }
}
