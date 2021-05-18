////////////////////////////////////////////
//
// Player_InteractionCheck
//
// 상호작용을 위한 Collider에서 작동하는 스크립트
// 20. 11. 19
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_InteractionCheck : MonoBehaviour
{
    #region 변수

    public GameObject gDialogWindow;

    private Transform parentTF;
    private InputManager _input;
    private Collider2D _currentCollision;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShowCut"))
        {
            // 컷 재생
            Player temp = parentTF.GetComponent<Player>();
            temp.qDialogSentences = new Queue<DialogStruct>(collision.GetComponent<Dialog>().qSentences);
            temp.bIsShowCut = true;
            temp.gShowCut = collision.gameObject;

            Player.FSM.SetState("idle");
            collision.GetComponent<PlayableDirector>().Play();
            parentTF.GetComponent<Player>().bDialog = true;

            return;
        }

        transform.parent.Find("Attack").GetComponent<AttackPoint>()._animator.SetBool("Interaction", true);

        if (_currentCollision == null)
            _currentCollision = collision;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_currentCollision == collision || collision.CompareTag("ShowCut"))
            return;

        float distance1 = Vector2.Distance(parentTF.position, _currentCollision.transform.position);
        float distance2 = Vector2.Distance(parentTF.position, collision.transform.position);

        if(distance1 > distance2)
        {
            _currentCollision = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.Find("Attack").GetComponent<AttackPoint>()._animator.SetBool("Interaction", false);
        _currentCollision = null;
    }

    #endregion


    #region 실행

    private void Start()
    {
        parentTF = transform.parent;
        _input = InputManager.GetInstance();
    }

    private void Update()
    {
        if (_input.keyDown_F && _currentCollision != null)
        {
            switch (_currentCollision.tag)
            {
                case "NPC":
                    if(_currentCollision.TryGetComponent<Quiz>(out var t))
                    {
                        parentTF.GetComponent<Player>()._quiz = t;
                    }
                    parentTF.GetComponent<Player>()._TalkNPC = _currentCollision.GetComponent<NPC>();
                    parentTF.GetComponent<Player>().bDialog = true;
                    parentTF.GetComponent<Player>().qDialogSentences =
                        new Queue<DialogStruct>(_currentCollision.GetComponent<Dialog>().qSentences);
                    if (_currentCollision.GetComponent<Dialog>().AlwaysSentence.Length != 0)
                        _currentCollision.GetComponent<Dialog>().ChangeSentence();
                    gDialogWindow.SetActive(true);
                    return;

                case "Object":
                    // 거울스킬
                    
                    return;

                case "Scaffold":

                    return;
            }
        }
    }

    #endregion
}
