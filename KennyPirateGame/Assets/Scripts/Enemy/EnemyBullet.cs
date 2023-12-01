using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float m_bulletDamage;

    public float BulletDamage { get => m_bulletDamage; set => m_bulletDamage = value; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

}
