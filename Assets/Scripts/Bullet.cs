using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�����ӵ����˺�
    public int damage = 50;
    //������
    public float speed = 200;
    //Ŀ��
    private Transform target;
    //��ը��Ч
    public GameObject explosionEffectPrefab;

    private float distanceArriveTarget = 1;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    private void Update()
    {
        //��������ǰ��ʧʱ
        if (target == null)
        {
            Die();
            return;
        }
        //ʹģ������Ŀ��
        transform.LookAt(target.position);
        //�ӵ����ƶ��ٶ�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //
        Vector3 dir = target.position - transform.position;
        //���ӵ��͹���ľ���С��һ��ֵʱ����Ϊ����
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
