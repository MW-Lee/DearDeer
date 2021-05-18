////////////////////////////////////////////
//
// DialogWindow
//
// 대화창에서 작동하는 스크립트
// 20. 11. 27
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialogStruct
{
    public string sName;
    [TextArea(3, 5)]
    public string sText;
    public bool bQuiz;

    public DialogStruct(string _name, string _text, bool _quiz)
    {
        sName = _name;
        sText = _text;
        bQuiz = _quiz;
    }
}

public class DialogWindow : MonoBehaviour
{
    #region 변수

    public Player player;

    public GameObject gQuizWindow;
    public GameObject gPanel;
    public Text tName;
    public Text tText;

    public Queue<DialogStruct> sentences    = new Queue<DialogStruct>();

    public Quiz _quiz;

    [HideInInspector]
    public bool bQuiz = false;

    private GameObject gShowCut;
    private bool bIsShowcut = false;

    private InputManager m_input;
    private Coroutine _typing = null;
    private DialogStruct _sentence;
    private bool bTyping = false;

    #endregion


    #region 함수

    public void StartDialog()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        _sentence = sentences.Dequeue();
        if (_typing != null)
            StopCoroutine(_typing);
        _typing = StartCoroutine(TypeSentence());
    }

    public void EndDialog()
    {
        sentences.Clear();
        player.bDialog = false;

        if (bIsShowcut)
        {
            gShowCut.SetActive(false);
            player.bIsShowCut = false;
            player.gShowCut = null;

            bIsShowcut = false;
            gShowCut = null;
        }

        if (player._TalkNPC != null)
        {
            if (player._TalkNPC.gameObject.TryGetComponent<HaveSkillToGive>(out var t))
            {
                t.gGetSkill.SetActive(true);
            }

            player._TalkNPC = null;
        }

        gameObject.SetActive(false);
    }

    public IEnumerator TypeSentence()
    {
        tName.text = _sentence.sName;
        tText.text = string.Empty;
        bTyping = true;

        foreach(char letter in _sentence.sText.ToCharArray())
        {
            tText.text += letter;
            yield return null;
        }

        bTyping = false;
        if (_sentence.bQuiz)
        {
            bQuiz = true;
            gQuizWindow.SetActive(true);
        }
        yield return null;
    }

    #endregion


    #region 실행

    private void Start()
    {
        m_input = InputManager.GetInstance();
    }

    private void OnEnable()
    {
        tName.text = string.Empty;
        tText.text = string.Empty;

        _sentence = new DialogStruct();
        _quiz = player._quiz;

        sentences = player.qDialogSentences;

        bIsShowcut = player.bIsShowCut;
        gShowCut = player.gShowCut;

        StartDialog();
    }

    private void Update()
    {
        if (m_input.keyDown_LeftMouse && !bIsShowcut && !bQuiz)
        {
            if (!bTyping)
                StartDialog();
        }
    }

    #endregion
}
