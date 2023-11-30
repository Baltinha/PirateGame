using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float m_spawnPointRate = 2f;
    [SerializeField] private GameObject[] m_enemysPrefab;
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private int m_amountPoll = 10;

    private List<GameObject> m_pool = new List<GameObject>();
    private int m_spawnRandomCount;
    private bool m_isSpawned = true;
    void Start()
    {

        if (m_enemysPrefab == null || m_spawnPoints == null)
            return;

        m_spawnRandomCount = Random.Range(0, m_spawnPoints.Length);
        for (int i = 0; i < m_amountPoll; i++)
        {
            int indexPrefab = Random.Range(0, m_enemysPrefab.Length);
            GameObject obj = Instantiate(m_enemysPrefab[indexPrefab], m_spawnPoints[m_spawnRandomCount].position, Quaternion.identity);
            obj.SetActive(false);
            m_pool.Add(obj);
            m_spawnRandomCount = Random.Range(0, m_spawnPoints.Length);
        }

        StartCoroutine(SpawnerEnemys());
    }

    private IEnumerator SpawnerEnemys() 
    {
        
        WaitForSeconds wait = new WaitForSeconds(m_spawnPointRate);

        while (m_isSpawned) 
        {
            yield return wait;

            int indexRand = Random.Range(0, m_enemysPrefab.Length);
            GameObject EnemyRandom = m_enemysPrefab[indexRand];
            //GameObject EnemyRandom = GetPooledObject();
            //EnemyRandom.SetActive(true);
            Instantiate(EnemyRandom, m_spawnPoints[m_spawnRandomCount].position,Quaternion.identity);

            m_spawnRandomCount = Random.Range(0, m_spawnPoints.Length);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < m_pool.Count; i++)
        {
            if (!m_pool[i].activeInHierarchy)
                return m_pool[i];
        }
        return null;
    }
}
