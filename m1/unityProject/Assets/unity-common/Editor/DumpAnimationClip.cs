using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DumpAnimationClip {

    [MenuItem("Assets/dump animation clip")]    
    static void Dump()
    {
        if (Selection.assetGUIDs==null || Selection.assetGUIDs.Length!=1)
        {
            Debug.LogError("Select an animation clip in the project window");
            return;
        }		

        var path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);

        var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
        if (clip==null)
        {
            Debug.LogError("Select an animation clip in the project window");
            return;
        }

        {
            Debug.Log("#  AnimationUtility.GetCurveBindings #");
            var allbindlings =  AnimationUtility.GetCurveBindings(clip);
        
            foreach(var bind in allbindlings)
            {
                var keyframes = AnimationUtility.GetObjectReferenceCurve(clip,bind);
                if (keyframes!=null)
                {
                    Debug.Log( bind.path + "/" + bind.propertyName +", keys" + keyframes.Length );
                } 
                else
                {
                    var curves = AnimationUtility.GetEditorCurve(clip,bind);
                    string s = null;
                    for(var i = 0; i<curves.length; i++)
                    {
                        if (s!=null) s+="-";
                        s+= "(" + curves[i].time + ":" + curves[i].value +")";
                    }
                    Debug.Log( bind.path + "/" + bind.propertyName + "(" + bind.type + "), curve keys = " + curves.keys.Length +"," + s);
                }
            }
        }        

        {
            Debug.Log("#  AnimationUtility.GetCurveBindings #");
            var allbindlings =  AnimationUtility.GetObjectReferenceCurveBindings(clip);
            foreach(var bind in allbindlings)
            {
                var keyframes = AnimationUtility.GetObjectReferenceCurve(clip,bind);
                if (keyframes!=null)
                {
                    Debug.Log( bind.path + "/" + bind.propertyName +", keys" + keyframes.Length );
                } 
                else
                {
                    var curves = AnimationUtility.GetEditorCurve(clip,bind);
                    string s = null;
                    for(var i = 0; i<curves.length; i++)
                    {
                        if (s!=null) s+="-";
                        s+= "(" + curves[i].time + ":" + curves[i].value +")";
                    }
                    Debug.Log( bind.path + "/" + bind.propertyName + "(" + bind.type + "), curve keys = " + curves.keys.Length +"," + s);                    
                }
            }            
        }
    }
}