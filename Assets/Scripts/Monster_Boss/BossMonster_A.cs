////////////////////////////////////////////
//
// BossMonster_A
//
// BossMoster_A에서 작동하는 스크립트
// 20. 11. 21
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_A : Monster
{
    #region 변수

    public StateMachine<BossMonster_A> FSM;

    public SpriteRenderer m_spriterenderer;
    public SpriteRenderer m_Effectrenderer;

    public int iOldDir;
    public bool bOnGround;
    public bool bDie = false;

    private Rigidbody2D m_rigidbody;

    // 보스 소지 오디오
    // 0 jump
    // 1 3Cut
    // 2 rush
    // 3 tel 1
    // 4 tel 2
    // 5 die
    public AudioSource _audioSource;
    public Sound _sound;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ihit++;

            if(ihit == iMaxHP)
            {
                // 사망
                bDie = true;
                FSM.SetState("die");
            }
        }
    }

    public override void ChangeState(string _input)
    {
        FSM.SetState(_input);
    }

    private void RefreshDirection()
    {
        iDir = iWhereisPlayer;

        if (!bAttack)
        {
            if (iDir == -1)
            {
                m_spriterenderer.flipX = true;
                m_Effectrenderer.flipX = true;
            }
            else if (iDir == 1)
            { 
                m_spriterenderer.flipX = false;
                m_Effectrenderer.flipX = false;
            }
        }
    }

    //
    // 공격 패턴을 위한 Animation Event 함수
    //
    public void Attack_3Cut()
    {
        m_rigidbody.velocity = Vector2.right * iOldDir * 20;
    }

    public void Pause_3Cut()
    {
        m_rigidbody.velocity = Vector2.zero;
    }

    public void Attack_Goup()
    {
        transform.Translate(Vector2.up * 6);
    }

    public void Attack_Rush()
    {
        m_rigidbody.AddForce(Vector2.right * iDir * 50, ForceMode2D.Impulse);
    }

    public void ChangeState_ani(string input)
    {
        FSM.SetState(input);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    #endregion


    #region 실행

    private void Start()
    {
        IState _move    = new BossAState_Move(this.gameObject);
        IState _attack  = new BossAState_Attack(this.gameObject);
        IState _jump    = new BossAState_Jump(this.gameObject);
        IState _fall    = new BossAState_Fall(this.gameObject);
        IState _3cut    = new BossAState_3Cut(this.gameObject);
        IState _goup    = new BossAState_Goup(this.gameObject);
        IState _crash   = new BossAState_Crash(this.gameObject);
        IState _rush    = new BossAState_Rush(this.gameObject);
        IState _atkend  = new BossAState_AttackEnd(this.gameObject);
        IState _die     = new BossAState_Die(this.gameObject);

        FSM = new StateMachine<BossMonster_A>(_move);

        FSM.AddState("move",    _move);
        FSM.AddState("attack",  _attack);
        FSM.AddState("jump",    _jump);
        FSM.AddState("fall",    _fall);
        FSM.AddState("3cut",    _3cut);
        FSM.AddState("goup",    _goup);
        FSM.AddState("crash",   _crash);
        FSM.AddState("rush",    _rush);
        FSM.AddState("atkend",  _atkend);
        FSM.AddState("die",     _die);

        m_rigidbody = GetComponent<Rigidbody2D>();

        _audioSource = GetComponent<AudioSource>();
        _sound = GetComponent<Sound>();
    }

    private void Update()
    {
        //Debug.Log(FSM.CurrentState);

        if (bDie) return;

        RefreshDirection();

        FSM.CurrentState.OperatorUpdate();
    }

    #endregion
}
