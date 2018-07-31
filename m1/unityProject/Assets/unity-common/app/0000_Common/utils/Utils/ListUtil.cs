using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUtil
{
    /// <summary>
    /// 指定したリストを指定した比較関数でソートしたリストを返す。元のリストはソートしない
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="srcφ"></param>
    /// <param name="comp"></param>
    /// <returns></returns>
    public static List<T> GetSortList<T>(List<T> srcφ, System.Comparison<T> comp = null)
    {
        if (srcφ == null)
            return null;
        if (srcφ.Count <= 0)
            return srcφ;
        List<T> dst = new List<T>();
        srcφ.ForEach((l) =>
        {
            dst.Add(l);
        });

        if (comp == null)
            dst.Sort();
        else
            dst.Sort(comp);
        return dst;
    }

    internal static string ToString(object vφ)
    {
        if (vφ==null) return "(null)";
        try {
            string s = null;

            var enumerable = vφ as IEnumerable;
            if (enumerable!=null)
            {
                int n = 0;
                foreach(var i in enumerable)
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

                    n++;
                }
            }
            else
            {
                return "-unknown-#1";
            }
            return s;

        }catch{
            return "-unknown-#2";
        }
    }

    public static bool IsValidIndex<T>(List<T> listφ, int index)
    {
        return listφ!=null && index >= 0 && index <listφ.Count; 
    }

    public static T Get<T>(List<T> list, int i) where T : class
    {
        if (IsValidIndex(list,i))
        {
            return list[i];
        }
        return null;
    }

}
