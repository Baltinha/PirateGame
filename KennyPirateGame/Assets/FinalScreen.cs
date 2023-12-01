using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private Image Menu;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ResetGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.TimeIsRunning = true;
        Time.timeScale = 1f;

    }

    public void GoToMenu() 
    {
        this.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
    }
}
