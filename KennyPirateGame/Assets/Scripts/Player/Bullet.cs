using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Damage")]
    [SerializeField] private float m_bulletDamage;

    private Animator m_Animator;

    public string Name;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    public float BulletDamage { get => m_bulletDamage; set => m_bulletDamage = value; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Name))
            return;
        if (collision.gameObject.CompareTag("Wall"))
            DesactivetBullet();
        
        m_Animator.SetBool("ShootFire", true);
    }
 
    public void DesactivetBullet () 
    {
        m_Animator.Play("IDLEBullet");
        gameObject.SetActive(false);

    }

}
