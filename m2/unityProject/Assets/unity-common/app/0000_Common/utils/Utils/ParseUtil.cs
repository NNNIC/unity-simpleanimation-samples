using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseUtil {
    public static int ParseInt(string s, int errorvalue = int.MinValue)
    {
        int ret;
        double retf;
        if(int.TryParse(s, out ret))
            return ret;
        
        if (double.TryParse(s, out retf)) //小数点対策
            return (int)retf;
            
        return errorvalue;
    }
    public static long ParseLong(string sφ, long errorvalue= long.MinValue)
    {
        long ret;
        double retf;
        if (long.TryParse(sφ, out ret))
            return ret;
        if (double.TryParse(sφ, out retf))
            return (long)retf;

        return errorvalue;
    }
    public static float ParseFloat(string s, float errorvalue = float.NaN)
    {
        float ret;
        if (float.TryParse(s, out ret))
            return ret;

        return errorvalue;
    }
    public static List<float> ParseFloatList(string s, float errorvalue = float.NaN)
    {
        if (string.IsNullOrEmpty(s)) return null;

        var list = new List<float>();
        var tokens = s.Split(',');
        foreach(var i in tokens)
        {
            var f = ParseFloat(i,errorvalue);
            list.Add(f);
        }
        return list;
    }
    public static List<int> ParseIntList(string s, int errorvalue = int.MinValue)
    {
        if (string.IsNullOrEmpty(s)) return null;

        var list = new List<int>();
        var tokens = s.Split(',');
        foreach(var i in tokens)
        {
            var v = ParseInt(i,errorvalue);
            list.Add(v);
        }
        return list;
    }
    /// <summary>
    /// カンマ区切りの３つの数字からVector3を求める。 接頭接尾の括弧も受容
    /// ex) 1,2,3 => Vector3(1,2,3)
    /// ex) (3,4,5) => Vector3(3,4,5)
    /// </summary>
    public static Vector3 ParseVector3(string s, Vector3? errorvalue = null)
    {
        var verrorvalue = errorvalue == null ? new Vector3(float.NaN,float.NaN,float.NaN) : (Vector3)errorvalue;

        if (string.IsNullOrEmpty(s)) return verrorvalue;

        var ns = s.Trim().TrimStart('(').TrimEnd(')');
        var sp = ns.Split(',');
        if (sp.Length<3) return verrorvalue;

        var v = Vector3.zero;
        v.x = ParseFloat(sp[0],verrorvalue.x);
        v.y = ParseFloat(sp[1],verrorvalue.y);
        v.z = ParseFloat(sp[2],verrorvalue.z);

        return v;
    }
    /// <summary>
    /// カンマ区切りの２つの数字からVector2を求める。 接頭接尾の括弧も受容
    /// ex) 1,2 => Vector2(1,2)
    /// ex) (3,4) => Vector2(3,4)
    /// </summary>
    public static Vector2 ParseVector2(string s, Vector2? errorvalue = null)
    {
        var verrorvalue = errorvalue == null ? new Vector2(float.NaN,float.NaN) : (Vector2)errorvalue;

        if (string.IsNullOrEmpty(s)) return verrorvalue;

        var ns = s.Trim().TrimStart('(').TrimEnd(')');
        var sp = ns.Split(',');
        if (sp.Length<2) return verrorvalue;

        var v = Vector2.zero;
        v.x = ParseFloat(sp[0],verrorvalue.x);
        v.y = ParseFloat(sp[1],verrorvalue.y);

        return v;
    }

    /// <summary>
    /// Vector3の配列文字列をパース　括弧と括弧の間のカンマ(,)の有無は関係なし
    /// 例) (1,2,3),(4,5,6) => Vector3(1,2,3), Vector3(4,5,6)
    /// </summary>
    public static List<Vector3> ParseVector3List(string s, Vector3? errorvalue = null )
    {
        if (string.IsNullOrEmpty(s)) return null;;

        var list = new List<Vector3>();

        var toks = s.Split(')');
        foreach(var t in toks)
        {
            var ns = t.Trim().TrimStart(',','('); //最初の[,] と [(]を削除
            var v = ParseVector3(ns,errorvalue);

            list.Add(v);
        }

        return list;
    }

	/// <summary>
	/// Vector3の要素が'|'(セパレータ)で分離された文字列をパース
	/// 先頭の要素のみが対象
	/// </summary>
	public static List<Vector3> ParseVector3SeparatorList(string s, Vector3? errorvalue = null)
	{
        if (string.IsNullOrEmpty(s)) return null;
        var list = new List<Vector3>();
		var tokens = StringUtil.Split(s);
		if (tokens==null || tokens.Length==0) return null;
		
		var val = tokens[0].Trim('(',')').Trim();
		var valtokens = val.Split('|');

		for(var i = 0; i < valtokens.Length; i++)
		{
			var v = ParseVector3(valtokens[i],errorvalue);
			list.Add(v);
		}
		return list;
	}
    /// <summary>
    /// Vector2の配列文字列をパース　括弧と括弧の間のカンマ(,)の有無は関係なし
    /// 例) (1,2),(4,5) => Vector2(1,2), Vector2(4,5)
    public static List<Vector2> ParseVector2List(string s, Vector2? errorvalue = null)
    {
        if (string.IsNullOrEmpty(s)) return null;

        var list = new List<Vector2>();

        var toks = s.Split(')');
        foreach(var t in toks)
        {
            var ns = t.Trim().TrimStart(',','('); //最初の[,] と [(]を削除
            var v = ParseVector2(ns,errorvalue);

            list.Add(v);
        }

        return list;
    }

	
}