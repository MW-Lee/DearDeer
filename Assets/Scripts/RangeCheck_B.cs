////////////////////////////////////////////
//
// RangeCheck_B
//
// 벌이 가지고있는 범위 콜라이더에서 작동하는 스크립트
// 20. 10. 26
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck_B : MonoBehaviour
{
    #region 변수

    public Monster_B monster;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.vDest = collision.transform.position;
            monster.bAttack = true;            

            //monster.FSM.SetState("attack");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.vDest = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.bAttack = false;
            monster.vDest = Vector2.zero;

            //monster.FSM.SetState("idle");
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        monster = transform.GetComponentInParent<Monster_B>();
    }

    #endregion
}
