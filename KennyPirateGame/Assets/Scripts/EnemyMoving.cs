using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMoving : MonoBehaviour
{ 
    [SerializeField] private Transform m_movePoints;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotateSpeed;
    [SerializeField] private float m_moveToNextSpotTimer = 3;

    private Camera m_camera;
    private Transform m_target;
    private int m_indexTarget;
    private float m_temptime;

    [field: SerializeField] public StateOfEnemy StateOfEnemy { get; private set; }
    [field: SerializeField] public TypeOfEnemy TypeOfEnemy { get; private set; }

    public Transform MovePoints { get => m_movePoints; set => m_movePoints = value; }

    private void Awake()
    {
        m_camera = Camera.main;
    }
    void Start()
    {
        m_temptime = m_moveToNextSpotTimer;
        RotateEnemy(m_movePoints);
    }

    void Update()
    {

        if (StateOfEnemy == StateOfEnemy.Moving) 
        {
            
            transform.position = Vector3.MoveTowards(transform.position, m_movePoints.position, m_speed * Time.deltaTime);

            m_moveToNextSpotTimer -= Time.deltaTime;
            if (m_moveToNextSpotTimer < 0)
            {

                RotateEnemy(m_movePoints);

                m_moveToNextSpotTimer = m_temptime;
            }
        }

    }

    public void RotateEnemy(Transform TransformTemp)
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("ai");
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            print("toma");
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StateOfEnemy = StateOfEnemy.Attacking;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StateOfEnemy = StateOfEnemy.Moving;
        RotateEnemy(m_movePoints);

    }
}
