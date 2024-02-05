using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //收集所有在攻击范围内的敌人
    private List<GameObject> enemys = new List<GameObject>();
    //定义攻击速率变量
    public float attackRateTime = 5;
    private float timer = 0;
    //定义武器变量
    public GameObject bulletPrefab;
    //定义子弹射出位置
    public Transform firePosition;
    public Transform head;
    //判断是否使用激光,但是我没有做这个
    public bool useLaser = false;
    //定义激光的伤害速率
    public float damageRate;
    //定义激光组件
    public LineRenderer laserRenderer;
    //定义激光特效
    public GameObject laserEffect;
    private void Start()
    {
        timer = attackRateTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //添加在攻击范围内的敌人
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        //让炮台看向敌人
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer > attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if (enemys.Count > 0)
        {
            if(laserRenderer.enabled == false)
                laserRenderer.enabled = true;
            laserEffect.SetActive(true);
            //使用激光进行攻击
            if (enemys[0] == null)
            {
                UpdataEnemys();
            }
            if (enemys.Count > 0)
            {
                //设置线的位置
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate*Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
            }
        }
        else
        {
            //
            laserRenderer.enabled = false;
            laserEffect.SetActive(false)    ;
        }
    }

    //攻击方法
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdataEnemys();
        }
        if (enemys.Count > 0)
        {
            //创建子弹 
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            //使子弹攻击对方
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = 0;
        }
      
    }
    //更新敌人信息
    void UpdataEnemys()
    {
        List<int> emptyIndex = new List<int>();
        //移除所有集合中的空元素
        for (int index = 0; index < enemys.Count; index++)
        {
            //获得所有空元素的下标
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        //移除
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
