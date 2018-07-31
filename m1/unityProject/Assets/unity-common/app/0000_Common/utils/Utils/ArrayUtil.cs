using System.Collections;
using System.Collections.Generic;
using System;

public class ArrayUtil  {

    /// <summary>
    /// 配列の次の値を取得。ない場合は先頭から。
    /// 重複した値がある場合は正しく動作しない
    /// 該当下物がない場合最初の値を返す
    /// </summary>
    public static T GetNextCycril<T>(T[] listfφ,  T val)
    {
        int i = Array.FindIndex(listfφ,v=>v.Equals(val));
        if (i<0) return listfφ[0];
        i = (i+1) % listfφ.Length;
        
        return listfφ[i];
    }

    public static T GetPrevCycril<T>(T[] listφ,  T val)
    {
        int i = Array.FindIndex(listφ,v=>v.Equals(val));
        if (i<0) return listφ[0];
        i = (i+ listφ.Length - 1) % listφ.Length;
        
        return listφ[i];
    }

    internal static string ToString<T>(T[] val)
    {
        if (val==null) return "(null)";
        string s = null;
        Array.ForEach(val,i=>{
            if (s!=null) s+=",";
            s+=i.ToString();
        });
        return s;
    }
    internal static string ToString(object valφ)
    {
        if (valφ==null) return "(null)";
        string s = null;

        try {
            int n = 0;
            foreach(var i in (Array)valφ)
            {
                if (s!=null) s+=",";
                var d = i.ToString();
                if (d.Contains("\n"))
                {
                    s+= string.Format("\n\n[{0}]\n",n);
                    s+=d;
                }
                else 
                {
                    s+= d;
                }
            }
        }
        catch
        {
            s+="- unknown -";
        }
        return s;
    }

    public static bool IsValidIndex<T>(T[] list, int index)
    {
        return list!=null && index >= 0 && index <list.Length; 
    }

    public static T Get<T>(T[] list, int i) where T : class
    {
        if (IsValidIndex(list,i))
        {
            return list[i];
        }
        return null;
    }

	/// <summary>
	/// リスト中の最小値のインデックスを返す
	/// </summary>
	public static int GetMinIndex(float[] list)
	{
		if (list == null)     return -1;
		if (list.Length == 1) return 0;

		var minvalue = float.MaxValue;
		var minindex = -1;
		for(var i = 0; i<list.Length; i++)
		{
			var a = list[i];
			if (a < minvalue)
			{
				minvalue = a;
				minindex = i;
			}
		}
		return minindex;
	}
}

