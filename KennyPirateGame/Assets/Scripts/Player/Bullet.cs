using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_bulletDamage;

    private Animator m_Animator;

    public string Name;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    public float BulletDamage { get => m_bulletDamage; set => m_bulletDamage = value; }

    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Name))
            return;
        if (collision.gameObject.CompareTag("Wall"))
            DesactivetBullet();
        
        m_Animator.SetBool("ShootFire", true);

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    gameObject.SetActive(false);
        //}
        //if (collision.gameObject.CompareTag("Bullet"))
        //{
        //    gameObject.SetActive(false);
        //}
        //if (collision.gameObject.CompareTag("SideBullet"))
        //{
        //    gameObject.SetActive(false);
        //}
        //gameObject.SetActive(false);
    }
 
    public void DesactivetBullet () 
    {
        m_Animator.Play("IDLEBullet");
        gameObject.SetActive(false);

    }

}
