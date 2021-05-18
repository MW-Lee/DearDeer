////////////////////////////////////////////
//
// Monster_RangeCheck
//
// 몬스터가 가지고있는 범위 콜라이더에서 작동하는 스크립트
// 20. 10. 23
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    #region 변수

    public Monster monster;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.bAttack = true;
            monster.vDest = collision.transform.position;

            if (transform.parent.position.x > collision.transform.position.x)
                monster.iWhereisPlayer = -1;
            else
                monster.iWhereisPlayer = 1;


            monster.ChangeState("attack");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.vDest = collision.transform.position;

            if (transform.parent.position.x > collision.transform.position.x)
                monster.iWhereisPlayer = -1;
            else
                monster.iWhereisPlayer = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.bAttack = false;
            monster.vDest = Vector2.zero;

            monster.ChangeState("idle");
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        monster = transform.GetComponentInParent<Monster>();
    }

    #endregion
}
