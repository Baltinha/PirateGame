using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float m_spawnPointRate = 2f;
    [SerializeField] private GameObject[] m_enemysPrefab;

    [SerializeField] private Transform[] m_patrolPoints;

    private bool m_isSpawned = true;
    void Start()
    {
        StartCoroutine(SpawnerEnemys());
    }


    private IEnumerator SpawnerEnemys() 
    {
        WaitForSeconds wait = new WaitForSeconds(m_spawnPointRate);

        while (m_isSpawned) 
        {
            yield return wait;

            int rand = Random.Range(0, m_enemysPrefab.Length);
            GameObject EnemyRandom = m_enemysPrefab[rand];
            print(rand);

             Instantiate(EnemyRandom,transform.position,Quaternion.identity);

        }
    }

}
