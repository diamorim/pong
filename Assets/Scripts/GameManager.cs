using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("GameOver Properties")]
    [SerializeField]private GameObject GameOverScreen;
    [SerializeField]private GameObject YouWinText;
    [SerializeField]private GameObject YouLoseText;

    public static GameManager instance;

    private void Start()
    {
        instance = this;
    }

    public void GameOver(int ID)
    {
        Time.timeScale = 0f;
        GameOverScreen.SetActive(true);
        if (ID == 0)
            YouWinText.SetActive(true);
        else
            YouLoseText.SetActive(true);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
