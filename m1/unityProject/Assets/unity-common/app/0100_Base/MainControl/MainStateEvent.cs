using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainStateEventId {

    UNKNOWN,
    USER,

    BUTTON

}

public class MainStateEvent
{
    public MainStateEventId id;
    public string           name;
    public object           obj;

    public MainStateEvent(MainStateEventId iid, string iname, object iobj)
    {
        id   = iid;
        name = iname;
        obj  = iobj;
    }

    private static MainStateEvent m_cur = null;
    public static List<MainStateEvent> m_event_list = new List<MainStateEvent>();
    public static void Push(MainStateEventId id, string name=null, object obj = null)
    {
        m_event_list.Add( new MainStateEvent(id,name,obj) );
    }
    public static void Pop()
    {
        if (m_event_list.Count>0)
        {
            m_cur = m_event_list[0];
            m_event_list.RemoveAt(0);
        }
        else
        {
            m_cur = null;
        }

        if (m_cur!=null) Debug.Log(m_cur.id.ToString() + ":" +   m_cur.name);
    }
    public static MainStateEvent Cur() { return m_cur; }    
}

