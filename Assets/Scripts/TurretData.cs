using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TurretData 
{
    //��Ϸ��λ  
    public GameObject turretPrefab;
    //��λ�۸�
    public int cost;
    //���׵�λ
    public GameObject turretUpPrefab;
    //���׼۸�
    public int costUp;
    //��̨����
    public TurretType type;
}
//ʹ��ö�����Ͷ�����̨���
public enum TurretType
{
    Turret2,
    Turret3,
    Turret1,
}
