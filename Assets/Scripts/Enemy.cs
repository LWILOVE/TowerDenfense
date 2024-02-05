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
    //���ùֵ��ٶ�
    public float speed = 10;

    public int heart;
    //�ֵ�Ѫ
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
        //���ֵ����յ㣬�����ƶ�
        if (index > positions.Length - 1) return;
        //�ֵ��ƶ�
        transform.Translate((positions[index].position-transform.position).normalized*Time.deltaTime*speed);
        //�����뱾·��ľ���С��0.1ʱ���ù�׷����һĿ��
        if (Vector3.Distance(positions[index].position,transform.position) < 0.1f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    //������������
    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }
    //���˹�������
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
