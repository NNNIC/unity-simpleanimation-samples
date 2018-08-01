using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class VectorUtil {

    public static Vector3 Set_X(Vector3 pos, float x)
    {
        return new Vector3(x, pos.y, pos.z);
    }

    public static Vector3 Set_Y(Vector3 posφ, float y)
    {
        return new Vector3(posφ.x, y, posφ.z);
    }

    public static Vector3 Set_Z(Vector3 pos, float z)
    {
        return new Vector3(pos.x, pos.y, z);
    }

    public static Vector3 Zero_X(Vector3 pos)
    {
        return new Vector3(0,pos.y, pos.z);
    }
    public static Vector3 Zero_Y(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }
    public static Vector3 Zero_Z(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, 0);
    }
    public static Vector3 Zero_XY(Vector3 pos)
    {
        return new Vector3(0, 0, pos.z);
    }
    public static Vector3 Zero_XZ(Vector3 pos)
    {
        return new Vector3(0, pos.y, 0);
    }
    public static Vector3 Zero_YZ(Vector3 pos)
    {
        return new Vector3(pos.x, 0, 0);
    }


    public static Vector3 Affect_X(Vector3 pos, float x)
    {
        return Zero_X(pos) + Vector3.right * x;
    }
    public static Vector3 Affect_Y(Vector3 pos, float y)
    {
        return Zero_Y(pos) + Vector3.up * y;
    }
    public static Vector3 Affect_Z(Vector3 pos, float z)
    {
        return Zero_Z(pos) + Vector3.forward * z;
    }
    public static Vector3 Affect_XY(Vector3 pos, float x, float y)
    {
        return new Vector3(x,y,pos.z);
    }
    public static Vector3 Affect_YZ(Vector3 pos, float y, float z)
    {
        return new Vector3(pos.x,y,z);
    }
    public static Vector3 Affect_XZ(Vector3 pos, float x, float z)
    {
        return new Vector3(x,pos.y,z);
    }


    public static Vector3 ToVector3(Vector2 v)
    {
        return new Vector3(v.x,v.y);
    }

    public static Vector3 Minus_X(Vector3 pos)
    {
        return new Vector3(-pos.x, pos.y, pos.z);
    }
    public static Vector3 Minus_Y(Vector3 pos)
    {
        return new Vector3(pos.x, -pos.y, pos.z);
    }
    public static Vector3 Minus_Z(Vector3 pos)
    {
        return new Vector3(pos.x, pos.y, -pos.z);
    }
    public static Vector3 Minus_YZ(Vector3 pos)
    {
        return new Vector3(pos.x, -pos.y, -pos.z);
    }
    public static Vector3 Scale(Vector3 pos, Vector3 scale)
    {
        return new Vector3(pos.x * scale.x, pos.y * scale.y, pos.z * scale.z);
    }
    public static Vector3 Scale(Vector3 pos, float sx, float sy, float sz)
    {
        return new Vector3(pos.x * sx, pos.y * sy, pos.z * sz);
    }
    public static Vector3 Clamp(Vector3 pos, Vector3 min, Vector3 max)
    {
        return new Vector3( 
            min.x<max.x ?  Mathf.Clamp(pos.x, min.x, max.x) : pos.x , 
            min.y<max.y ?  Mathf.Clamp(pos.y, min.y, max.y) : pos.y , 
            min.z<max.z ?  Mathf.Clamp(pos.z, min.z, max.z) : pos.z 
        );
    }
    public static Vector3 Add(Vector3 pos, float d)
    {
        return new Vector3(pos.x + d, pos.y +d , pos.z + d);
    }
    public static Vector3 Add_X(Vector3 pos, float d)
    {
        return new Vector3(pos.x + d, pos.y, pos.z);
    }
    public static Vector3 Add_Y(Vector3 pos, float d)
    {
        return new Vector3(pos.x, pos.y + d, pos.z);
    }
    public static Vector3 Add_Z(Vector3 pos, float d)
    {
        return new Vector3(pos.x, pos.y, pos.z + d);
    }
    public static Vector3 Abs(Vector3 pos)
    {
        return new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z));
    }
    public static Vector2 Vector2_XZ(Vector3 pos)
    {
        return new Vector2(pos.x, pos.z);
    }
    public static Vector3 Replace_XY(Vector3 pos)
    {
        return new Vector3(pos.y,pos.x,pos.z);
    }


    /// <summary>
    ///３次元補間関数 Cutmul-Rom Spline ※指定点通過型
    /// http://www.mvps.org/directx/articles/catmull/
    /// 補間対象区域の外２点を追加指定して補間値を求める
    /// vPrev  -- vStartの前 
    /// vStart -- 補間 始点
    /// vEnd   -- 補間 終了点
    /// vNext  -- vEndの次点
    /// </summary>
    public static Vector3 CutmullRomSpline(Vector3 vPrev, Vector3 vStart, Vector3 vEnd, Vector3 vNext, float t)
    {
        var p0 = vPrev;
        var p1 = vStart;
        var p2 = vEnd;
        var p3 = vNext;

        var p = 0.5f *( (2*p1) +
                        (-p0+p2) * t +
                        (2*p0-5*p1+4*p2-p3) * t* t +
                        (-p0 + 3*p1 - 3*p2 + p3) * t * t * t);
        return p;
    }

    /// <summary>
    /// 点と線の距離を求める
    /// http://www.sousakuba.com/Programming/gs_dot_line_distance.html
    /// </summary>
    /// <param name="p">ポイント</param>
    /// <param name="a">ラインのＡ点</param>
    /// <param name="b">ラインのＢ点</param>
    /// <returns></returns>
    public static float GetLengthPointFromLine(Vector3 p, Vector3 a, Vector3 b)
    {
        var D = Vector3.Cross((b-a),(p-a)).magnitude;
        return D / (b-a).magnitude;
    }

    /// <summary>
    /// UV座標を0～1内へリピートを考慮してクランプします。
    /// </summary>
    public static Vector2 UVClamp01(Vector2 uv)
    {
        Func<float,float> _clamp01 = (a)=>{
            float v = a<0 ? a  - Mathf.Floor(a) : a;
            return v % 1.0f;
        }; 
        
        return new Vector2(_clamp01(uv.x),_clamp01(uv.y));
    }

    /// <summary>
    /// 点配列の距離を求める
    /// </summary>
    /// <param name="positions"></param>
    /// <returns></returns>
    public static float GetLength(List<Vector3> positions)
    {
        float len = 0;
        for(int i = 0; i<positions.Count-1; i++)
        {
            var a = positions[i];
            var b = positions[i+1];
            if (a==b) continue;

            len += (b-a).magnitude;
        }
        return len;
    }

    public static float[] ToFloat3(Vector3 v)
    {
        var l = new float[3] {v.x,v.y,v.z};
        return l;
    }
    public static Vector3 ToVector3(float[] f)
    {
        if (f==null || f.Length<3) return Vector3.zero;
        return new Vector3(f[0],f[1],f[2]);
    }

    public static bool IsEqual(Vector3 v1, Vector3 v2, float e=float.Epsilon)
    {
        var d = Abs(v1-v2);
        return (d.x <= e && d.y <= e && d.z <=e);
    }
}
