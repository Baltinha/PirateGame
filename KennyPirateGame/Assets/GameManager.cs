using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_gameTime; 

    private int m_pointsInGame;

    public float GameTime { get => m_gameTime; set => m_gameTime = value; }
    public int PointsInGame { get => m_pointsInGame; set => m_pointsInGame = value; }

    public static GameManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        print(m_pointsInGame);
    }
}
