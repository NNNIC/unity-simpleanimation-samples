using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UITest01Controlをラップしてコンポネント化
public class UITest01ControlCompo : UIControlCompo {

    public UITest01Control m_sm;

    public Canvas          m_target;
    public Canvas          m_template;

    private void Start()
    {
        m_sm = new UITest01Control();
    }

    private void Update()
    {
        if (m_sm!=null) m_sm.update();
    }

    public override void SetTarget_TemplateAndStart()
    {
        m_sm.SetTargetAndTemplate(m_target,m_template);
        m_sm.Start();
    }

    public override bool IsEnd()
    {
        return m_sm.IsEnd();
    }
}
