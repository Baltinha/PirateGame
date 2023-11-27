using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform m_target;

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
        }
    }
}
