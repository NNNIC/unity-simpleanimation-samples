using System;
using UnityEngine;

public partial class UITest01Control {
	
    Canvas m_target;
    Canvas m_template;

    GameObject m_parent;
    GameObject m_latest;
    RectTransform m_latest_rt;

    public void SetTargetAndTemplate(Canvas target, Canvas template)
    {
        m_target   = target;
        m_template = template;
    }

    public bool IsEnd()
    {
        return CheckState(S_END);
    }

    void setup()
    {
        m_parent = m_target.gameObject;
    }

    void create(string parts, string reff)
    {
        var clone = UGuiUtil.FindAndClone(m_template.transform,reff,m_parent);
        if (clone!=null)
        {
            clone.name = parts;
            m_latest = clone;
            m_latest_rt = m_latest.GetComponent<RectTransform>();
        }
        else
        {
            throw new SystemException("Unexpected! {E8337688-F0C2-4506-9DE0-2CD12C0698B9}");
        }
    }

    void set_pos(float x, float y) {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {B33DAE1A-FAA5-41EF-B45C-FEC61B0EA1DB}");
        UGuiUtil.SetPos(m_latest_rt,x,y);
    }

    void set_size(float w, float h) {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {27138646-8AC1-491B-9F0F-63EA9DC8AEEA}");
        UGuiUtil.SetSize(m_latest_rt,w,h);
    }

    void set_anchor(string a) {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {C1BAF5D5-5453-41C9-80D5-333D7F7FCD4A}");
        UGuiUtil.SetAnchor(m_latest_rt,a);
    }

    void set_pivot(string a) {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {FA1BA1D2-CACD-47B6-ABEF-1C227C53E956}");
        UGuiUtil.SetPivot(m_latest_rt,a);
    }

    void set_text(string s) {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {F8CC1FAE-9834-4556-A026-1CE44B4ABBFB}");
        UGuiUtil.SetText(m_latest_rt,s);
    }
    void set_text_clr() {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {B4631292-B2B1-494B-A498-B884AB8F706D}");
        UGuiUtil.SetText(m_latest_rt,"");
    }
    void set_sprite(string s)
    {
        if (m_latest_rt==null) throw new SystemException("Unexpected! {975567DB-3E02-4E63-AF66-49C07F7228D2}");
        var sprite = UISpriteManager.V.GetSprite(s);
        if (sprite!=null)
        {
            UGuiUtil.SetSprite(m_latest_rt,sprite);
        }
    }
    bool _get_anchorpos(string anchorstr, out float x, out float y)
    {
        x = 0f;
        y = 0f;
        var pos_or_null = EnumUtil.TryParse(typeof(UIANCHORPOS),anchorstr);
        if (pos_or_null == null) return false;
        var pos = (UIANCHORPOS)pos_or_null;

        if (pos == UIANCHORPOS.TL) { x = 0;        y = 1;    }
        if (pos == UIANCHORPOS.TC) { x = 0.5f;     y = 1;    }
        if (pos == UIANCHORPOS.TR) { x = 1;        y = 1;    }
                                                           
        if (pos == UIANCHORPOS.ML) { x = 0;        y = 0.5f; }
        if (pos == UIANCHORPOS.MC) { x = 0.5f;     y = 0.5f; }
        if (pos == UIANCHORPOS.MR) { x = 1;        y = 0.5f; }

        if (pos == UIANCHORPOS.BL) { x = 0;        y = 0f;   }
        if (pos == UIANCHORPOS.BC) { x = 0.5f;     y = 0f;   }
        if (pos == UIANCHORPOS.BR) { x = 1;        y = 0f;   }

        return true;
    }

    void set_parent()
    {
        m_parent = m_latest;
    }

}
