using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //�ռ������ڹ�����Χ�ڵĵ���
    private List<GameObject> enemys = new List<GameObject>();
    //���幥�����ʱ���
    public float attackRateTime = 5;
    private float timer = 0;
    //������������
    public GameObject bulletPrefab;
    //�����ӵ����λ��
    public Transform firePosition;
    public Transform head;
    //�ж��Ƿ�ʹ�ü���,������û�������
    public bool useLaser = false;
    //���弤����˺�����
    public float damageRate;
    //���弤�����
    public LineRenderer laserRenderer;
    //���弤����Ч
    public GameObject laserEffect;
    private void Start()
    {
        timer = attackRateTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //����ڹ�����Χ�ڵĵ���
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
        //����̨�������
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
            //ʹ�ü�����й���
            if (enemys[0] == null)
            {
                UpdataEnemys();
            }
            if (enemys.Count > 0)
            {
                //�����ߵ�λ��
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

    //��������
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdataEnemys();
        }
        if (enemys.Count > 0)
        {
            //�����ӵ� 
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            //ʹ�ӵ������Է�
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = 0;
        }
      
    }
    //���µ�����Ϣ
    void UpdataEnemys()
    {
        List<int> emptyIndex = new List<int>();
        //�Ƴ����м����еĿ�Ԫ��
        for (int index = 0; index < enemys.Count; index++)
        {
            //������п�Ԫ�ص��±�
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        //�Ƴ�
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
