using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class HierarchyUtility  {

    /// <summary>
    /// シーン全走査
    /// </summary>
    /// <param name="action"></param>
    public static void TraverseGameObject(Action<Transform> action) {TraverseGameObject(null,action);}
    public static void TraverseGameObject(Transform root, Action<Transform> action) //if 'root' is null, it will traverse from the top level.
    {
        var rootList = new List<Transform>();
        
        if (root==null)
        {
            foreach(GameObject o in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (o.transform.parent==null) rootList.Add(o.transform);
            }
        }
        else
        {
            rootList.Add(root);
        }

        Action<Transform> travchildren = null;
        travchildren = (t) =>  
        {
            action(t);
            if (t.childCount==0) return;
            for(int i=0;i<t.childCount;i++)
            {
                travchildren(t.GetChild(i));
            }
        };

        rootList.ForEach(travchildren);
    }

    public static GameObject FindGameObject(Transform root, string name, bool bIgnoreCase=false)
    {
        Transform find = null;
        TraverseGameObject(root,(t)=>{
            if (find!=null) return;
            if (bIgnoreCase)
            {
                if (t.name.ToLower() == name.ToLower())
                {
                    find = t;
                }
            }
            else
            {
                if (t.name == name)
                {
                    find = t;
                }
            }
        });

        return find!=null ? find.gameObject : null;
    }

    /// <summary>
    /// GameObject.Findの強化版
    /// オブジェクトが非アクティブでも検索可能。
    /// 要素の中に１か所ワイルドカード仕様可能 
    /// 
    /// 例）　Bip_*/Bip_* Pelvis/Bip_* Spine/Bip_* Spine1/Bip_* Neck/Bip_* Head
    /// </summary>
    public static GameObject FindGameObjectByUncPath(Transform root, string path, bool ignoreCase=false)
    {


        var plist = path.Split('/');
        Array.Reverse(plist); //使いやすいように逆転

        Transform find = null;
        TraverseGameObject(root, (t)=>{
            if (find!=null) return;
            if (CountDepth(t) < plist.Length) return;

            var u = t;
            for(int i = 0; i<plist.Length; i++)
            {
                if (u==null || !StringUtil.CheckWildcard(u.name, plist[i], ignoreCase)) return;
                u = u.parent;
            }
            find = t;
        });

        return find!=null ? find.gameObject : null;
    }

    public static int CountDepth(Transform t)
    {
        int i = 0;
        var u = t;
        while(u!=null)
        {
            i++;
            u = u.parent;
        }
        return i;
    }


    /// <summary>
    /// 親のゲームオブジェクトを再帰検索
    /// 検索方法はi_nameがgameobject.nameに含まれるかで大文字小文字も区別しない
    /// </summary>
    /// <param name="i_transform"></param>
    /// <param name="i_name"></param>
    /// <returns></returns>
    public static GameObject FindGameObjectParentRecursiveContains(Transform i_transform, string i_name)
    {
        if (i_transform.parent != null)
        {
            //string obj_name = i_transform.parent.name.ToLower();
            if (i_transform.parent.name.Contains(i_name))
                return i_transform.parent.gameObject;
            return FindGameObjectParentRecursiveContains(i_transform.parent.transform, i_name);
        }

        return null;
    }

    public static GameObject FindGameObjectContains(Transform root, string partofname)
    {
        Transform find = null;
        TraverseGameObject(root,(t)=>{
            if (find==null && t.name.Contains(partofname))
            {
                find = t;
            }
        });

        return find!=null ? find.gameObject : null;
    }

    public static GameObject[] CollectGameObjectContains(Transform root, string partofname)
    {
        List<GameObject> list = new List<GameObject>();
        TraverseGameObject(root,(t)=>{
            if (t.name.Contains(partofname))
            {
                list.Add(t.gameObject);
            }
        });

        return list.ToArray();
    }

    /// <summary>
    /// 指定のコンポーネントがアタッチされた全ゲームオブジェクトに対して指定の処理を行う
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static void TraverseComponent<T>(Transform root, System.Action<T> action) where T : Component
    {
        TraverseGameObject(root, (t) =>
        {
            T c = t.gameObject.GetComponent<T>();
            if (c != null)
                action(c);
        });
    }

    public static void TraverseSetLayerMask(Transform root, LayerMask layer)
    {
        TraverseGameObject(root,(t)=>{t.gameObject.layer = layer; });
    }

    public static void TraverseSetRenderSortOder(Transform root, int order)
    {
        TraverseGameObject(root,(t)=>{ if (t.GetComponent<Renderer>()!=null) t.GetComponent<Renderer>().sortingOrder = order; });
    }

    public static void TraverseSetShader(Transform root, Shader shader)
    {
        if (shader==null) return;
        TraverseGameObject(root, (t)=>
            {  
                if (t.GetComponent<Renderer>()!=null) 
                {
                    foreach(var mat in t.GetComponent<Renderer>().materials)
                    {
                        mat.shader = shader;  
                    } 
                }
            });
    }
    
    public static void TraverseSetShaderForSkinnedMesh(Transform root, Shader shader)
    {
        if (shader==null) return;
        TraverseGameObject(root, (t)=>
                           {  
            if (t.GetComponent<Renderer>()!=null) 
            {
                foreach(var mat in t.GetComponent<Renderer>().sharedMaterials)
                {
                    mat.shader = shader;  
                } 
            }
        });
    }

    public static void TraverseVisibility(Transform root, bool bVisible)
    {
        TraverseGameObject(root, (t) =>
        {
            Renderer r = t.gameObject.GetComponent<Renderer>();
            if (r != null)
                r.enabled = bVisible;
        });
    }

    public static void TraverseRenderer(Transform root, Action<Renderer> act) // NULLチェック済みのレンダラを引数にできる。
    {
       TraverseGameObject(root,(t) =>
           {
               if (t.GetComponent<Renderer>()!=null) act(t.GetComponent<Renderer>());
           });
    }
    public static void TraverseMaterial(Transform root, Action<Material> act) // 
    {
        TraverseRenderer(root,r=>{
            foreach(var m in r.materials)
            {
                act(m);
            }
        });
    }
    public static void TraverseSharedMaterial(Transform root, Action<Material> act) // 
    {
        TraverseRenderer(root,r=>{
            foreach(var m in r.sharedMaterials)
            {
                act(m);
            }
        });
    }

    /// <summary>
    /// 名前に指定文字列を含む、距離が最も近いゲームオブジェクトを探す
    /// </summary>
    public static GameObject FindNearestGameObjectContains(Transform root, Vector3 pos, string partofname)
    {
        Transform find = null;
        TraverseGameObject(root,(t)=>{
            if (!t.name.Contains(partofname)) return;
            if (find==null)
            {
                find = t;
            }
            else
            {
                if (  (find.position - pos).sqrMagnitude > (t.position - pos).sqrMagnitude )
                {
                    find = t;
                }
            }
        });

        return find!=null ? find.gameObject : null;
    }

    /// <summary>
    /// 指定トランスフォーム(=引数)から親トランスフォームを再帰的に上り
    /// 指定のコンポーネント(=T)をもっているか調べて
    /// 持っている場合は、そのコンポーネントを
    /// 持っていない場合は、Nullを戻す
    /// </summary>
    public static T FindInParents<T> (Transform trans) where T : Component
    {
        // 指定トランスフォームの指定コンポーネントを取得
        object comp = trans.GetComponent<T>();

        // 指定トランスフォームが指定コンポーネントを持っていない場合
        if (comp == null){
            // 親トランスフォームを取得
            Transform t = trans.parent;

            // 親があり且つ、親が指定コンポーネントを持っていない場合
            while (t != null && comp == null){
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
        }

        return (T)comp;
    }

    /// <summary>
    /// ゲームオブジェクトのノード名をUNC形式で返す
    /// </summary>
    public static string GetAbsoluteNodePath(GameObject go)
    {
        string s =null;
        Transform tr = go.transform;
        for(var loop = 0; loop < 200; loop++)
        {
            if (tr==null) break;
            if (s == null)
            {
                s = "/" + tr.name;
            }
            else
            {
                s = "/" +  tr.name + s;
            }

            tr = tr.parent;
        }

        return s;
    }

    /// <summary>
    /// コンポーネントの検索。なければ子オブジェクトもチェック
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="o"></param>
    /// <returns></returns>
    public static T FindComponentInChildren<T>(Transform root) where T : Component
    {
        for (int i = 0; i < root.childCount; i++)
        {
            T c = root.GetChild(i).gameObject.GetComponent<T>();
            if (c != null)
                return c;
        }

        return null;
    }
}
