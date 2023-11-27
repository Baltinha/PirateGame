using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_speed = 10;
    
    private Transform m_target;
    private float m_timer = 3f;
    private float m_temptime;
    private Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_rb.velocity = Vector2.up * m_speed;
    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null)
            return;

        if (collision.gameObject.layer == 1)
            gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Enemy"))
        {
        }
    }
}
