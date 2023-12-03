using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooring : MonoBehaviour
{
    [SerializeField] private Transform m_targetFireMainShot;
    [SerializeField] private GameObject m_prefabBullet;
    [SerializeField] private float m_shootTimer = 2f;
    [SerializeField] private float m_bulletForce;

    private EnemyMoving m_enemyMoving;
    private float m_temptime;
    private GameObject m_targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        m_enemyMoving = GetComponent<EnemyMoving>();
        m_targetPosition = GameObject.FindGameObjectWithTag("Player");
        m_temptime = m_shootTimer;
    }

    private void Update()
    {
        if (GameManager.Instance.TimeIsRunning)
        {

            if (m_enemyMoving.TypeOfEnemy == TypeOfEnemy.Shooter && m_enemyMoving.StateOfEnemy == StateOfEnemy.Attacking)
            {
                m_shootTimer -= Time.deltaTime;
                if (m_shootTimer < 0)
                {
                    m_enemyMoving.RotateEnemy(m_targetPosition.transform);
                    FireMainShoot();
                    m_shootTimer = m_temptime;
                }
            }
        }
    }

    public void FireMainShoot()
    {
        if (m_targetFireMainShot == null)
            return;

        //Instantiate(m_prefabBullet, m_targetFireMainShot.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().Name = this.gameObject.tag;
            bullet.transform.position = m_targetFireMainShot.position;
            bullet.SetActive(true);
            Rigidbody2D TempRb = bullet.GetComponent<Rigidbody2D>();
            TempRb.velocity = Vector2.zero;
            TempRb.AddForce(m_targetFireMainShot.right * m_bulletForce, ForceMode2D.Impulse);
            
        }
    }
}
