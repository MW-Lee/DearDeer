////////////////////////////////////////////
//
// BossAState_Attack
//
// BossA의 공격 패턴 설정 상태에서 작동하는 스크립트
// 20. 11. 22
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Attack : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;

    private float fDistance;
    private int iRandom;

    #endregion


    #region 함수

    private void CaculateDistance()
    {
        fDistance = Vector2.Distance(m_BossGO.transform.position, m_Boss.vDest);
    }

    #endregion


    #region 실행

    public BossAState_Attack(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();

        iRandom = 0;
    }

    public void OperatorEnter()
    {
        CaculateDistance();
        iRandom = Random.Range(1, 100);
    }

    public void OperatorUpdate()
    {
        if (m_Boss.bDie)
        {
            m_Boss.FSM.SetState("die");
            return;
        }

        // 근거리 패턴
        if (fDistance < 15f)
        {
            if (iRandom <= 20)
            {
                // 내려찍기
                m_Boss.FSM.SetState("goup");
                return;
            }
            else if (iRandom > 20 && iRandom <= 45)
            {
                // 점프
                m_Boss.FSM.SetState("jump");
                return;
            }
            else if (iRandom > 45 && iRandom <= 65)
            {
                // 돌진
                m_Boss.FSM.SetState("rush");
                return;
            }
            else if (iRandom > 65)
            {
                // 삼연격
                m_Boss.FSM.SetState("3cut");
                return;
            }
        }
        // 원거리 패턴
        else if(fDistance >= 15f)
        {
            if (iRandom <= 25)
            {
                // 원거리
                m_Boss.FSM.SetState("move");
                return;
            }
            else if (iRandom > 25 && iRandom <= 45)
            {
                // 내려찍기
                m_Boss.FSM.SetState("goup");
                return;
            }
            else if (iRandom > 45 && iRandom <= 70)
            {
                // 점프
                m_Boss.FSM.SetState("jump");
                return;
            }
            else if (iRandom > 70)
            {
                // 돌진
                m_Boss.FSM.SetState("rush");
                return;
            }
        }

        
    }

    public void OperatorExit()
    {
        fDistance = 0;
        iRandom = 0;
    }

    #endregion
}
