using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooring : MonoBehaviour
{
    [SerializeField] private Transform m_targetFireMainShot;
    [SerializeField] private GameObject m_prefabBullet;
    [SerializeField] private float m_bulletSpeed = 10;
    [SerializeField] private float m_shootTimer = 2;

    private float m_temptime;
    private GameObject m_targetPosition;

    public static EnemyShooring Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        m_targetPosition = GameObject.FindGameObjectWithTag("Player");
        m_temptime = m_shootTimer;
        Instance = this;
    }

    private void Update()
    {

        if (EnemyMoving.Instance.TypeOfEnemy == TypeOfEnemy.Shooter && EnemyMoving.Instance.StateOfEnemy == StateOfEnemy.Attacking) 
        {
            m_shootTimer -= Time.deltaTime;
            if (m_shootTimer < 0)
            {
                EnemyMoving.Instance.RotateEnemy(m_targetPosition.transform);
                FireMainShoot();
                m_shootTimer = m_temptime;
            }
        }
    }

    public void FireMainShoot()
    {
        if (m_targetFireMainShot == null)
            return;

        GameObject bullet = ObjectPool.Instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = m_targetFireMainShot.position;
            bullet.SetActive(true);
            Rigidbody2D TempRb = bullet.GetComponent<Rigidbody2D>();
            TempRb.AddForce(-m_targetPosition.transform.position * m_bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
