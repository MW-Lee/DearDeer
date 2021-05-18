////////////////////////////////////////////
//
// Monster_A
//
// Monster_A에서 작동하는 스크립트
// 20. 10. 18
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_A : Monster
{
    #region 변수

    public StateMachine<Monster_A> FSM;

    [HideInInspector]
    //public float fDest;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ihit++;

            if (ihit == iMaxHP)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public override void ChangeState(string _input)
    {
        FSM.SetState(_input);
    }

    #endregion


    #region 실행

    private void Start()
    {
        IState _idle = new MonsterAState_Idle(this.gameObject);
        IState _move = new MonsterAState_Move(this.gameObject);
        IState _atk = new MonsterAState_Attack(this.gameObject);

        FSM = new StateMachine<Monster_A>(_idle);

        FSM.AddState("idle", _idle);
        FSM.AddState("move", _move);
        FSM.AddState("attack", _atk);

        // 상태 외 기본 변수들 초기화
        _animator = GetComponent<Animator>();

        iWhereisPlayer = 0;
        bAttack = false;

        ihit = 0;
    }

    private void Update()
    {
        FSM.CurrentState.OperatorUpdate();
    }

    #endregion
}
