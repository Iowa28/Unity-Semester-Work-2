using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;

    [SerializeField] private int enemyCount;
    [SerializeField] private TextMeshProUGUI infoText;

    private void Start()
    {
        infoText.text = "Enemy left: " + enemyCount;
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ReduceEnemyCount()
    {
        enemyCount--;
        
        if (enemyCount <= 0)
        {
            infoText.text = "You win!";
        }
        else
        {
            infoText.text = "Enemy left: " + enemyCount;
        }
    }
}
