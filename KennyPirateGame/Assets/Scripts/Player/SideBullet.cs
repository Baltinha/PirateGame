using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBullet : MonoBehaviour
{
    [SerializeField] float m_bulletDamage;
    private float m_timer = 2f;
    private float m_temptime;

    public float BulletDamage { get => m_bulletDamage; set => m_bulletDamage = value; }

    // Start is called before the first frame update

    private void Start()
    {
        m_temptime = Time.time;
    }
    private void FixedUpdate()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
        {
            Destroy(gameObject);
            m_timer = m_temptime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision == null)
            Destroy(gameObject);


        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
        

        if (collision.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
        
        if (collision.gameObject.CompareTag("Bullet"))
            Destroy(gameObject);
        
    }
 
}
