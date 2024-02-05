using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExitGame()
    {
        //本语句在Unity界面生效
#if UNITY_EDITOR
        //使Unity的开始游戏界面退出
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //使打包后的游戏退出
        Application.Quit();
#endif
    }
}
