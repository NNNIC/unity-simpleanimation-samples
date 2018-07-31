using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ReflectionUtil {

    /// <summary>
    /// staticで定義されたメンバ変数をポインタクラスのメンバ変数にコピー
    /// ※publicで名前と型が一致すればコピーを実行　それ以外は無視
    /// ※配列未対応
    /// </summary>
    public static void Copy<T>(Type src_t, T dst)
    {
        var dst_t = typeof(T);
        Copy(src_t,null,dst_t,dst);
    } 

    /// <summary>
    /// ポインタクラスのメンバ変数をstaticで定義されたメンバ変数にコピー
    /// ※publicで名前と型が一致すればコピーを実行　それ以外は無視
    /// ※配列未対応
    /// </summary>
    public static void Copy<T>(T src, Type dst_t)
    {
        var src_t = typeof(T);
        Copy(src_t,src, dst_t,null);
    }
    /// <summary>
    /// クラス間のコピーを行う。
    /// staticを指定する場合はnullを指定する
    /// ※publicで名前と型が一致すればコピーを実行　それ以外は無視
    /// ※配列未対応
    /// </summary>
    public static void Copy(Type src_t, System.Object src, Type dst_t, System.Object dst)
    {
        var bindflag  = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly ;
        var src_members = src_t.GetMembers( bindflag );
        var dst_members = dst_t.GetMembers( bindflag );

        foreach(var src_mi in src_members)
        {
            MemberInfo dst_mi = Array.Find(dst_members,m=>m.Name == src_mi.Name);
            if (dst_mi == null) continue;
            
            try {
                var val = GetValue(src_mi,src);
                SetValue(dst_mi,val,dst);
                //Debug.Log("Copy " + src_t.Name + "." + src_mi.Name + " to " +   dst_t.Name + " pointer");
            }
            catch { }
        }
    }

    public static object GetValue(MemberInfo mi, System.Object p)
    {
        if (  ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL)
        {
            var fi =  mi.ReflectedType.GetField(mi.Name);
            return fi.GetValue(p);
        }
        if ( ((ulong)mi.MemberType & (ulong)MemberTypes.Property) != 0UL)
        {
            var pi = mi.ReflectedType.GetProperty(mi.Name);
            return pi.GetValue(p,null);
        }
        throw new SystemException();
    }

    public static void SetValue(MemberInfo mi, System.Object val, System.Object memberowner = null)
    {
        if (  ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL)
        {
            var fi =  mi.ReflectedType.GetField(mi.Name);
            fi.SetValue(memberowner,val);
            return;
        }
        if ( ((ulong)mi.MemberType & (ulong)MemberTypes.Property) != 0UL)
        {
            var pi = mi.ReflectedType.GetProperty(mi.Name);
            pi.SetValue(memberowner,val,null);
            return;
        }
        throw new SystemException();
    } 
		
    private static bool _getValue_ifPrimitive(object o, out string s)
    {
        if (o==null) {s = "-null-"; return true; }
        var type = o.GetType();
        if (type.IsPrimitive || type == typeof(string))
        {
            s= o.ToString();
            return true;
        }
        s= null;
        return false;
    }
    private static string _getValue(object o)
    {
        string s = null;
        if (o==null) {s = "-null-"; return s; }
        if (_getValue_ifPrimitive(o,out s))
        {
            return s;
        }

        var type = o.GetType();
        if (type.IsArray)
        {
			s += "[";
			var a = (Array)o;
			foreach(var i in a)
			{
				s+=i.ToString();
			}
			s += "]";
        }
		else if (o is IList)
		{
			s+= "[";
			var l = (IList)o;
			foreach(var i in l)
			{
				s += i.ToString();
			}
			s += "]";
		}
		else
		{
			s+= o.ToString();
		}
		return s;
    }

	private static Dictionary<string, object> _getAllMembers(object o, bool bDeclaredOnly)
	{
		var list = new Dictionary<string, object>();

        var type = o.GetType();
        var bindflag  = BindingFlags.Public |BindingFlags.Static | BindingFlags.Instance ;
		if (bDeclaredOnly)
		{
			bindflag |= BindingFlags.DeclaredOnly ;
		}
        MemberInfo[] members = null;

		try {
	        members = type.GetMembers( bindflag );
		} catch { return null;}

        foreach(var mi in members)
        {
            //string ns = "-";
            try {
                if (mi.Name.StartsWith("get_")) continue;
                if (mi.Name == ".ctor"         ) continue;
                if (mi.Name.StartsWith("set_")) continue;

                if (  ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL && ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL)
                {
                    var fi =  mi.ReflectedType.GetField(mi.Name);
                    var a = fi.GetValue(o);
					list.Add(mi.Name,a);
                }
                else if ( ((ulong)mi.MemberType & (ulong)MemberTypes.Property) != 0UL)
                {
                    var pi = mi.ReflectedType.GetProperty(mi.Name);
                    var a = pi.GetValue(o,null);
					list.Add(mi.Name,a);
                }
            } catch {}
        }
        return list;
	}

    public static string Dump(object o, bool bDeclaredOnly = false, int depth=1)
    {
		var nest = -1;
	
		Func<object,string> _to_string =null;

		_to_string = (e)=>{
			var ns = string.Empty;

			nest ++;

			if (e==null)  {nest --; return "-null-";}
			var t = e.GetType();
			if (t.IsPrimitive || e is string || t.IsEnum)
			{
				{nest--; return e.ToString();}
			}
			else if (e is Array)
			{
				ns += "[";
				foreach(var i in (Array)e)
				{
					if (nest < depth)
					{
						ns += _to_string(i);
					}
					else
					{
						if (i!=null)
						{
							ns += i.ToString();
						}
						else 
						{
							ns += "-null-";
						}
					}
					ns += ",";
				}
				ns += "]";
				{nest--; return ns;}
			}
			else if (e is IList)
			{
				ns += "[";
				foreach(var i in (IList)e)
				{
					if (nest < depth)
					{
						ns += _to_string(i);
					}
					else
					{
						if (i!=null)
						{
							ns += i.ToString();
						}
						else 
						{
							ns += "-null-";
						}
					}
					ns += ",";
				}
				ns += "]";
				{nest--; return ns;}
			}
			else {
				var list = _getAllMembers(e,bDeclaredOnly);
				if (list == null)
				{
					{nest--; return "unknown";}
				}

				ns = "{";
				foreach(var p in list)
				{
					ns += p.Key +"=";
					if (nest < depth)
					{
						ns += _to_string(p.Value);
					}
					else
					{
						if (p.Value!=null)
						{
							ns += p.Value.ToString();
						}
						else 
						{
							ns += "-null-";
						}
					}
					ns += ",";
				}
				ns += "}";
				{nest--; return ns;}
			}
		};

		return _to_string(o);


		//string s;
		//if (_getValue_ifPrimitive(o, out s))
		//{
		//	return s;
		//}
		//var type = o.GetType();
		//var bindflag  = BindingFlags.Public |BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly ;
		//MemberInfo[] members = null;

		//try {
		//	members = type.GetMembers( bindflag );
		//} catch {
		//	return "- unable to analyze - ";
		//}

		//foreach(var mi in members)
		//{
		//	string ns = "-";
		//	try {
		//		if (mi.Name.StartsWith("get_")) continue;
		//		if (mi.Name == ".ctor"         ) continue;
		//		if (mi.Name.StartsWith("set_")) continue;

		//		if (  ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL && ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL)
		//		{
		//			var fi =  mi.ReflectedType.GetField(mi.Name);
		//			var a = fi.GetValue(o);
		//			ns = _getValue(a);
		//		}
		//		else if ( ((ulong)mi.MemberType & (ulong)MemberTypes.Property) != 0UL)
		//		{
		//			var pi = mi.ReflectedType.GetProperty(mi.Name);
		//			var a = pi.GetValue(o,null);
		//			ns = _getValue(a);
		//		}
		//	} catch {}
            
		//	s += mi.Name +"=" + ns +",";
		//}
		//return s;
    }


	//private static string _dump(object o, int nestcount, int maxdepth, string s)
	//{
	//	string ns;
	//	if (_getValue_ifPrimitive(o, out ns))
	//	{
	//		s+= ns;
	//		return s;
	//	}
	//	var type = o.GetType();
	//	var bindflag  = BindingFlags.Public |BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly ;
	//	MemberInfo[] members = null;

	//	try {
	//		members = type.GetMembers( bindflag );
	//	} catch {
	//		return "- unable to analyze - ";
	//	}

	//	foreach(var mi in members)
	//	{
	//		ns = "-";
	//		try {
	//			if (mi.Name.StartsWith("get_")) continue;
	//			if (mi.Name == ".ctor"         ) continue;
	//			if (mi.Name.StartsWith("set_")) continue;

	//			if (  ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL && ((ulong)mi.MemberType & (ulong)MemberTypes.Field) != 0UL)
	//			{
	//				var fi =  mi.ReflectedType.GetField(mi.Name);
	//				var a = fi.GetValue(o);
	//				ns = _getValue(a);
	//			}
	//			else if ( ((ulong)mi.MemberType & (ulong)MemberTypes.Property) != 0UL)
	//			{
	//				var pi = mi.ReflectedType.GetProperty(mi.Name);
	//				var a = pi.GetValue(o,null);
	//				ns = _getValue(a);
	//			}
	//		} catch {}
            
	//		s += mi.Name +"=" + ns +",";
	//	}
	//	return s;
	//}
}
