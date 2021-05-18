////////////////////////////////////////////
//
// QuizWindow
//
// 퀴즈창에서 작동하는 스크립트
// 20. 12. 07
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizWindow : MonoBehaviour
{
    #region 변수

    public DialogWindow _dialogWindow;
    public InputField _input;

    [HideInInspector]
    public string[] answer;

    private int iterator = 0;
    private int iAnswer = 0;


    #endregion


    #region 함수

    public void Btn_Click()
    {
        if(_dialogWindow.sentences.Count > 0)
        {
            if(_input.text == answer[iterator])
            {
                iAnswer++;
            }
            iterator++;

            _dialogWindow.StartDialog();
            _input.text = "";
        }
        else
        {
            if (_input.text == answer[iterator])
            {
                iAnswer++;
            }

            _dialogWindow.sentences.Clear();

            // 정답 입력
            if (iAnswer == answer.Length)
            {
                for (int i = 0; i < _dialogWindow._quiz.CorrectSentence.Length; i++)
                {
                    _dialogWindow.sentences.Enqueue(_dialogWindow._quiz.CorrectSentence[i]);

                    // 플레이어의 세이브포인트 갱신
                    _dialogWindow.player.SetSavePoint(_dialogWindow._quiz.TFSavepoint);                    
                }
            }
            // 오답 입력
            else
            {
                for (int i = 0; i < _dialogWindow._quiz.WrongSentence.Length; i++)
                {
                    _dialogWindow.sentences.Enqueue(_dialogWindow._quiz.WrongSentence[i]);
                }
            }

            EndQuiz();
        }
    }

    private void EndQuiz()
    {
        _dialogWindow.bQuiz = false;
        iAnswer = 0;
        iterator = 0;
        answer = null;
        _dialogWindow.StartDialog();
        gameObject.SetActive(false);
    }

    #endregion


    #region 실행

    private void OnEnable()
    {
        _input.text = "";
        answer = _dialogWindow._quiz.answer;
    }

    #endregion
}
