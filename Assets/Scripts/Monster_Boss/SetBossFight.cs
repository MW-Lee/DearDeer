////////////////////////////////////////////
//
// SetBossFight
//
// Boss 전 시작시 작동하는 스크립트
// 20. 12. 08
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBossFight : MonoBehaviour
{
    public Player _player;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.SetSavePoint(transform.parent);
        _player.bBossFight = true;
        gameObject.SetActive(false);
    }
}
