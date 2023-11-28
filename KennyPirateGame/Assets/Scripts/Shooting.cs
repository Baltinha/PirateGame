using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform[] m_sideBulletsTarget;
    [SerializeField] private GameObject m_sideBulletPrefab;
    [SerializeField] private Transform m_targetFireMainShot;
    [SerializeField] private float m_bulletSpeed = 10;

    private const string k_FireMainShoot = "Fire1"; 
    private const string k_FireSideShoot = "Fire2";


    void Update()
    {
        if (Input.GetButtonDown(k_FireMainShoot) && !Input.GetButtonDown(k_FireSideShoot)) 
        {
            FireMainShoot();
        }
        if (Input.GetButtonDown(k_FireSideShoot) && !Input.GetButtonDown(k_FireMainShoot))
        {
            FireSideBullet();
        }
    }

    private void FireMainShoot()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObject();

        if (bullet != null )
        {   
            bullet.transform.position = m_targetFireMainShot.position;
            bullet.SetActive(true);
            Rigidbody2D TempRb = bullet.GetComponent<Rigidbody2D>();
            TempRb.AddForce(-m_targetFireMainShot.up * m_bulletSpeed, ForceMode2D.Impulse);
        }
    }

    private void FireSideBullet() 
    {

        GameObject sideBullet0 = Instantiate(m_sideBulletPrefab, m_sideBulletsTarget[0].position, m_sideBulletsTarget[0].rotation);
        GameObject sideBullet1 = Instantiate(m_sideBulletPrefab, m_sideBulletsTarget[1].position, m_sideBulletsTarget[0].rotation);
        GameObject sideBullet2 = Instantiate(m_sideBulletPrefab, m_sideBulletsTarget[2].position, m_sideBulletsTarget[0].rotation);
        
        Rigidbody2D sideRb0 = sideBullet0.GetComponent<Rigidbody2D>();
        Rigidbody2D sideRb1 = sideBullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D sideRb2 = sideBullet2.GetComponent<Rigidbody2D>();

        sideRb0.AddForce(-sideBullet0.transform.right * m_bulletSpeed, ForceMode2D.Impulse);
        sideRb1.AddForce(-sideBullet1.transform.right * m_bulletSpeed, ForceMode2D.Impulse);
        sideRb2.AddForce(-sideBullet2.transform.right * m_bulletSpeed, ForceMode2D.Impulse);
    }
}
