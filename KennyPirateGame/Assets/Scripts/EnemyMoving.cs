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

    private float m_maxX;
    private float m_minX;
    private float m_maxY;
    private float m_minY;
    private Camera m_camera;
    private float m_temptime;
    private Transform m_target;
    private int m_indexTarget;

    [field: SerializeField] public StateOfEnemy StateOfEnemy { get; private set; }
    [field: SerializeField] public TypeOfEnemy TypeOfEnemy { get; private set; }

    public static EnemyMoving Instance { get; private set; }

    private void Awake()
    {
        m_camera = Camera.main;
    }
    void Start()
    {

        var height = m_camera.orthographicSize;
        var width = height * m_camera.aspect;

        m_minX = + width;
        m_minX = + width;

        m_maxY = - height;
        m_minY = + height;

        m_temptime = m_moveToNextSpotTimer;
        Instance = this;
        RotateEnemy(m_movePoints);
        m_movePoints.position = new Vector2(Random.Range(-5,10), Random.Range(7, -7));
    }

    void Update()
    {

        if (StateOfEnemy == StateOfEnemy.Moving) 
        {
            
            transform.position = Vector3.MoveTowards(transform.position, m_movePoints.position, m_speed * Time.deltaTime);

            m_moveToNextSpotTimer -= Time.deltaTime;
            if (m_moveToNextSpotTimer < 0)
            {
                m_movePoints.position = new Vector2(Random.Range(-5, 10), Random.Range(7, -7));
                //m_movePoints.position = new Vector2(Random.Range(m_minX, m_minX), Random.Range(m_minY, m_minY));

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
