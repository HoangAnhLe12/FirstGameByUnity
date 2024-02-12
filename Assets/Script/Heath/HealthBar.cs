using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
     public Player_Life playerHealth;
     public Player_Life playerLive;
     public Image totalhealthBar;
     public Image currenthealthBar;
     public Text livesText;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
        livesText.text = playerLive.currentLive.ToString();
    }


    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
        livesText.text = playerLive.currentLive.ToString();

    }

}
