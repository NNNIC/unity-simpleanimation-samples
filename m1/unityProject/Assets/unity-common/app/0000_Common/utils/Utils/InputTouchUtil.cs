using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchUtil  {

    public static Vector3? GetTouchPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            return Input.mousePosition;
        }
        else
        {
            return null;
        }
#elif UNITY_IPHONE || UNITY_ANDROID
        if (Input.touchCount==0) return null;
        return Input.touches[0].position;        
#endif
    }
    public static Vector3? GetTouchPosition(TouchPhase phase)
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            return Input.mousePosition;
        }
        else
        {
            return null;
        }
#elif UNITY_IPHONE || UNITY_ANDROID
        if (Input.touchCount==0) return null;
        if (Input.touches[0].phase == phase)
        {      
            return Input.touches[0].position;        
        }
        return null;
#endif
    }

    static Vector3 m_savePosition;
    public static Vector3? GetDeltaPosition(bool bReset=false)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (bReset) 
        {
            m_savePosition = Input.mousePosition;
            return Vector3.zero;
        }
        var d = Input.mousePosition - m_savePosition;
        m_savePosition = Input.mousePosition;
        return d;
#elif UNITY_IPHONE || UNITY_ANDROID
        if (Input.touchCount==0) return null;
        return Input.touches[0].deltaPosition;
#endif
    }




}
