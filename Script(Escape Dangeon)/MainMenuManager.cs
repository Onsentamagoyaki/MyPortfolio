using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void _StartTheGame()//GameStartを押した時の処理
    {
        SceneManager.LoadScene("Stage");
    }

    public void _QuitTheGame()//ゲームを終了させる処理（WebGL版では実装しないように）
    {
        Application.Quit();
    }

}
