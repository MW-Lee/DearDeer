////////////////////////////////////////////
//
// Boss_RangeCheck
//
// 보스가 가지고있는 범위 콜라이더에서 작동하는 스크립트
// 20. 11. 23
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_RangeCheck : MonoBehaviour
{
    #region 변수
    
    public Monster monster;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        monster.vDest = collision.transform.position;

        if (transform.parent.position.x > collision.transform.position.x)
            monster.iWhereisPlayer = -1;
        else
            monster.iWhereisPlayer = 1;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        monster.vDest = collision.transform.position;

        if (transform.parent.position.x > collision.transform.position.x)
            monster.iWhereisPlayer = -1;
        else
            monster.iWhereisPlayer = 1;
    }

    #endregion


    #region 실행

    private void Start()
    {
        monster = transform.GetComponentInParent<Monster>();
    }

    #endregion
}
