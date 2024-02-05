using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//存储每一波敌人生成所需要的属性   
[System.Serializable]
public class Wave
{
    //敌人种类
    public GameObject enemyPrefab;
    //生成数目
    public int count;
    //生成速率
    public float rate;
    
}
