using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum UIANCHORPOS {
    NONE,
    TL,TC,TR,
    ML,MC,MR,
    BL,BC,BR
};

public class UGuiUtil {

    // SetPos
    public static void SetPos(GameObject go, float x, float y,float z = 0)
    {
        var rt = go.GetComponent<RectTransform>();
        SetPos(rt,x,y,z);
    }
    public static void SetPos(RectTransform rt, float x, float y,float z = 0)
    {
        rt.anchoredPosition = new Vector2(x,y);
        rt.transform.localPosition = VectorUtil.Affect_Z(rt.transform.localPosition,z);
    }

    // SetSize
    public static void SetSize(GameObject go, float w, float h)
    {
        var rt = go.GetComponent<RectTransform>();
        SetSize(rt,w,h);
    }
    public static void SetSize(RectTransform rt, float w, float h)
    {
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }

    // SetAnchor
    public static void SetAnchor(GameObject go,string a)
    {
        var rt = go.GetComponent<RectTransform>();
        SetAnchor(rt,a);
    }
    public static void SetAnchor(RectTransform rt, string a)
    { 
        float x,y;
        if (!_get_anchorpos(a,out x, out y))
        {
            throw new SystemException("Unexpected! {B7D7873B-42D5-4EBC-9075-32343B6C8DD2}");
        }

        rt.anchorMax = new Vector2(x,y);
        rt.anchorMin = new Vector2(x,y);
    }
    public static void SetAnchor(GameObject go, UIANCHORPOS pos)
    {
        var rt = go.GetComponent<RectTransform>();
        SetAnchor(rt,pos);
    }
    public static void SetAnchor(RectTransform rt, UIANCHORPOS pos)
    {
        float x = 0;
        float y = 0;

        _get_anchorpos(pos,ref x, ref y);

        rt.anchorMax = new Vector2(x,y);
        rt.anchorMin = new Vector2(x,y);
    }
    // SetPivot
    public static void SetPivot(GameObject go,string a)
    {
        var rt = go.GetComponent<RectTransform>();
        SetPivot(rt,a);
    }
    public static void SetPivot(RectTransform rt, string a)
    {
        float x,y;
        if (!_get_anchorpos(a,out x, out y))
        {
            throw new SystemException("Unexpected! {34BB670C-6ED6-4D08-B63C-E9FC07E0FD77}");
        }

        rt.pivot = new Vector2(x,y);
    }
    public static void SetPivot(GameObject go, UIANCHORPOS pos)
    {
        var rt = go.GetComponent<RectTransform>();
        SetPivot(rt,pos);
    }
    public static void SetPivot(RectTransform rt, UIANCHORPOS pos)
    {


    }

    static bool _get_anchorpos(string anchorstr, out float x, out float y)
    {
        x = 0f;
        y = 0f;
        var pos_or_null = EnumUtil.TryParse(typeof(UIANCHORPOS),anchorstr);

        if(pos_or_null == null)
            return false;

        _get_anchorpos((UIANCHORPOS)pos_or_null,ref x,ref y);

        return true;
    }

    private static void _get_anchorpos(UIANCHORPOS pos, ref float x,ref float y)
    {
        if(pos == UIANCHORPOS.TL)
        { x = 0; y = 1; }
        if(pos == UIANCHORPOS.TC)
        { x = 0.5f; y = 1; }
        if(pos == UIANCHORPOS.TR)
        { x = 1; y = 1; }

        if(pos == UIANCHORPOS.ML)
        { x = 0; y = 0.5f; }
        if(pos == UIANCHORPOS.MC)
        { x = 0.5f; y = 0.5f; }
        if(pos == UIANCHORPOS.MR)
        { x = 1; y = 0.5f; }

        if(pos == UIANCHORPOS.BL)
        { x = 0; y = 0f; }
        if(pos == UIANCHORPOS.BC)
        { x = 0.5f; y = 0f; }
        if(pos == UIANCHORPOS.BR)
        { x = 1; y = 0f; }
    }

    // SetText
    public static void SetText(GameObject go, string s, string name=null)
    {
        var rt = go.GetComponent<RectTransform>();
        SetText(rt,s,name);
    }
    public static void SetText(RectTransform rt, string s, string name=null)
    {
        UnityEngine.UI.Text tc = null;
        HierarchyUtility.TraverseComponent<UnityEngine.UI.Text>(rt.transform, i=> {
            if (tc!=null) return;
            var b = true;
            if (name != null)
            {
                b = (i.name == name);
            }

            if (b)
            {
                tc = i;
            }
        });
        if (tc!=null)
        {
            tc.text = s;
        }
    }
    public static void SetSprite(RectTransform rt, UnityEngine.Sprite s, string name=null)
    {
        UnityEngine.UI.Image  tc = null;
        HierarchyUtility.TraverseComponent<UnityEngine.UI.Image>(rt.transform, i=> {
            if (tc!=null) return;
            var b = true;
            if (name != null)
            {
                b = (i.name == name);
            }
            if (b)
            {
                tc = i;
            }
        });
        if (tc!=null)
        {
            tc.sprite = s;
        }
    }

    // GetPos
    public static Vector3 GetPos(GameObject go)
    {
        var rt = go.GetComponent<RectTransform>();
        return GetPos(rt);
    }
    public static Vector3 GetPos(RectTransform rt)
    {
        return rt.anchoredPosition3D;
    }

    //Clone
    public static GameObject FindAndClone(Transform root, string name, GameObject parent)
    {
        var find = HierarchyUtility.FindGameObject(root,name);
        if (find !=null)
        {
            var clone = GameObject.Instantiate(find);
            clone.transform.SetParent(parent.transform);
            HierarchyUtility.TraverseSetLayerMask(clone.transform,LayerMask.NameToLayer("UI"));
            return clone;
        }
        return null;
    }
}
