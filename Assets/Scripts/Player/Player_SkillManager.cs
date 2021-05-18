////////////////////////////////////////////
//
// Player_SkillManager
//
// 플레이어가 스킬 사용시 나오는 이펙터에서 작동하는 스크립트
// 20. 11. 15
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    #region 변수

    public Animator _animator_skill;

    #endregion


    #region 함수

    public void EndOfSkill()
    {
        _animator_skill.SetInteger("Skill", 0);
    }

    #endregion


    #region 실행

    private void Start()
    {
        _animator_skill = GetComponent<Animator>();
    }

    #endregion
}
