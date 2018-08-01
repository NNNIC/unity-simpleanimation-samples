using System.Collections;
using System;

public static class EnumUtil {

    public static T GetNextCyclic<T>(T enumvaliableφ)
    {
        var names = Enum.GetNames(typeof(T));
        int i = Array.FindIndex(names,s=>s==enumvaliableφ.ToString());
        i = (i+1) % names.Length;

        return (T)Enum.Parse(typeof(T),names[i]);
    }
    
    public static T GetPrevCyclic<T>(T enumvaliable)
    {
        var names = Enum.GetNames(typeof(T));
        int i = Array.FindIndex(names,s=>s==enumvaliable.ToString());
        i = (i+ names.Length - 1) % names.Length;

        return (T)Enum.Parse(typeof(T),names[i]);
    }

    public static bool IsFirst<T>(T enumvaliable)
    {
        var names = Enum.GetNames(typeof(T));
        int i = Array.FindIndex(names,s=>s==enumvaliable.ToString());
        return (i==0);
    }

    public static bool IsLast<T>(T enumvaliableφ)
    {
        var names = Enum.GetNames(typeof(T));
        int i = Array.FindIndex(names,s=>s==enumvaliableφ.ToString());
        return (i==names.Length-1);
    }

    public static bool IsOnFlag(int val, int chk)
    {
        return ((val & chk) != 0) ? true : false;
    }
    public static bool IsOffFlag(int val, int chk)
    {
        return ((val & chk) == 0) ? true : false;
    }
    public static object TryParse(Type t,  string s) 
    {
        if (string.IsNullOrEmpty(s)) return null;
        if (!t.IsEnum) return null;
        if (Enum.IsDefined(t,s))
        {
            return Enum.Parse(t,s);
        }
        return null;
    }
	public static T TryParse<T>(string s)
	{
		return (T)TryParse(typeof(T), s);
	}
    public static object TryParse_IgnoreCase(Type t,  string s) 
    {
        if (string.IsNullOrEmpty(s)) return null;
        if (!t.IsEnum) return null;
        var allnames = Enum.GetNames(t);
        foreach(var n in allnames)
        {
            if (n.ToUpper() == s.ToUpper())
            {
                return Enum.Parse(t,n);
            }
        }
        return null;
    }
    public static bool IsValidValue(Type t, int n)
    {
        var l = Enum.GetValues(t);
        foreach(var a in l)
        {
            if (a.GetType()!= typeof(int)) continue;
            int x = (int)a;
            if (x==n) return true;
        }
        return false;
    }
}
