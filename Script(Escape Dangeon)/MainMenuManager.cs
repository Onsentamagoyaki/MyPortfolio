using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void _StartTheGame()//GameStart�����������̏���
    {
        SceneManager.LoadScene("Stage");
    }

    public void _QuitTheGame()//�Q�[�����I�������鏈���iWebGL�łł͎������Ȃ��悤�Ɂj
    {
        Application.Quit();
    }

}
