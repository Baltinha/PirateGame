using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBullet : MonoBehaviour
{
    [Header("Bullet Damage")]
    [SerializeField] float m_bulletDamage;

    public float BulletDamage { get => m_bulletDamage; set => m_bulletDamage = value; }


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
