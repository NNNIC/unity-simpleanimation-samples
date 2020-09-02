using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class test04Control : MonoBehaviour {
 
    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    void _update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    void _start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }

	#region    // [PSGG OUTPUT START] indent(4) $/./$
    //             psggConverterLib.dll converted from psgg-file:test04Control.psgg

    /*
        E_0001
    */
    bool   m_anykey;
    string m_aim = null;
    int    m_loopcnt = 0;
    /*
        S_0000
    */
    void S_0000(bool bFirst)
    {
        if (bFirst)
        {
            m_anykey = false;
        }
        //
        if (!HasNextState())
        {
            Goto(S_UPCAM);
        }
    }
    /*
        S_BACKTO_WAIT
    */
    void S_BACKTO_WAIT(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_UPCAM);
        }
    }
    /*
        S_CAM_DOWN
    */
    void S_CAM_DOWN(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("down");
        }
        //
        if (!HasNextState())
        {
            Goto(S_POSE38);
        }
    }
    /*
        S_CAM_DOWN1
    */
    void S_CAM_DOWN1(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("down2");
        }
        //
        if (!HasNextState())
        {
            Goto(S_POSE39);
        }
    }
    /*
        S_CAM_MAIN
    */
    void S_CAM_MAIN(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("main");
        }
        //
        if (!HasNextState())
        {
            Goto(S_JUMP02);
        }
    }
    /*
        S_CAM_UP
    */
    void S_CAM_UP(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("up");
        }
        //
        if (!HasNextState())
        {
            Goto(S_RET000);
        }
    }
    /*
        S_CAM_UP1
    */
    void S_CAM_UP1(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("up");
        }
        //
        if (!HasNextState())
        {
            Goto(S_GSB002);
        }
    }
    /*
        S_DAMAGED00
    */
    void S_DAMAGED00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="DAMEGED00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_DAMAGED01
    */
    void S_DAMAGED01(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="DAMEGED01";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_GSB000
    */
    void S_GSB000(bool bFirst)
    {
        GoSubState(S_SBS000,S_GSB001);
        NoWait();
    }
    /*
        S_GSB001
    */
    void S_GSB001(bool bFirst)
    {
        GoSubState(S_SBS000,S_POSE44);
        NoWait();
    }
    /*
        S_GSB002
    */
    void S_GSB002(bool bFirst)
    {
        GoSubState(S_SBS000,S_JUMP03);
        NoWait();
    }
    /*
        S_GSB003
    */
    void S_GSB003(bool bFirst)
    {
        var n = RandomUtil.GetInt(2);
        // branch
        if (n == 0) { Goto( S_POSE36 ); }
        else if (n == 1) { Goto( S_POSE35 ); }
        else { Goto( S_HANDUP00_R1 ); }
    }
    /*
        S_HANDUP00_R
    */
    void S_HANDUP00_R(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="HANDUP00_R";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_HANDUP00_R1
    */
    void S_HANDUP00_R1(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="HANDUP00_R";
            pto(m_aim,8,0.7f);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_CAM_UP);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_JUMP00
    */
    void S_JUMP00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="JUMP00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_JUMP00B
    */
    void S_JUMP00B(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="DAMEGED00B";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_JUMP01
    */
    void S_JUMP01(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="JUMP01";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_JUMP01B
    */
    void S_JUMP01B(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="JUMP01B";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_JUMP02
    */
    void S_JUMP02(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="JUMP00";
            pto(m_aim,40);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_GSB003);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_JUMP03
    */
    void S_JUMP03(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="JUMP00";
            pto(m_aim,40);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_CAM_DOWN);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_LOOP
        Loop 10 times
    */
    int m_S_LOOP;
    int m_S_LOOP_max;
    void S_LOOP(bool bFirst)
    {
        m_S_LOOP=0;
        m_S_LOOP_max = m_loopcnt;
        Goto(S_LOOP_LoopCheckAndGosub____);
        NoWait();
    }
    void S_LOOP_LoopCheckAndGosub____(bool bFirst)
    {
        if (m_S_LOOP < m_S_LOOP_max) GoSubState(S_SUBSTART,S_LOOP_LoopNext____);
        else               Goto(S_CAM_MAIN);
        NoWait();
    }
    void S_LOOP_LoopNext____(bool bFirst)
    {
        m_S_LOOP++;
        Goto(S_LOOP_LoopCheckAndGosub____);
        NoWait();
    }
    /*
        S_LOOP2
        Loop X times
    */
    int m_S_LOOP2;
    int m_S_LOOP2_max;
    void S_LOOP2(bool bFirst)
    {
        m_S_LOOP2=0;
        m_S_LOOP2_max = m_loopcnt;
        Goto(S_LOOP2_LoopCheckAndGosub____);
        NoWait();
    }
    void S_LOOP2_LoopCheckAndGosub____(bool bFirst)
    {
        if (m_S_LOOP2 < m_S_LOOP2_max) GoSubState(S_SBS004,S_LOOP2_LoopNext____);
        else               Goto(S_CAM_UP1);
        NoWait();
    }
    void S_LOOP2_LoopNext____(bool bFirst)
    {
        m_S_LOOP2++;
        Goto(S_LOOP2_LoopCheckAndGosub____);
        NoWait();
    }
    /*
        S_LOSE00
    */
    void S_LOSE00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="LOSE00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE01
    */
    void S_POSE01(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE01";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE02
    */
    void S_POSE02(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE02";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE03
    */
    void S_POSE03(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE03";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE04
    */
    void S_POSE04(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE04";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE05
    */
    void S_POSE05(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE05";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE06
    */
    void S_POSE06(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE06";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE07
    */
    void S_POSE07(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE07";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE08
    */
    void S_POSE08(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE08";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE09
    */
    void S_POSE09(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE09";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE10
    */
    void S_POSE10(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE10";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE11
    */
    void S_POSE11(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE11";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE12
    */
    void S_POSE12(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE12";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE13
    */
    void S_POSE13(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE13";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE14
    */
    void S_POSE14(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE14";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE15
    */
    void S_POSE15(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE15";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE16
    */
    void S_POSE16(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE16";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE17
    */
    void S_POSE17(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE17";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE18
    */
    void S_POSE18(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE18";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE19
    */
    void S_POSE19(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE19";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE20
    */
    void S_POSE20(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE20";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE21
    */
    void S_POSE21(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE21";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE22
    */
    void S_POSE22(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE22";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE23
    */
    void S_POSE23(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE23";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE24
    */
    void S_POSE24(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE24";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE25
    */
    void S_POSE25(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE25";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE26
    */
    void S_POSE26(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE26";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE27
    */
    void S_POSE27(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE27";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE28
    */
    void S_POSE28(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE28";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE29
    */
    void S_POSE29(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE29";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE30
    */
    void S_POSE30(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE30";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE31
    */
    void S_POSE31(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE31";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE32
    */
    void S_POSE32(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE04";
            pto(m_aim,12);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_POSE33);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE33
    */
    void S_POSE33(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE09";
            pto(m_aim,12);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_RET004);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE34
    */
    void S_POSE34(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE03";
            pto(m_aim,12);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_POSE32);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE35
    */
    void S_POSE35(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE07";
            pto(m_aim,18,0.7f);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_CAM_UP);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE36
    */
    void S_POSE36(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE08";
            pto(m_aim,18,0.7f);
        }
        //
        if (!HasNextState())
        {
            Goto(S_CAM_UP);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE37
    */
    void S_POSE37(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE17";
            pto(m_aim,30);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_BACKTO_WAIT);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE38
    */
    void S_POSE38(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE19";
            pto(m_aim,300);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_CAM_DOWN1);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE39
    */
    void S_POSE39(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE27";
            pto(m_aim,300);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_BACKTO_WAIT);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE40
    */
    void S_POSE40(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE28";
            pto(m_aim,16);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_POSE42);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE41
    */
    void S_POSE41(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE30";
            pto(m_aim,30);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_RET003);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE42
    */
    void S_POSE42(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE29";
            pto(m_aim,16);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_POSE41);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE43
    */
    void S_POSE43(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE31";
            pto(m_aim,2);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_SETLOOP2);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE44
    */
    void S_POSE44(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE02";
            pto(m_aim,10);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_POSE45);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_POSE45
    */
    void S_POSE45(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="POSE03";
            pto(m_aim,10);
        }
        if (!is_to()) return;
        //
        if (!HasNextState())
        {
            Goto(S_POSE43);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_REFRESH00
    */
    void S_REFRESH00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="REFRESH00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_RET000
    */
    void S_RET000(bool bFirst)
    {
        ReturnState();
        NoWait();
    }
    /*
        S_RET003
    */
    void S_RET003(bool bFirst)
    {
        ReturnState();
        NoWait();
    }
    /*
        S_RET004
    */
    void S_RET004(bool bFirst)
    {
        ReturnState();
        NoWait();
    }
    /*
        S_RUN00_F
    */
    void S_RUN00_F(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="RUN00_F";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_RUN00_L
    */
    void S_RUN00_L(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="RUN00_L";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_RUN00_R
    */
    void S_RUN00_R(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="RUN00_R";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_SBS000
    */
    void S_SBS000(bool bFirst)
    {
        Goto(S_SETLOOP3);
        NoWait();
    }
    /*
        S_SBS004
    */
    void S_SBS004(bool bFirst)
    {
        Goto(S_POSE40);
        NoWait();
    }
    /*
        S_SETLOOP
    */
    void S_SETLOOP(bool bFirst)
    {
        if (bFirst)
        {
            m_loopcnt = 5;
        }
        //
        if (!HasNextState())
        {
            Goto(S_GSB000);
        }
    }
    /*
        S_SETLOOP1
    */
    void S_SETLOOP1(bool bFirst)
    {
        if (bFirst)
        {
            m_loopcnt = 5;
        }
        //
        if (!HasNextState())
        {
            Goto(S_LOOP2);
        }
    }
    /*
        S_SETLOOP2
    */
    void S_SETLOOP2(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("left");
        }
        //
        if (!HasNextState())
        {
            Goto(S_SETLOOP1);
        }
    }
    /*
        S_SETLOOP3
    */
    void S_SETLOOP3(bool bFirst)
    {
        if (bFirst)
        {
            m_loopcnt = 5;
        }
        //
        if (!HasNextState())
        {
            Goto(S_LOOP);
        }
    }
    /*
        S_SLIDE00
    */
    void S_SLIDE00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="SLIDE00";
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        Goto(S_0000);
        NoWait();
    }
    /*
        S_SUBSTART
    */
    void S_SUBSTART(bool bFirst)
    {
        Goto(S_POSE34);
        NoWait();
    }
    /*
        S_UMATOBI00
    */
    void S_UMATOBI00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="UMATOBI00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_UPCAM
    */
    void S_UPCAM(bool bFirst)
    {
        if (bFirst)
        {
            set_cam("up");
        }
        //
        if (!HasNextState())
        {
            Goto(S_WAIT05);
        }
    }
    /*
        S_WAIT00
    */
    void S_WAIT00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WAIT01
    */
    void S_WAIT01(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT01";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WAIT02
    */
    void S_WAIT02(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT02";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WAIT03
    */
    void S_WAIT03(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT03";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WAIT04
    */
    void S_WAIT04(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT04";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WAIT05
    */
    void S_WAIT05(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT00";
            pto(m_aim,200);
        }
        if (!is_tok()) return;
        // branch
        if (!m_anykey) { Goto( S_WAIT06 ); }
        else { Goto( S_SETLOOP ); }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WAIT06
    */
    void S_WAIT06(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WAIT01";
            pto(m_aim,400);
        }
        if (!is_tok()) return;
        // branch
        if (!m_anykey) { Goto( S_BACKTO_WAIT ); }
        else { Goto( S_SETLOOP ); }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WALK00_B
    */
    void S_WALK00_B(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WALK00_B";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WALK00_F
    */
    void S_WALK00_F(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WALK00_F";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WALK00_L
    */
    void S_WALK00_L(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WALK00_L";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WALK00_R
    */
    void S_WALK00_R(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WALK00_R";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_WIN00
    */
    void S_WIN00(bool bFirst)
    {
        if (bFirst)
        {
            m_aim="WIN00";
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }


    #endregion // [PSGG OUTPUT END]

    SimpleAnimation m_sa;
    int m_cnt;
    
    // write your code below
    void pto(string anim, int f, float y = 0)
    {
        transform.position = Vector3.up * y;
        transform.eulerAngles = Vector3.up * 180;
        if (m_sa == null) m_sa = GetComponent<SimpleAnimation>();
        var st = m_sa.GetState(anim);
        if (st != null)
        {
            st.normalizedTime = 0;
        }
        else
        {
            Debug.Log(anim + "!!!!!!!!!!!!");
        }
        m_sa.CrossFade(anim,3f/15f);
        m_cnt = f;
    }
    bool is_to()
    {
        m_cnt -= 1;
        if (m_cnt <= 0) return true;
        return false;
    }
    bool is_tok()
    {
        m_cnt -= 1;
        m_anykey = Input.anyKey;
        if (m_anykey) return true;
        if (m_cnt <= 0) return true;
        return false;
    }

    GameObject m_camroot;
    void set_cam(string camname)
    {
        if (m_camroot == null) m_camroot = GameObject.Find("camroot");
        var camo = HierarchyUtility.FindGameObject(m_camroot.transform,"Camera_"+camname);
        var dstcam = camo.GetComponent<Camera>();
        StartCoroutine(set_cam_co(dstcam, 30));
    }
    IEnumerator set_cam_co(Camera dstcam, int f)
    {
        var cam = Camera.main;
        var srcpos = cam.transform.position;
        var srcrot = cam.transform.rotation;
        var srcfov = cam.fieldOfView;
        for (var n = 0; n < f; n++)
        {
            var r = (float)n / f;
            cam.transform.position = Vector3.Lerp(srcpos, dstcam.transform.position, r);
            cam.transform.rotation = Quaternion.Lerp(srcrot, dstcam.transform.rotation, r);
            cam.fieldOfView = Mathf.Lerp(srcfov, dstcam.fieldOfView,r);
            yield return null;
        }
        cam.transform.position = dstcam.transform.position;
        cam.transform.rotation = dstcam.transform.rotation;
        cam.fieldOfView = dstcam.fieldOfView;
    }


    #region Monobehaviour framework
    void Start()
    {
        _start();
    }
    void Update()
    {
        if (!IsEnd()) 
        {
            _update();
        }
    }
    #endregion
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

