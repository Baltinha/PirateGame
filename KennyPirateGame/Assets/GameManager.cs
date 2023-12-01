using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Range(60f,180f)]
    [SerializeField] private float m_gameTime; 

    private int m_pointsInGame;
    private bool m_timeIsRunning = true;
    private float m_timeRemaining = 0;
    public TMP_Text m_textTime;
    public Image m_imageFinal;

    public float GameTime { get => m_gameTime; set => m_gameTime = value; }
    public int PointsInGame { get => m_pointsInGame; set => m_pointsInGame = value; }

    public static GameManager Instance { get; private set; }
    public float TimeRemaining { get => m_timeRemaining; set => m_timeRemaining = value; }

    // Start is called before the first frame update
    void Start()
    {
        
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timeIsRunning) 
        {
            if (m_timeRemaining >= 0)
            {
                m_timeRemaining += Time.deltaTime;
                DisplayTime(m_timeRemaining);
            } 
        }
        if (m_timeRemaining >= m_gameTime) 
        {
            m_imageFinal.gameObject.SetActive(true);
        }

    }
    private void DisplayTime(float timeToDisplay) 
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        m_textTime.text = string.Format("{0:00} : {1:00}", minutes,seconds);

    }
}
