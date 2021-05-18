////////////////////////////////////////////
//
// Monster_B
//
// Monster_B에서 작동하는 스크립트
// 20. 10. 26
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_B : Monster
{
    #region 변수

    public StateMachine<Monster_B> FSM;

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
        IState _idle = new MonsterBState_Idle(this.gameObject);
        IState _atk = new MonsterBState_Attack(this.gameObject);

        FSM = new StateMachine<Monster_B>(_idle);

        FSM.AddState("idle", _idle);
        //FSM.AddState("move", _move);
        FSM.AddState("attack", _atk);

        // 상태 외 기본 변수들 초기화
        _animator = GetComponent<Animator>();

        vDest = this.transform.position;
        iDir = 1;
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
