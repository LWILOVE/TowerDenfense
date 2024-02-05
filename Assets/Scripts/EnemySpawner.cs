using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //记录目前剩下的怪
    public static int CountEnemyAlive = 0;
    //波数管理器
    public Wave[] waves;
    //生成起始点
    public Transform START;
    //设置每一波怪出现的间隔时间
    public float waveRate = 0.3f;

    private void Start()
    {
        //启动协程
        StartCoroutine(SpawnEnemy()); ;
    }
    public void Stop()
    {   
        StopAllCoroutines(); 
    }
    //使用协程生成怪物
    IEnumerator SpawnEnemy()
    {       
        //每一波敌人按照每一波的属性进行生成
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab,START.position,Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.rate);
                }
            }
            //当有敌人存活的时候
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while(CountEnemyAlive > 0)
        {
            yield return 0;
        }
        GameManager._instance.Win();
    }
}
