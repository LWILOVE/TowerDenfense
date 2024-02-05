using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour
{
    //存储炮台数据的对象
    public TurretData turret2;
    public TurretData turret3;
    public TurretData turret1;
    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;
    [HideInInspector]
    //管理当前点击的炮台(场景中的物体)
    public MapCube selectedMapCube;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 99;

    public GameObject upCanvas;

    public Button buttonUp;

    private Animator UpCanvasAnimator;

    //金钱管理器
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
            //检测鼠标是否在UI上，若是，则无现象,否则建造炮台
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开发炮台的建造
                //进行射线检测,检测鼠标现在在哪个炮台上
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //射线检测范围
                RaycastHit hit;
                //参数1：检测射线，参数2：射线碰到的物体  参数3：射线最大距离，参数4：检测层数
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //得到射线检测到的游戏物体
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turrentGo == null)
                    {
                        //该位置上无炮台，允许建炮台
                        //检测玩家当前拥有的金币数
                        if (money >= selectedTurretData.cost)
                        {

                            ChangeMoney(-selectedTurretData.cost);
                            //调用创建函数
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //提示玩家没钱了
                            moneyAnimator.SetTrigger("Flicker");
                        }

                    }
                    else if(mapCube.turrentGo != null)
                    {
                        //该位置上有炮台，升级或拆除
                       
                        //upCanvas.activeInHierarchy:检测本游戏物体是否处于激活状态
                        if (mapCube == selectedMapCube && upCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpUI());
                        }
                        else
                        {
                            //升级面板
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
    //升级UI的显隐
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

    //升级与拆除操作
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
