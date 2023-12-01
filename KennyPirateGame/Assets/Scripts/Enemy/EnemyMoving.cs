using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMoving : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotateSpeed;
    [Header("Health")]
    [SerializeField] private float m_maxHeath = 100f;
    [Header("Chaser")]
    [SerializeField] private float m_explodeTimer = 2f;
    [SerializeField] private float m_explodeSpeed;
    [SerializeField] private float m_explodeDamage;

    private float m_currentHealth;
    private FloatHealthBar m_healthBar;
    private Transform m_targetPlayer;
    private float m_temptime;
    private Player m_movement;
    

    [field: SerializeField] public StateOfEnemy StateOfEnemy { get; private set; }
    [field: SerializeField] public TypeOfEnemy TypeOfEnemy { get; private set; }


    private void Awake()
    {
        m_healthBar = GetComponentInChildren<FloatHealthBar>();
        m_targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        m_movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Start()
    {
        m_temptime = m_explodeTimer;
        RotateEnemy(m_targetPlayer);
        m_currentHealth = m_maxHeath;
        m_healthBar.UpdateHealthBar(m_currentHealth, m_maxHeath);
    }

    void Update()
    {
        if (GameManager.Instance.TimeIsRunning)
        {

            if (m_targetPlayer == null)
                return;

            if (StateOfEnemy == StateOfEnemy.Moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_targetPlayer.position, m_speed * Time.deltaTime);
                RotateEnemy(m_targetPlayer);
            }
            if (TypeOfEnemy == TypeOfEnemy.Chaser && StateOfEnemy == StateOfEnemy.Attacking)
            {
                m_explodeTimer -= Time.deltaTime;
                RotateEnemy(m_targetPlayer.transform);
                transform.position = Vector3.MoveTowards(transform.position, m_targetPlayer.transform.position, m_explodeSpeed * Time.deltaTime);
            }
            if (m_explodeTimer < 0)
            {
                //adicionar animação
                Destroy(gameObject);
                //this.gameObject.SetActive(false);
                m_movement.CurrentHealth -= m_explodeDamage;
                m_movement.HealthBar.UpdateHealthBar(m_movement.CurrentHealth, m_movement.MaxHeath);
                m_explodeTimer = m_temptime;

            }


            if (m_currentHealth <= 0)
            {
                //this.gameObject.SetActive(false);
                //m_currentHealth = m_maxHeath;
                //this.transform.position = Vector3.zero;
                GameManager.Instance.PointsInGame += 1;
                Destroy(gameObject);
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
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            m_currentHealth -= bullet.BulletDamage;
            m_healthBar.UpdateHealthBar(m_currentHealth, m_maxHeath);
        }
        if (collision.gameObject.CompareTag("SideBullet"))
        {
            SideBullet bullet = collision.gameObject.GetComponent<SideBullet>();
            m_currentHealth -= bullet.BulletDamage;
            m_healthBar.UpdateHealthBar(m_currentHealth, m_maxHeath);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            StateOfEnemy = StateOfEnemy.Attacking;

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
    }

}
