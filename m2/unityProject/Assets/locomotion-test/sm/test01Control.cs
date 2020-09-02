using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class test01Control : MonoBehaviour {
 
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
    //             psggConverterLib.dll converted from psgg-file:test01Control.psgg

    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_IDLE
        アイドル
    */
    void S_IDLE(bool bFirst)
    {
        if (bFirst)
        {
            idle_kick();
        }
        if (!check_button_or_timeout_inidle()) return;
        // branch
        if (m_checkresult == CheckResult.WALK) { Goto( S_WALK ); }
        else if (m_checkresult == CheckResult.NEXTIDLE) { Goto( S_SET_NEXTIDLE ); }
    }
    /*
        S_INIT
        初期化
    */
    void S_INIT(bool bFirst)
    {
        if (bFirst)
        {
            init();
        }
        //
        if (!HasNextState())
        {
            Goto(S_IDLE);
        }
    }
    /*
        S_JUMP_IN_RUN
        jump in run
    */
    void S_JUMP_IN_RUN(bool bFirst)
    {
        if (bFirst)
        {
            jump_kick();
        }
        if (!jump_is_done()) return;
        //
        if (!HasNextState())
        {
            Goto(S_RUN);
        }
    }
    /*
        S_JUMP_IN_WALK
        jump in walk
    */
    void S_JUMP_IN_WALK(bool bFirst)
    {
        if (bFirst)
        {
            jump_kick();
        }
        if (!jump_is_done()) return;
        //
        if (!HasNextState())
        {
            Goto(S_WALK);
        }
    }
    /*
        S_RUN
        走る
    */
    void S_RUN(bool bFirst)
    {
        if (bFirst)
        {
            run_kick();
        }
        if (!check_button_inrun()) return;
        // branch
        if (m_checkresult == CheckResult.JUMP) { Goto( S_JUMP_IN_RUN ); }
        else if (m_checkresult == CheckResult.IDLE) { Goto( S_SET_NEXTIDLE ); }
    }
    /*
        S_SET_NEXTIDLE
        アイドルを変更
    */
    void S_SET_NEXTIDLE(bool bFirst)
    {
        if (bFirst)
        {
            set_next_idle();
        }
        //
        if (!HasNextState())
        {
            Goto(S_IDLE);
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        Goto(S_INIT);
        NoWait();
    }
    /*
        S_WALK
        Walk
    */
    void S_WALK(bool bFirst)
    {
        if (bFirst)
        {
            walk_kick();
        }
        if (!check_jumpbutton_or_timeout_to_run_inwalk()) return;
        // branch
        if (m_checkresult == CheckResult.RUN) { Goto( S_RUN ); }
        else if (m_checkresult == CheckResult.JUMP) { Goto( S_JUMP_IN_WALK ); }
        else if (m_checkresult == CheckResult.IDLE) { Goto( S_SET_NEXTIDLE ); }
    }


	#endregion // [PSGG OUTPUT END]

	// write your code below
	#region timeout
	float m_timeout;
	void set_timeout(float t)
	{
		m_timeout = Time.time + t;
	}
	#endregion

	#region key
	bool m_accept_key_forward=true;
	bool m_accept_key_jump=true;

	void accept_key_reset()
	{
		m_accept_key_forward = false;
		m_accept_key_jump    = false;
	}
	void accept_forward_key()
	{
		m_accept_key_forward = true;
	}
	void accept_jump_key()
	{
		m_accept_key_jump = true;
	}
	#endregion

	#region check
	public enum CheckResult
	{
		UNKNOWN,
		TIMEOUT,
		IDLE,
		NEXTIDLE,
		FORWARD,
		RUN,
		WALK,
		JUMP,
		NOKEY
	}
	CheckResult m_checkresult;
	bool check_button_or_timeout_inidle()
	{
		m_checkresult = CheckResult.UNKNOWN;

		if (Time.time > m_timeout)  {m_checkresult = CheckResult.NEXTIDLE;  return true;}

		var btn = _check_button();
		if (btn == CheckResult.FORWARD) 
		{
			m_checkresult = CheckResult.WALK;
			return true;
		}

		return false;
	}
	bool check_jumpbutton_or_timeout_to_run_inwalk()
	{
		if (m_accept_key_jump && Input.GetKey(KeyCode.J))
		{
			m_checkresult = CheckResult.JUMP;
			return true;
		}

		if (m_accept_key_forward && Input.GetKey(KeyCode.F)) 
		{
			if (Time.time>m_timeout) { m_checkresult = CheckResult.RUN; return true; }
		}
		else
		{
			m_checkresult = CheckResult.IDLE;
			return true;
		}
		return false;
	}
	bool check_button_inrun()
	{
		if (m_accept_key_jump && Input.GetKey(KeyCode.J))
		{
			m_checkresult = CheckResult.JUMP;
			return true;
		}
		if (m_accept_key_forward && Input.GetKey(KeyCode.F)) 
		{
			return false;
		}
		m_checkresult = CheckResult.IDLE;
		return true;
	}
	CheckResult _check_button()
	{
		if  (
				m_accept_key_jump && Input.GetKey(KeyCode.J)
			) { return CheckResult.JUMP;   }
		if  (
				m_accept_key_forward &&  Input.GetKey(KeyCode.F)
			) { return CheckResult.FORWARD;}
		return CheckResult.UNKNOWN;	
	}
	#endregion

	#region br
	//void br_WALK(Action<bool> st)
	//{
	//	if (m_checkresult == CheckResult.WALK) Goto(st);
	//}
	//void br_NEXTIDLE(Action<bool> st)
	//{
	//	if (m_checkresult == CheckResult.NEXTIDLE) Goto(st);
	//}
	//void br_RUN(Action<bool> st)
	//{
	//	if (m_checkresult == CheckResult.RUN) Goto(st);
	//}
	//void br_JUMP(Action<bool> st)
	//{
	//	if (m_checkresult == CheckResult.JUMP) Goto(st);
	//}
	//void br_IDLE(Action<bool> st)
	//{
	//	if (m_checkresult == CheckResult.IDLE) Goto(st);
	//}
	#endregion

	#region others
	int m_idle_num = 0;
	void init()
	{
		m_idle_num = 0;
        set_timeout(1);
	}
	void idle_kick()
	{
		var posenum = m_idle_num + 1;
		var idlestate = "POSE" + posenum.ToString("00");
		Debug.Log(idlestate);
		test3.V.Kick(idlestate);
        set_timeout(0.25f);
	}
	void set_next_idle()
	{
		m_idle_num++;
		m_idle_num = m_idle_num % 31;
	}
	void jump_kick()
	{
		test3.V.Kick("JUMP00");
	}
	bool jump_is_done()
	{
		return test3.V.IsDone();
	}
	void walk_kick()
	{
		test3.V.Kick("WALK00_F");
	}
	void run_kick()
	{
		test3.V.Kick("RUN00_F");
	}
	#endregion

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

