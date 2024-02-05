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
        endMessage.text = "胜 利";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text = "失 败";
    }

    public void OnButtonRetry()
    {
        //重新加载游戏
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
