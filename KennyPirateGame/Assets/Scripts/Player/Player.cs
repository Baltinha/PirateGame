using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotatespeed;
    [SerializeField] private float m_maxHeath;
    [SerializeField] private Image m_imageFinal;
    [SerializeField] private Sprite[] SpriteHealth; 

    private float m_currentHealth;
    private float m_Horizontal;
    private float m_vertical;
    private Transform m_sprite;
    private SpriteRenderer m_spriteRenderer;
    private FloatHealthBar m_healthBar;

    private const string k_Horizontal = "Horizontal";
    private const string k_Vertical = "Vertical";

    public float CurrentHealth { get => m_currentHealth; set => m_currentHealth = value; }
    public float MaxHeath { get => m_maxHeath; set => m_maxHeath = value; }
    public FloatHealthBar HealthBar { get => m_healthBar; set => m_healthBar = value; }

    void Start()
    {
        m_currentHealth = m_maxHeath;
        m_healthBar = GetComponentInChildren<FloatHealthBar>();
        m_sprite = GetComponentInChildren<Transform>().GetChild(0);
        m_healthBar.UpdateHealthBar(m_currentHealth, m_maxHeath);
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.TimeIsRunning)
        {
            m_Horizontal = Input.GetAxisRaw(k_Horizontal);
            m_vertical = Input.GetAxisRaw(k_Vertical);

            Vector2 movement = new Vector2(m_Horizontal, m_vertical);
            float InputMagnitude = Mathf.Clamp01(movement.magnitude);
            movement.Normalize();

            transform.Translate(movement * m_speed * InputMagnitude * Time.deltaTime, Space.World);

            if (movement != Vector2.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movement);
                m_sprite.transform.rotation = Quaternion.RotateTowards(m_sprite.transform.rotation, rotation, m_rotatespeed * Time.deltaTime);
            }


        }
        if (m_currentHealth <= 0 )
        {
            this.gameObject.SetActive(false);
            m_imageFinal.gameObject.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.TimeIsRunning = false;
        }
        if (CurrentHealth <= 65) 
        {
            m_spriteRenderer.sprite = SpriteHealth[0];
        }
        if (CurrentHealth <= 25)
        {
            m_spriteRenderer.sprite = SpriteHealth[1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            m_currentHealth -= bullet.BulletDamage;
            m_healthBar.UpdateHealthBar(m_currentHealth, m_maxHeath);
        }
    }
}
