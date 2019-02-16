//  psggConverterLib.dll converted from test01Control.xlsx. 
public partial class test01Control : StateManager {

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()
    {
        return CheckState(S_END);
    }



    /*
        S_END
    */
    void S_END(bool bFirst)
    {
        if (bFirst)
        {
            accept_key_reset();
        }
        if (HasNextState())
        {
            GoNextState();
        }
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
            set_timeout(3);
            accept_key_reset();
            accept_forward_key();
        }
        if (!check_button_or_timeout_inidle()) return;
        br_WALK(S_WALK);
        br_NEXTIDLE(S_SET_NEXTIDLE);
        if (HasNextState())
        {
            GoNextState();
        }
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
            accept_key_reset();
        }
        if (!HasNextState())
        {
            SetNextState(S_IDLE);
        }
        if (HasNextState())
        {
            GoNextState();
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
            accept_key_reset();
        }
        if (!jump_is_done()) return;
        if (!HasNextState())
        {
            SetNextState(S_RUN);
        }
        if (HasNextState())
        {
            GoNextState();
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
            accept_key_reset();
        }
        if (!jump_is_done()) return;
        if (!HasNextState())
        {
            SetNextState(S_WALK);
        }
        if (HasNextState())
        {
            GoNextState();
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
            accept_key_reset();
            accept_forward_key();
            accept_jump_key();
        }
        if (!check_button_inrun()) return;
        br_JUMP(S_JUMP_IN_RUN);
        br_IDLE(S_SET_NEXTIDLE);
        if (HasNextState())
        {
            GoNextState();
        }
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
            accept_key_reset();
        }
        if (!HasNextState())
        {
            SetNextState(S_IDLE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        if (bFirst)
        {
            accept_key_reset();
        }
        if (!HasNextState())
        {
            SetNextState(S_INIT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
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
            set_timeout(2);
            accept_key_reset();
            accept_forward_key();
            accept_jump_key();
        }
        if (!check_jumpbutton_or_timeout_to_run_inwalk()) return;
        br_RUN(S_RUN);
        br_JUMP(S_JUMP_IN_WALK);
        br_IDLE(S_SET_NEXTIDLE);
        if (HasNextState())
        {
            GoNextState();
        }
    }

}

