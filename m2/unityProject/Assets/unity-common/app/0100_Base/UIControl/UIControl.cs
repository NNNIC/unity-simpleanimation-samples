using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    [HideInInspector]
    public UIControlCompo m_tc;

    public static UIControl V;
    [HideInInspector]
    public ProcessState m_state = ProcessState.UNKNOWN;
    
    public void ReqStart()
    {
        m_state = ProcessState.STARTING;
    }

	// Use this for initialization
	IEnumerator Start () {
        V = this;
        m_state = ProcessState.WAIT_START;

        yield return null; // 全ComponentのStart完了

        while(m_state == ProcessState.WAIT_START) yield return null;

        
        Debug.Log("..Start !");

        UISpriteManager.V.ReqStart();
        while(UISpriteManager.V.m_state != ProcessState.RUNNING) yield return null;

        m_tc = GetComponent<UIControlCompo>();
        m_tc.SetTarget_TemplateAndStart();

        m_state = ProcessState.RUNNING;

        while(true)
        {
            if (m_tc.IsEnd()) break;

            yield return null;
        }

        Debug.Log("..END !!");
	}
	
}
