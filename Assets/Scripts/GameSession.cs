using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int playerLives = 1;

    [SerializeField]
    Text ColorAmount;

    public int color;

    void Start()
    {   
        color = 0;
        ColorAmount.text = color.ToString();
    }

    public void AddColorAmount(int amount)
    {
        color = color + amount;
        ColorAmount.text = color.ToString();
    }

    public void SubtractColorAmount(int amount)
    {
        color = color - amount;
        ColorAmount.text = color.ToString();
    }

    

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            Invoke("ResetGameSession", 2f);
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives = playerLives - 1;
    }
}
