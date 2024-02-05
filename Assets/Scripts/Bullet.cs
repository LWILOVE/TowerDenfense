using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //设置子弹的伤害
    public int damage = 50;
    //弹道速
    public float speed = 200;
    //目标
    private Transform target;
    //爆炸特效
    public GameObject explosionEffectPrefab;

    private float distanceArriveTarget = 1;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    private void Update()
    {
        //当敌人提前消失时
        if (target == null)
        {
            Die();
            return;
        }
        //使模型望向目标
        transform.LookAt(target.position);
        //子弹的移动速度
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //
        Vector3 dir = target.position - transform.position;
        //当子弹和怪物的距离小于一定值时，视为命中
        if (dir.magnitude <= distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }

}
