using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;

public class CoinKeeper : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    public Canvas canvas;
    public TMP_Text coinsText;
    private float coins = 0;
   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
            if (coll.gameObject.tag == "Coin")
            {
                audioSource.PlayOneShot(audioClip);
                coins++;
                coinsText.text = coins.ToString();
                Destroy(coll.gameObject);
            }
    }
}
