using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;
    public static GameManager _instance;
    private EnemySpawner enemySpawner;
    private void Awake()
    {
        _instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "ʤ ��";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text = "ʧ ��";
    }

    public void OnButtonRetry()
    {
        //���¼�����Ϸ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
