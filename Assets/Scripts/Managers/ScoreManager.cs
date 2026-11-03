using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [Header("ScoreBoardsObjects")]
    public TMP_Text[] scoreBoards;

    int[] scores = new int[2];

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        GameManager.instance.OnPointMade += AddScore;
    }
    private void OnDisable()
    {
        GameManager.instance.OnPointMade -= AddScore;
    }
    public void AddScore(int ID)
    {
        scores[ID]++;
        if (scores.Contains(5))
        {
            scoreBoards[ID].text = scores[ID].ToString();
            GameManager.instance.TriggerGameOver(ID);
            return ;
        }
        scoreBoards[ID].text = scores[ID].ToString();
    }
}
