//<<<include=inc.txt
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using UnityEngine;
//>>>
public class MainStateControl : MonoBehaviour {

    public static MainStateControl V;

    MainControl m_mc;

    void Start () {
        V = this;
        m_mc = new MainControl();
        m_mc.Start();
	}

	void Update () {
        // 1 フレームに一個イベントを処理
        MainStateEvent.Pop();
        if (m_mc!=null) m_mc.update();
	}
}
