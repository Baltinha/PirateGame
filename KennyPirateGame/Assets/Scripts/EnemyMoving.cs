using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{ 
    [SerializeField] private Transform[] m_movePoints;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotateSpeed;

    private int m_indexRandoPoints;
    private int m_indexTarget;
    [field: SerializeField] public StateOfEnemy StateOfEnemy { get; private set; }

    void Start()
    {
        m_indexRandoPoints = Random.Range(0, m_movePoints.Length);
        RotateEnemy();
    }

    void Update()
    {
        if (StateOfEnemy == StateOfEnemy.Moving) 
        {
            if (transform.position == m_movePoints[m_indexRandoPoints].position)
            {
                m_indexRandoPoints = Random.Range(0, m_movePoints.Length);

                RotateEnemy();

                IncreaseTargetInt();
            }
            transform.position = Vector3.MoveTowards(transform.position, m_movePoints[m_indexRandoPoints].position, m_speed * Time.deltaTime);
        }
        else if (StateOfEnemy == StateOfEnemy.Attacking)
        {
            print("estou atacando");
        }
        //
        //    if (m_target)
        //    {
        //        switch (TypeOfEnemy)
        //        {
        //            case TypeOfEnemy.Chaser:
        //                Vector2 TargetDirectionToPlayer = m_target.position - m_moveSprite.position;
        //                float Angle = Mathf.Atan2(TargetDirectionToPlayer.y, TargetDirectionToPlayer.x) * Mathf.Rad2Deg;
        //                Quaternion TempQuaternion = Quaternion.Euler(new Vector3(0, 0, Angle));
        //                m_moveSprite.localRotation = Quaternion.Slerp(m_moveSprite.localRotation, TempQuaternion, m_rotateSpeed);
        //                transform.position = Vector3.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
        //                break;
        //            case TypeOfEnemy.Shooter:
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    if (!m_target)

    }

    private void RotateEnemy()
    {
        Vector3 direction = m_movePoints[m_indexRandoPoints].position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void IncreaseTargetInt()
    {
        m_indexTarget++;
        if (m_indexTarget >= m_movePoints.Length)
        {
            m_indexRandoPoints = Random.Range(0, m_movePoints.Length);

            m_indexTarget = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("ai");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StateOfEnemy = StateOfEnemy.Attacking;

            //m_target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StateOfEnemy = StateOfEnemy.Moving;
    }
}
