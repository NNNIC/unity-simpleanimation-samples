using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UniLinq;
using System;

public static class StringUtil {

    public static readonly string NL = System.Environment.NewLine;
    public static readonly string DQ = "\"";

    public static string PackDQ(string sφ) { return DQ + sφ + DQ; }

    public static char GetLastChar(string s)
    {
        if (string.IsNullOrEmpty(s)) throw new System.Exception();
        return s[s.Length-1];
    }

	public static string GetLast(string s, int n)
	{
        if (string.IsNullOrEmpty(s)) return null;
		if (s.Length < n) return null;

		return s.Substring(s.Length - n, n);
	}

    public static bool CheckPrefix(string s, string prefix)
    {
        if (string.IsNullOrEmpty(s)) return false;
        if (string.IsNullOrEmpty(prefix)) return true;  //prefixが指定されてなければ、trueを返す

        if (s.Length < prefix.Length) return false; //文字列がprefixより短い

        return (s.Substring(0,prefix.Length)==prefix);
    }

    public static bool CheckSurfix(string s, string surfix)
    {
        if (string.IsNullOrEmpty(s)) return false;
        if (string.IsNullOrEmpty(surfix)) return true;

        if (s.Length < surfix.Length) return false;

        return (s.Substring(s.Length-surfix.Length)==surfix);
    }

    /// <summary>
    /// ワイルドカードが入った文字列をチェックする。
    /// ※ワイルドカード(*)は１ヵ所のみ使用可能
    /// ※?を使用可能
    /// </summary>
    public static bool CheckWildcard(string i_s, string i_pattern, bool ignoreCase)
    {
        string s= i_s.Trim();
        string pattern = i_pattern.Trim();

        Func<char,char, bool> cp = (c,p) =>
        {
            if (p=='?') return true;
            var a = ignoreCase ? char.ToUpper(c) : c;
            var b = ignoreCase ? char.ToUpper(p) : p;

            return a == b;
        };


        Func<string,string,bool> _checkWord = (w,ptn) =>
        {
            if (w.Length != ptn.Length) return false;
            for(int i = 0; i<w.Length; i++)
            {
                if (!cp(w[i],ptn[i])) return false;
            }
            return true;
        };

        Func<string,string,bool> _checkPrefix =  (w,ptn) =>
        {
            if (w.Length < ptn.Length) return false;
            var w2 = w.Substring(0,ptn.Length);
            return _checkWord(w2,ptn);
        };

        Func<string,string,bool> _checkSurfix =  (w,ptn) =>
        {
            if (w.Length < ptn.Length) return false;
            var w2 = w.Substring(w.Length - ptn.Length, ptn.Length);
            return _checkWord(w2,ptn);
        };

        if (pattern.Contains("*"))
        {
            if (pattern == "*") return true;
            
            string pre = null;
            string sur = null;

            if (pattern[0] == '*') 
            {
                sur = pattern.Substring(1);
            }
            else if (pattern[pattern.Length-1]=='*')
            {
                pre = pattern.Substring(0,pattern.Length-1);
            }
            else
            {
                var i = pattern.IndexOf('*');
                if (i!=pattern.LastIndexOf('*')) throw new System.Exception("Use A '*' in the string."); 
                pre = pattern.Substring(0,i);
                sur = pattern.Substring(i+1);
            }

            bool preOk = (pre==null);
            bool surOk = (sur==null);
            if (pre!=null) 
            {
                preOk = _checkPrefix(s,pre);
            }
            if (sur!=null)
            {
                surOk = _checkSurfix(s,sur);
            }

            return preOk && surOk;
        }

        return _checkWord(s,pattern);
    }

    /// <summary>
    ///  ＩＤ文字列を１加算する。
    ///  ie.  MD_0100 -> MD_01001
    /// </summary>
    public static string IncrementID(string id)
    {
        // 低レベル計算機風
        bool parity =true;
        Func<char,char> Calc = (c)=>
        {
            if (c<'0' || c>'9') return c;

            char nc=c;
            int x = int.Parse(new string(c,1));
            if (parity)
            {
                parity = false;

                x++;
                if (x==10)
                {
                    parity = true;
                    nc = '0';
                }
                else
                {
                    nc = (char)('0' + x);
                }
            }
            return nc;
        };

        //文字列を後方から計算する
        string newid = "";
        for(int i=id.Length-1; i>=0; i--)
        {
            var a = Calc(id[i]);
            newid = (new string(a,1)) + newid;
        }

        return newid;
    }

