using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RandomUtil  {

    public static T Get<T>(T[] list)
    {
        int r = Random.Range(0,int.MaxValue);
        return list[r % list.Length]; 
    }

    public static T Get<T>(List<T> list)
    {
        int r = Random.Range(0,int.MaxValue);
        return list[r % list.Count]; 
    }
    public static T[] Get<T>(T[] list, int numφ)
    {
        List<T> seList = new List<T>();
        List<T> tlist = new List<T>(list);
        for(int i=0;i<numφ; i++)
        {
            if (tlist.Count==0) break;
            int n = Random.Range(0,int.MaxValue) % tlist.Count;
            var s = tlist[n];
            tlist.RemoveAt(n);
            seList.Add(s);
        }
        return seList.ToArray();
    }
    public static List<T> Get<T>(List<T> list, int num)
    {
        List<T> seList = new List<T>();
        List<T> tlist = new List<T>(list);
        for(int i=0;i<num; i++)
        {
            if (tlist.Count==0) break;
            int n = Random.Range(0,int.MaxValue) % tlist.Count;
            var s = tlist[n];
            tlist.RemoveAt(n);
            seList.Add(s);
        }
        return seList;
    }
    public static int Mod(int num)
    {
        int r = Random.Range(0,int.MaxValue);
        return r % num;
    }
    /// <summary>
    /// ランダムint値を得る。 maxは最大値 ( GetInt(2)を指定すると 0,1,2のどれかの値が返る )
    /// </summary>
    /// <returns></returns>
    public static int GetInt(int max)
    {
        int x =Random.Range(0,max+1);
        return x;
    }

}
