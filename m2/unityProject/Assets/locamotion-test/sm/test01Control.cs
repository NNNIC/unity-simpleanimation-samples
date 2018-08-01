using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class test01Control  {
		
	
	
	

	#region timeout
	float m_timeout;
	void set_timeout(float t)
	{
		m_timeout = Time.time + t;
	}
	#endregion

	#region key
	bool m_accept_key_forward;
	bool m_accept_key_jump;

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
	void br_WALK(Action<bool> st)
	{
		if (m_checkresult == CheckResult.WALK) SetNextState(st);
	}
	void br_NEXTIDLE(Action<bool> st)
	{
		if (m_checkresult == CheckResult.NEXTIDLE) SetNextState(st);
	}
	void br_RUN(Action<bool> st)
	{
		if (m_checkresult == CheckResult.RUN) SetNextState(st);
	}
	void br_JUMP(Action<bool> st)
	{
		if (m_checkresult == CheckResult.JUMP) SetNextState(st);
	}
	void br_IDLE(Action<bool> st)
	{
		if (m_checkresult == CheckResult.IDLE) SetNextState(st);
	}
	#endregion

	#region others
	int m_idle_num = 0;
	void init()
	{
		m_idle_num = 0;
	}
	void idle_kick()
	{
		var posenum = m_idle_num + 1;
		var idlestate = "POSE" + posenum.ToString("00");
		Debug.Log(idlestate);
		test3.V.Kick(idlestate);
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
}
