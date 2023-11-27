using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //private float m_timer = 3f;
    //private float m_temptime;
    //private Rigidbody2D m_rb;
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
