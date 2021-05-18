using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }
}
