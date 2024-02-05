using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour
{
    //�洢��̨���ݵĶ���
    public TurretData turret2;
    public TurretData turret3;
    public TurretData turret1;
    //��ʾ��ǰѡ�����̨��Ҫ�������̨��
    private TurretData selectedTurretData;
    [HideInInspector]
    //����ǰ�������̨(�����е�����)
    public MapCube selectedMapCube;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 99;

    public GameObject upCanvas;

    public Button buttonUp;

    private Animator UpCanvasAnimator;

    //��Ǯ������
    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }
    private void Start()
    {
        ChangeMoney();
        UpCanvasAnimator = upCanvas.GetComponent<Animator>(); 
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //�������Ƿ���UI�ϣ����ǣ���������,��������̨
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //������̨�Ľ���
                //�������߼��,�������������ĸ���̨��
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //���߼�ⷶΧ
                RaycastHit hit;
                //����1��������ߣ�����2����������������  ����3�����������룬����4��������
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //�õ����߼�⵽����Ϸ����
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turrentGo == null)
                    {
                        //��λ��������̨��������̨
                        //�����ҵ�ǰӵ�еĽ����
                        if (money >= selectedTurretData.cost)
                        {

                            ChangeMoney(-selectedTurretData.cost);
                            //���ô�������
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //��ʾ���ûǮ��
                            moneyAnimator.SetTrigger("Flicker");
                        }

                    }
                    else if(mapCube.turrentGo != null)
                    {
                        //��λ��������̨����������
                       
                        //upCanvas.activeInHierarchy:��Ȿ��Ϸ�����Ƿ��ڼ���״̬
                        if (mapCube == selectedMapCube && upCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpUI());
                        }
                        else
                        {
                            //�������
                            ShowUpUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;
                    }
                }
            }
        }
    }
    public void OnTurret2Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret2;
        }
    }

    public void OnTurret3Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret3;
        }
    }

    public void OnTurret1Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = turret1;
        }
    }
    //����UI������
    void ShowUpUI(Vector3 pos,bool isDisableUpgrade = false)
    {
        upCanvas.SetActive(true);
        pos.y += 2;
        upCanvas.transform.position = pos;
        buttonUp.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpUI()
    {
        UpCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.6f);
        upCanvas.SetActive(false);
    }

    //������������
    public void OnUpButtonDown()
    {
        if (money >= selectedMapCube.turretData.costUp)
        {
            ChangeMoney(-selectedMapCube.turretData.costUp);
            selectedMapCube.UpTurret();
            StartCoroutine(HideUpUI());
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }
    }

    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpUI());
    }
}
