using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapCube : MonoBehaviour
{
    //即使下面的变量是public的也无法在Unity界面看到
    [HideInInspector]
    //存储在本方格上的炮台
    public GameObject turrentGo;

    public GameObject buildEffect;
    [HideInInspector]
    public TurretData turretData;
    private new Renderer renderer;
    [HideInInspector]
    public bool isUpgraded = false;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        //创建炮台
        isUpgraded = false;
        turrentGo = GameObject.Instantiate(turretData.turretPrefab,this.transform.position,Quaternion.identity);
        //建筑特效
        GameObject effect = GameObject.Instantiate(buildEffect, this.transform.position, Quaternion.identity);
        Destroy(effect,1);
    }

    private void OnMouseEnter()
    {
        //可建造形态，若鼠标在物体上，材质变成红色
        if (turrentGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }

    public void UpTurret()
    {
        if (isUpgraded == true)
        {
            return;
        }
        Destroy(turrentGo);
        //创建炮台
        isUpgraded = true;
        turrentGo = GameObject.Instantiate(turretData.turretUpPrefab, this.transform.position, Quaternion.identity);
        //建筑特效
        GameObject effect = GameObject.Instantiate(buildEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void DestroyTurret()
    {
        Destroy(turrentGo);
        isUpgraded=false;
        turrentGo = null;    
        turretData = null;
        //建筑特效
        GameObject effect = GameObject.Instantiate(buildEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
