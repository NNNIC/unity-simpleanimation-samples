using UnityEngine;
using System.Collections;

public static class RayUtil {

    public static bool IsHit(out RaycastHit hitinfoφ, Vector3 origin, Vector3 dir, float len = 0)
    {
        Ray ray = new Ray(origin,dir);
        if (len==0)
        {
            Debug.DrawRay(origin,dir,Color.red,5);
            if (Physics.Raycast(ray,out hitinfoφ))
            {
                return true;
            }
        }
        else
        {
            Debug.DrawLine(origin,origin + dir.normalized * len);
            if (Physics.Raycast(ray,out hitinfoφ,len))
            {
                return true;
            }
        }
        return false;
    }

    public static RaycastHit? GetHitObject(Camera cam=null)
    {
        if (cam==null) cam = Camera.main;
        Ray ray= cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            return hit;
        }
        return null;
    }
}
