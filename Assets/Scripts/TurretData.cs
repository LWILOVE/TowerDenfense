using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TurretData 
{
    //游戏单位  
    public GameObject turretPrefab;
    //单位价格
    public int cost;
    //进阶单位
    public GameObject turretUpPrefab;
    //进阶价格
    public int costUp;
    //炮台种类
    public TurretType type;
}
//使用枚举类型定义炮台类别
public enum TurretType
{
    Turret2,
    Turret3,
    Turret1,
}
