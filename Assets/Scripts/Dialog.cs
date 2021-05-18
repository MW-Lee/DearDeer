////////////////////////////////////////////
//
// Dialog
//
// 대화창에서 표시할 텍스트 내용을 담는 스크립트
// 20. 11. 27
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    #region 변수

    public DialogStruct[] InputSentence;
    public DialogStruct[] AlwaysSentence;

    public Queue<DialogStruct> qSentences = new Queue<DialogStruct>();

    #endregion

    #region 함수

    public void ChangeSentence()
    {
        qSentences.Clear();
        for(int i = 0; i < AlwaysSentence.Length; i++)
        {
            qSentences.Enqueue(AlwaysSentence[i]);
        }
    }

    #endregion

    #region 실행

    private void Start()
    {
        for(int i = 0; i < InputSentence.Length; i++)
        {
            qSentences.Enqueue(InputSentence[i]);
        }
    }

    #endregion
}
