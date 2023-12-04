using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float m_spawnPointRate;
    [SerializeField] private GameObject[] m_enemysPrefab;
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private int m_amountPoll = 10;
    [SerializeField]private GameManager m_gameManager;

    private List<GameObject> m_pool = new List<GameObject>();
    private int m_spawnRandomCount;
    private bool m_isSpawned;
    private float m_timeTemp;

    public static SpawnEnemy Instance { get; private set; }
    public float SpawnPointRate { get => m_spawnPointRate; set => m_spawnPointRate = value; }
    public bool IsSpawned { get => m_isSpawned; set => m_isSpawned = value; }
    public float TimeTemp { get => m_timeTemp; set => m_timeTemp = value; }

    void Start()
    {
        
        Instance = this;

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
        TimeTemp = SpawnPointRate;
    }
    private void Update()
    {
        
        if (IsSpawned) 
        {
            SpawnPointRate -= Time.deltaTime;
            if (SpawnPointRate < 0) 
            {
                int indexRand = Random.Range(0, m_enemysPrefab.Length);
                GameObject EnemyRandom = m_enemysPrefab[indexRand];
                Instantiate(EnemyRandom, m_spawnPoints[m_spawnRandomCount].position, Quaternion.identity);

                m_spawnRandomCount = Random.Range(0, m_spawnPoints.Length);
                SpawnPointRate = TimeTemp;
            }
            
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
