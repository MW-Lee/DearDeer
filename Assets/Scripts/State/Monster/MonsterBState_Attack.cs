using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBState_Attack : IState
{
    #region 변수

    private GameObject m_monsterGO;
    private Monster_B m_monster;
    private Rigidbody2D m_rigidbody;

    private Vector3 vDest;

    #endregion


    #region 실행

    public MonsterBState_Attack(GameObject _input)
    {
        m_monsterGO = _input;
        m_monster = m_monsterGO.GetComponent<Monster_B>();
        m_rigidbody = m_monsterGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        vDest = m_monster.vDest;
    }

    public void OperatorUpdate()
    {
        vDest = m_monster.vDest;

        if (m_monster.bAttack)
            m_monster.transform.Translate((vDest - m_monster.transform.position).normalized * 0.05f);
    }

    public void OperatorExit()
    {
        return;
    }

    #endregion
}
