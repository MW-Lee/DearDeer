////////////////////////////////////////////
//
// Wind
//
// 바람에서 작동하는 스크립트
// 20. 12. 06
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    #region 변수

    private Vector2 vWindDir;
    private int iWindPower;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().vWindDir = vWindDir * iWindPower;
        }
        else if (collision.CompareTag("PuzzleObject"))
        {
            transform.Translate(Vector2.down * 2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(vWindDir, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().vWindDir = Vector2.zero;
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        vWindDir = transform.parent.GetComponent<WindArea>().vWindDir;
        iWindPower = transform.parent.GetComponent<WindArea>().iWindPower;
    }

    #endregion
}
