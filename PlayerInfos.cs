using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos pi;

    public int playerHealth = 3;
    public int nbCoins = 0;
    public Image[] hearts;
    public Text coinText;
    public Text scoreText;
    public CheckpointMgr chkp;


    private void Awake()
    {
        pi = this;
    }

    public void SetHealth(int val)
    {
        playerHealth += val;
        if (playerHealth > 3)
            playerHealth = 3;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        SetHealthBar();
    }

    public void GetCoin()
    {
        nbCoins++;
        coinText.text = nbCoins.ToString();
    }

    public void SetHealthBar()
    {
        //On vide la barre de vie
        foreach(Image img in hearts)
        {
            img.enabled = false;
        }

        // On met le bon nombre de coeur 
        for(int i=0; i<playerHealth; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public int GetScore()
    {
        int scorefinal = (nbCoins * 5) + (playerHealth * 10);
        scoreText.text = scoreText.text + scorefinal.ToString();
        scoreText.enabled = true;
        return scorefinal;
    }
}
