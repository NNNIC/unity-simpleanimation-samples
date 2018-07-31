using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil {

    public static string NormalizeSlash(string path) //Unityパス型式へ正規化　　バックスラッシュをスラッシュへ
    {
        return path.Replace('\\','/');
    }

}
