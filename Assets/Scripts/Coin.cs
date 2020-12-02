using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX = null;
    [SerializeField] int pointsWorth = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(pointsWorth);
        Destroy(gameObject);
        //call a method to increase the players coin count
    }
}
