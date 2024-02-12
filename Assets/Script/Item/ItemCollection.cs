using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public int coins = 0;
    public int stars = 0;
    public Text coinsText;
    public Text coinsTotalText;
    public Text coinsDefeat;
    public Text starsText;
    public Text starsTotalText;
    public Text starsCompleteText;
    public AudioClip collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            SoundManager.instance.PlaySound(collectionSoundEffect);
            Destroy(collision.gameObject);
            coins++;
            coinsText.text = coins.ToString();
            coinsTotalText.text =coins.ToString();
            coinsDefeat.text =coins.ToString();

        }

        if (collision.gameObject.CompareTag("star"))
        {
            SoundManager.instance.PlaySound(collectionSoundEffect);
            Destroy(collision.gameObject);
            stars++;
            starsText.text = stars.ToString();
            starsTotalText.text = stars.ToString();
            starsCompleteText.text = stars.ToString();
        }

    }

}
