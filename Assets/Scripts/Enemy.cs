using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private Transform[] positions;
    private int index = 0;
    public GameObject explosionEffect;
    public Slider hpSlider;
    //设置怪的速度
    public float speed = 10;

    public int heart;
    //怪的血
    public float hp = 500;
    private float totalHp;
    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //若怪到达终点，则不再移动
        if (index > positions.Length - 1) return;
        //怪的移动
        transform.Translate((positions[index].position-transform.position).normalized*Time.deltaTime*speed);
        //当怪与本路标的距离小于0.1时，让怪追踪下一目标
        if (Vector3.Distance(positions[index].position,transform.position) < 0.1f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    //敌人死亡函数
    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }
    //敌人攻击函数
    void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
        GameManager._instance.Failed();
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0)
            return;
        hp -= damage;
        hpSlider.value = hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect,transform.position,transform.rotation);
        Destroy(effect,2.0f);
        Destroy(this.gameObject);
    }
}
