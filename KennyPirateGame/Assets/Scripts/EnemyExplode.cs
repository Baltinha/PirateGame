using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    [SerializeField] private float m_explodeSpeed;
    [SerializeField] private float m_explodeTimer = 2;

    private GameObject m_targetPosition;
    private float m_temptime;
    // Start is called before the first frame update
    void Start()
    {
        m_temptime = m_explodeTimer;
        m_targetPosition = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyMoving.Instance.TypeOfEnemy == TypeOfEnemy.Chaser && EnemyMoving.Instance.StateOfEnemy == StateOfEnemy.Attacking)
        {
            m_explodeTimer -= Time.deltaTime;
            EnemyMoving.Instance.RotateEnemy(m_targetPosition.transform);
            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition.transform.position, m_explodeSpeed * Time.deltaTime);
        }
        if (m_explodeTimer < 0)
        {
            print("Explodiu");
            Destroy(gameObject);
            m_explodeTimer = m_temptime;
        }
    }
    

}
