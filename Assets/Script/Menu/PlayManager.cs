using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    int characterIndex;
    public static Vector2 lastCheckPointPos = new    Vector2 (66.04f, -5.58f);//Vector2(223.4589f, -5.635663f);
    public CinemachineVirtualCamera VCam;
    private ItemCollection itemCollection;
    private Player_Life playerHealth;
    [SerializeField] private Text CoinCount;
    [SerializeField] private Text CoinTotal;
    [SerializeField] private Text CoinDefeat;
    [SerializeField] private Text starsCount;
    [SerializeField] private Text starsComplete;
    [SerializeField] private Image HeathToTal;
    [SerializeField] private Image HealthCurrent;
    [SerializeField] private Text lives;

    private static bool created = false; // Check if the PlayManager is created

    private void Awake()
    {
        if (!created)
        {
            // This ensures that the PlayManager persists between scenes
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // If another PlayManager already exists, destroy this one
            Destroy(gameObject);
        }
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex],lastCheckPointPos,Quaternion.identity);
        VCam.m_Follow = player.transform;
        // Attach ItemCollection script to the player (assuming it's not already attached)
         itemCollection = player.GetComponent<ItemCollection>();
        if (itemCollection == null)
        {
            itemCollection = player.AddComponent<ItemCollection>();
        }
        // Set the UI text for coin and star collection
        itemCollection.coinsText = CoinCount;
        itemCollection.coinsTotalText = CoinTotal;
        itemCollection.coinsDefeat = CoinDefeat;
        itemCollection.starsText = starsCount;
        itemCollection.starsCompleteText = starsComplete;
        // Replace with your actual UI text component

        HealthBar healthBar = player.GetComponent<HealthBar>();
        if (healthBar == null)
        {
            healthBar = player.AddComponent<HealthBar>();
        }

        // Set references in the HealthBar script
        healthBar.playerHealth = player.GetComponent<Player_Life>();
        healthBar.playerLive = player.GetComponent<Player_Life>();
        healthBar.totalhealthBar = HeathToTal; // Replace with your actual Image reference
        healthBar.currenthealthBar = HealthCurrent; // Replace with your actual Image reference
        healthBar.livesText = lives; // Replace with your actual Text reference
    }

}
