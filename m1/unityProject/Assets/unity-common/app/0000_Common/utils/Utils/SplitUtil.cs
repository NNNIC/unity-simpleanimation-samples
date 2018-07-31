using System;
using System.Collections.Generic;
using UnityEngine;

public class SplitUtil : MonoBehaviour {

	public static List<string> Split(string val)
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

        return list;
    }
}
