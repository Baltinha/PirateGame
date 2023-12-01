using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_bulletDamage;

    public float BulletDamage { get => m_bulletDamage; set => m_bulletDamage = value; }

    // Start is called before the first frame update


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

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SideBullet"))
        {
            gameObject.SetActive(false);
        }
    }
 
}
