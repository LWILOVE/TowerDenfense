using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapCube : MonoBehaviour
{
    //��ʹ����ı�����public��Ҳ�޷���Unity���濴��
    [HideInInspector]
    //�洢�ڱ������ϵ���̨
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
        //������̨
        isUpgraded = false;
        turrentGo = GameObject.Instantiate(turretData.turretPrefab,this.transform.position,Quaternion.identity);
        //������Ч
        GameObject effect = GameObject.Instantiate(buildEffect, this.transform.position, Quaternion.identity);
        Destroy(effect,1);
    }

    private void OnMouseEnter()
    {
        //�ɽ�����̬��������������ϣ����ʱ�ɺ�ɫ
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
        //������̨
        isUpgraded = true;
        turrentGo = GameObject.Instantiate(turretData.turretUpPrefab, this.transform.position, Quaternion.identity);
        //������Ч
        GameObject effect = GameObject.Instantiate(buildEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    public void DestroyTurret()
    {
        Destroy(turrentGo);
        isUpgraded=false;
        turrentGo = null;    
        turretData = null;
        //������Ч
        GameObject effect = GameObject.Instantiate(buildEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
