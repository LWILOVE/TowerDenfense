using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //��¼Ŀǰʣ�µĹ�
    public static int CountEnemyAlive = 0;
    //����������
    public Wave[] waves;
    //������ʼ��
    public Transform START;
    //����ÿһ���ֳ��ֵļ��ʱ��
    public float waveRate = 0.3f;

    private void Start()
    {
        //����Э��
        StartCoroutine(SpawnEnemy()); ;
    }
    public void Stop()
    {   
        StopAllCoroutines(); 
    }
    //ʹ��Э�����ɹ���
    IEnumerator SpawnEnemy()
    {       
        //ÿһ�����˰���ÿһ�������Խ�������
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
            //���е��˴���ʱ��
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
