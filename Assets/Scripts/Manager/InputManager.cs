////////////////////////////////////////////
//
// InputManager
//
// 입력과 관련된 모든 작업을 총괄하는 스크립트
// 20. 10. 02
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region 변수

    // 다른 스크립트에서 입력을 불러다 사용할 instance
    private static InputManager instance;

    // GetKey
    public bool key_W;
    public bool key_A;
    public bool key_S;
    public bool key_D;
    //public bool key_E;
    public bool key_Space;
    public bool key_LeftControl;
    public bool key_LeftMouse;

    // GetKeyDown
    public bool keyDown_W;
    public bool keyDown_A;
    public bool keyDown_S;
    public bool keyDown_D;
    public bool keyDown_E;
    public bool keyDown_F;
    public bool keyDown_Space;
    public bool keyDown_LeftControl;
    public bool keyDown_LeftMouse;

    // GetKeyUp
    //public bool keyUp_W;
    public bool keyUp_A;
    public bool keyUp_S;
    public bool keyUp_D;
    public bool keyUp_Space;
    //public bool keyUp_LeftControl;
    //public bool keyUp_LeftMouse;

    // ETC
    public bool keyDouble_A;
    public bool keyDouble_D;

    #endregion


    #region 함수

    /// <summary>
    /// 다른 스크립트에서 사용하기 위한 instance 반환 함수
    /// </summary>
    /// <returns>InputManager의 instance</returns>
    public static InputManager GetInstance()
    {
        if (instance != null)
            return instance;
        else
        {
            Debug.Log("There is no Inputmanager");
            return null;
        }
    }

    #endregion


    #region 실행

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        //
        // GetKey
        //
        key_W           = Input.GetKey(KeyCode.W);
        key_A           = Input.GetKey(KeyCode.A);
        key_S           = Input.GetKey(KeyCode.S);
        key_D           = Input.GetKey(KeyCode.D);
        key_Space       = Input.GetKey(KeyCode.Space);
        key_LeftControl = Input.GetKey(KeyCode.LeftControl);
        key_LeftMouse   = Input.GetMouseButton(0);

        //
        // GetKeyDown
        //
        keyDown_W           = Input.GetKeyDown(KeyCode.W);
        keyDown_A           = Input.GetKeyDown(KeyCode.A);
        keyDown_S           = Input.GetKeyDown(KeyCode.S);
        keyDown_D           = Input.GetKeyDown(KeyCode.D);
        keyDown_E           = Input.GetKeyDown(KeyCode.E);
        keyDown_F           = Input.GetKeyDown(KeyCode.F);
        keyDown_Space       = Input.GetKeyDown(KeyCode.Space);
        keyDown_LeftControl = Input.GetKeyDown(KeyCode.LeftControl);
        keyDown_LeftMouse   = Input.GetMouseButtonDown(0);

        //
        // GetKeyUp
        //
        //keyUp_W = Input.GetKey(KeyCode.W);
        keyUp_A     = Input.GetKeyUp(KeyCode.A);
        keyUp_S     = Input.GetKeyUp(KeyCode.S);
        keyUp_D     = Input.GetKeyUp(KeyCode.D);
        keyUp_Space = Input.GetKeyUp(KeyCode.Space);
        //keyUp_LeftControl = Input.GetKey(KeyCode.LeftControl);
        //keyUp_LeftMouse = Input.GetMouseButton(0);

        //
        // ETC
        //

        // 서로 반대되는 방향의 키가 동시에 입력될 시
        if (key_A && keyDown_D) keyDouble_D = true;
        if (key_D && keyDown_A) keyDouble_A = true;

        if (keyDouble_D && (keyUp_A || keyUp_D)) keyDouble_D = false;
        if (keyDouble_A && (keyUp_A || keyUp_D)) keyDouble_A = false;
    }

    #endregion
}
