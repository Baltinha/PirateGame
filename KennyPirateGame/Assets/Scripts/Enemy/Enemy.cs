using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotateSpeed;
    [Header("Health")]
    [SerializeField] private float m_maxHeath = 100f;
    [SerializeField] private Sprite [] m_spritesHealthChange;
    [Header("Chaser")]
    [SerializeField] private float m_explodeTimer = 2f;
    [SerializeField] private float m_explodeSpeed;
    [SerializeField] private float m_explodeDamage;


    private float m_currentHealth;
    private FloatHealthBar m_healthBar;
    private Transform m_targetPlayer;
    private float m_temptime;
    private Player m_movementPlayer;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    

    [field: SerializeField] public StateOfEnemy StateOfEnemy { get; private set; }
    [field: SerializeField] public TypeOfEnemy TypeOfEnemy { get; private set; }


    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_healthBar = GetComponentInChildren<FloatHealthBar>();
        m_targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        m_movementPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
                
                m_animator.SetBool("ItsExplode", true);
                m_explodeTimer = m_temptime;

            }


            if (m_currentHealth <= 0)
            {

                m_animator.SetBool("ItsDead", true);
                
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
            ChangeSpriteByHealth();
        }
        if (collision.gameObject.CompareTag("SideBullet"))
        {
            SideBullet bullet = collision.gameObject.GetComponent<SideBullet>();
            m_currentHealth -= bullet.BulletDamage;
            m_healthBar.UpdateHealthBar(m_currentHealth, m_maxHeath);
            ChangeSpriteByHealth();
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

    public void DestroyGameObj() 
    {
        GameManager.Instance.PointsInGame += 1;
        Destroy(gameObject);
    }
    public void DestroyChacerAttaking()
    {

        DelayPlayerDamageTaken();
        Destroy(gameObject);
    }

    public void DelayPlayerDamageTaken() 
    {
        m_movementPlayer.CurrentHealth -= m_explodeDamage;
        m_movementPlayer.HealthBar.UpdateHealthBar(m_movementPlayer.CurrentHealth, m_movementPlayer.MaxHeath);
    }

    public void ChangeSpriteByHealth() 
    {

        if (m_currentHealth <= 40)
        {
            m_animator.SetTrigger("ChangeSprite");
        }
        if (m_currentHealth <= 20)
        {
            m_animator.SetTrigger("ChangeSprite");
        }
    }
}
