using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX = null;
    [SerializeField] int pointsWorth = 1;

    bool isPickedUp = false;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!isPickedUp)
        {
            isPickedUp = true;
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(pointsWorth);
            Destroy(gameObject);
        }
    }
}