    /// <summary>
    /// 短い文字列に対してパディングを先頭に追加する。
    /// </summary>
    public static string AddPaddingHead(string s, char c, int len)
    {
        if (s.Length >= len) return s;
        var s1 = s;
        int n = len - s.Length;
        for(int i = 0; i<n; i++)
        {
            s1 = new string(c,1) + s1;
        }
        return s1;
    }

    public static string ToBase64(string s)
    {
        var l = System.Text.Encoding.UTF8.GetBytes(s);
        return System.Convert.ToBase64String(l);
    }
    
    public static string FromBase64(string s)
    {
        var l = System.Convert.FromBase64String(s);
        return System.Text.Encoding.UTF8.GetString(l);
    }

    /// <summary>
    /// カンマ区切りの文字列リストを結合(Union)する
    /// </summary>
    /// <param name="i_src"></param>
    /// <returns></returns>
    public static string MergeStringList(string s1, string s2)
    {
        string res = "";
        if (string.IsNullOrEmpty(s1))
            return s2;
        if (string.IsNullOrEmpty(s2))
            return s1;
        string[] __s1a = s1.Split(',');
        string[] s2a = s2.Split(',');

        List<string> s1a = new List<string>();
        List<string> news = new List<string>();
        for (int n = 0; n < __s1a.Length; n++)
        {
            s1a.Add(__s1a[n]);
            news.Add(__s1a[n]);
        }
        for (int i = 0; i < s2a.Length; i++)
        {
            bool bFound = false;
            for (int n = 0; n < s1a.Count; n++)
            {
                if (s2a[i] == s1a[n])
                {
                    bFound = true;
                    break;
                }
            }
            if (bFound == false)
                news.Add(s2a[i]);
        }

        for (int i = 0; i < news.Count; i++)
        {
            if (i != 0)
                res += ",";
            res += news[i];
        }
        return res;
    }

    /// <summary>
    /// カンマ区切り文字列作成
    /// </summary>
    /// <param name="i_res"></param>
    /// <returns></returns>
    public static string MakeStringList(List<string> i_list)
    {
        string res = "";
        for (int i = 0; i < i_list.Count; i++)
        {
            if (i != 0)
                res += ",";
            res += i_list[i];
        }
        return res;
    }

    /// <summary>
    /// カンマ区切り文字列作成
    /// </summary>
    /// <param name="i_res"></param>
    /// <returns></returns>
    public static string MakeStringList(List<int> i_list)
    {
        string res = "";
        for (int i = 0; i < i_list.Count; i++)
        {
            if (i != 0)
                res += ",";
            res += i_list[i].ToString();
        }
        return res;
    }

    /// <summary>
    /// カンマ区切り文字列作成
    /// </summary>
    /// <param name="i_res"></param>
    /// <returns></returns>
    public static string MakeStringList(List<long> i_list)
    {
        string res = "";
        for (int i = 0; i < i_list.Count; i++)
        {
            if (i != 0)
                res += ",";
            res += i_list[i].ToString();
        }
        return res;
    }

	/// <summary>
	///  括弧を考慮したSplit
	///  例：
	///  (a,b,c),{d,[e]},f ==> string[] {"(a,b,c)","{d,[e]}","f" }
	/// </summary>
	public static string[] Split(string val)
	{
        if (string.IsNullOrEmpty(val)) return null;

        var list = new List<string>();

        char[] pair_start = new char[] { '(','[','{' };
        char[] pair_end   = new char[] { ')',']','}' };

        var stack = new List<char>();
        Func<char> expect_endchar = ()=> stack.Count>0 ? stack[0] : (char)0;

        var word = string.Empty;
        for(var i = 0; i<val.Length; i++)
        {
            var c = val[i];
            if (expect_endchar()==0 && (c==','))
            {
                list.Add(word);
                word = string.Empty;
                continue;
            }
            var pair_start_index = Array.FindIndex(pair_start,v=>v==c);
            if (pair_start_index>=0)
            {
                var endchar = pair_end[pair_start_index];
                stack.Insert(0,endchar);
            }
            else if (c==expect_endchar())
            {
                stack.RemoveAt(0);
            }

            word += c;
        }
        if (!string.IsNullOrEmpty(word)) list.Add(word);

        return list.ToArray();

	}
}
