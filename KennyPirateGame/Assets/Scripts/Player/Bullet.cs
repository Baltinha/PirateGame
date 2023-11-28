using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_bulletDamage;
    private float m_timer = 2f;
    private float m_temptime;
    // Start is called before the first frame update

    private void Start()
    {
        m_temptime = Time.time;
    }
    private void FixedUpdate()
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
        {
            m_timer -= Time.deltaTime;
            if (m_timer < 0)
            {
                gameObject.SetActive(false);
                m_timer = m_temptime;
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
 
}
