using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UISpriteManager : MonoBehaviour {

    // Write sprite texture path in resources.
    public string[] m_sprite_texure_resources;

    [HideInInspector]
    public List<Sprite> m_sprite_list;

    public static UISpriteManager V;
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

        if (m_sprite_texure_resources==null || m_sprite_texure_resources.Length == 0)
        {
            ErrorDlg.V.SetError("Unexpected. {7272B075-E326-4029-B878-39C3985A65EA}");
            yield break;
        }

        m_sprite_list = new List<Sprite>();

        bool bError =false;
        Array.ForEach(m_sprite_texure_resources,p=> {
            if (bError) return;

            var sprites = ResourcesUtil.LoadAllSprites(p);
            if (sprites==null)
            {
                bError = true;
            }
            else
            {
                m_sprite_list.AddRange(sprites);
            }
        });

        if (bError)
        {
            ErrorDlg.V.SetError("Unexpected. {357CA513-03BB-4644-B2F5-9FF96CFCA656}");
            yield break;
        }

        m_state = ProcessState.RUNNING;
	}

    public Sprite GetSprite(string sprite_name)
    {
        var sp = m_sprite_list.Find(i=>i.name == sprite_name);
        return sp;
    }


}
