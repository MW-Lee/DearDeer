////////////////////////////////////////////
//
// MonsterBState_Idle
//
// Monster_B의 기본 상태에서 작동하는 스크립트
// 20. 10. 26
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBState_Idle : IState
{
    #region 변수

    private GameObject m_monsterGO;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public MonsterBState_Idle(GameObject _input)
    {
        m_monsterGO = _input;
    }

    public void OperatorEnter()
    {
        return;
    }

    public void OperatorUpdate()
    {
        return;
    }

    public void OperatorExit()
    {
        return;
    }

    #endregion
}
