using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_bulletDamage;

    // Start is called before the first frame update

    private void Start()
    {

    }
    private void FixedUpdate()
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

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
