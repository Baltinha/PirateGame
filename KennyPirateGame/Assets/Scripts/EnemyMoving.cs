using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMoving : MonoBehaviour
{ 
    [SerializeField] private Transform[] m_movePoints;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotateSpeed;


    private Transform m_target;
    private int m_indexRandoPoints;
    private int m_indexTarget;

    [field: SerializeField] public StateOfEnemy StateOfEnemy { get; private set; }
    [field: SerializeField] public TypeOfEnemy TypeOfEnemy { get; private set; }

    public static EnemyMoving Instance { get; private set; }

    void Start()
    {
        Instance = this;
        m_indexRandoPoints = Random.Range(0, m_movePoints.Length);
        RotateEnemy(m_movePoints[m_indexRandoPoints]);
    }

    void Update()
    {
        if (StateOfEnemy == StateOfEnemy.Moving) 
        {
            if (transform.position == m_movePoints[m_indexRandoPoints].position)
            {
                m_indexRandoPoints = Random.Range(0, m_movePoints.Length);

                RotateEnemy(m_movePoints[m_indexRandoPoints]);

                IncreaseTargetInt();
            }
            transform.position = Vector3.MoveTowards(transform.position, m_movePoints[m_indexRandoPoints].position, m_speed * Time.deltaTime);
        }
        else if (StateOfEnemy == StateOfEnemy.Attacking)
        {
            //switch (TypeOfEnemy)
            //{
            //    case TypeOfEnemy.Chaser:
            //        if (m_target != null)
            //        {
            //            RotateEnemy(m_target);
            //            transform.position = Vector3.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
            //        }
            //        break;
            //    case TypeOfEnemy.Shooter:
            //        if (m_target != null) 
            //        {
            //            //EnemyShooring.Instance.FireMainShoot();

            //        }
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    private void RotateEnemy(Transform TransformTemp)
    {
        if (TransformTemp == null)
            return;
        if (transform == null)
            return;

        Vector3 direction = TransformTemp.position - transform.position;
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
        if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("Player"))
        {
            print("ai");
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Explodiu");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StateOfEnemy = StateOfEnemy.Attacking;

            m_target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StateOfEnemy = StateOfEnemy.Moving;
        m_indexRandoPoints = Random.Range(0, m_movePoints.Length);
        RotateEnemy(m_movePoints[m_indexRandoPoints]);

    }
}
