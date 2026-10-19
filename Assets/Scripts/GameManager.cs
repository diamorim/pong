using System;
using UnityEngine;

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
}
