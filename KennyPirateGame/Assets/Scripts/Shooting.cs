using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_bulletSpeed = 10;

    private const string k_Fire = "Fire1";

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown(k_Fire)) 
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObject();

        if (bullet != null )
        {   
            bullet.transform.position = m_target.position;
            bullet.SetActive(true);
            Rigidbody2D TempRb = bullet.GetComponent<Rigidbody2D>();
            TempRb.AddForce(-m_target.up * m_bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
