using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Lives : MonoBehaviour
{
    public static int healthValue;
    TMPro.TextMeshProUGUI health;
    public GameObject endMenu;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        healthValue = 5;
        gameOver = false;
        health = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = healthValue.ToString();
        if (healthValue <= 0 && gameOver == false)
        {
            health.text = healthValue.ToString();
            endMenu.SetActive(true);
            Time.timeScale = 0f;
            gameOver = true;
        }
        if (gameOver)
        {
            healthValue = 0;
        }
    }
}
