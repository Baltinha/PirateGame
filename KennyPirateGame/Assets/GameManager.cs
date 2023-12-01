using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float m_gameTime;
    [SerializeField] private TMP_Text m_textTime;
    [SerializeField] private Image m_imageFinal;

    private int m_pointsInGame;
    private bool m_timeIsRunning = false;
    private float m_timeRemaining = 0;


    public float GameTime { get => m_gameTime; set => m_gameTime = value; }
    public int PointsInGame { get => m_pointsInGame; set => m_pointsInGame = value; }

    public static GameManager Instance { get; private set; }
    public bool TimeIsRunning { get => m_timeIsRunning; set => m_timeIsRunning = value; }

    // Start is called before the first frame update
    void Start()
    {
        //GameTime = m_gameTime;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeIsRunning) 
        {
            if (m_timeRemaining >= 0)
            {
                m_timeRemaining += Time.deltaTime;
                DisplayTime(m_timeRemaining);
            } 
        }
        if (m_timeRemaining >= GameTime) 
        {
            Time.timeScale = 0f;
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
