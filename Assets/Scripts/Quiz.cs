////////////////////////////////////////////
//
// Quiz
//
// 퀴즈창에서 작동하는 스크립트
// 20. 12. 07
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    #region 변수

    public string[] answer;
    public Transform TFSavepoint;
    public DialogStruct[] CorrectSentence;
    public DialogStruct[] WrongSentence;

    #endregion


    #region 함수

    #endregion


    #region 실행

    private void Start()
    {
        TFSavepoint = transform.parent;
    }

    #endregion
}
