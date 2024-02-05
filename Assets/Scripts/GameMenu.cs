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
        //�������Unity������Ч
#if UNITY_EDITOR
        //ʹUnity�Ŀ�ʼ��Ϸ�����˳�
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //ʹ��������Ϸ�˳�
        Application.Quit();
#endif
    }
}
