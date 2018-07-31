using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public class ReadyForExport  {


    [MenuItem("Tools/MakeSpriteInfo")]
    static void MakeSpriteInfo()
    {
        var scene1file = "Assets/public/Testbed/UI/ui_work.unity";
        // load
        EditorSceneManager.OpenScene(scene1file);
        
        // template検索
        var tmplgo = HierarchyUtility.FindGameObjectByUncPath(null,"UI/template");
        Debug.Log("target :" +  HierarchyUtility.GetAbsoluteNodePath(tmplgo) );

        HierarchyUtility.TraverseComponent<Image>(tmplgo.transform,c=> {
            if (c.sprite == null)
            {
                Debug.Log(c.name); 
                return;
            }

            GameObject sprite_go = null;
            for(var i = 0; i < c.transform.childCount; i++)
            {
                var ct = c.transform.GetChild(i);
                if (ct.name.StartsWith("*sprite=" + c.name +":"))
                {
                    sprite_go = ct.gameObject;
                    break;
                }
            }
            if (sprite_go == null)
            {
                sprite_go = new GameObject();
            }
            sprite_go.name = "*sprite=" + c.name +":" + c.sprite.name;
            sprite_go.transform.parent = c.transform;
            sprite_go.transform.localPosition = Vector3.zero;
            sprite_go.transform.localRotation = Quaternion.identity;
            sprite_go.transform.localScale    = Vector3.one;
        });
    }
    [MenuItem("Tools/Create Prefab then Pack")]
    static void CreatePrefab()
    {

        var scene1file = "Assets/public/Testbed/UI/ui_work.unity";
        // load
        EditorSceneManager.OpenScene(scene1file);

        //念のため再度スプライト書き出し
        MakeSpriteInfo();

        // template検索
        var tmplgo = HierarchyUtility.FindGameObjectByUncPath(null,"UI/template");
        Debug.Log("target :" +  HierarchyUtility.GetAbsoluteNodePath(tmplgo) );

        var localpath = "Assets/Public/Testbed/UI/template.prefab";
        try { 
            if (AssetDatabase.LoadAssetAtPath(localpath,typeof(GameObject))!=null)
            {
                AssetDatabase.DeleteAsset(localpath);
            }
        } catch { }

        var prefab = PrefabUtility.CreateEmptyPrefab(localpath);
        var newprefab = PrefabUtility.ReplacePrefab(tmplgo,prefab);
        AssetDatabase.Refresh();

        var exportpath = Path.Combine(Application.dataPath,@"../template.unitypackage");

        AssetDatabase.ExportPackage(localpath,exportpath,ExportPackageOptions.Recurse);

        Debug.Log("Exported : " + exportpath);
    }

}
