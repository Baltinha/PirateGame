using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Configs")]
    [SerializeField] private TMP_Text m_gameTimer;
    [SerializeField] private Image m_options;
    [SerializeField] private SpawnEnemy m_spawnEnemy;
    [SerializeField] private TextMeshProUGUI m_slideTimerText;
    [SerializeField] private TextMeshProUGUI m_slideEnemyText;
    [SerializeField] private Slider m_sliderTimer;
    [SerializeField] private Slider m_sliderEnemy;

    public void StarGame() 
    {
        this.gameObject.SetActive(false);
        m_gameTimer.gameObject.SetActive(true);
        SpawnEnemy.Instance.IsSpawned = true;
        GameManager.Instance.TimeIsRunning = true;
    }

    public void OptionsScreen() 
    {
        this.gameObject.SetActive(false);
        m_options.gameObject.SetActive(true);
    }

    public void BackMenu() 
    {
        this.gameObject.SetActive(true);
        m_options.gameObject.SetActive(false);

    }

    public void SetGameSessionTime(float Time) 
    {
        GameManager.Instance.GameTime = Time;
        m_sliderTimer.onValueChanged.AddListener((v) =>
            m_slideTimerText.text = v.ToString("0.0"));
    }
    public void SetEnemySpawnTime(float Time)
    {
        m_spawnEnemy.SpawnPointRate = Time;
        m_spawnEnemy.TimeTemp = Time;
        m_sliderEnemy.onValueChanged.AddListener((v) =>
            m_slideEnemyText.text = v.ToString("0"));
    }
}
