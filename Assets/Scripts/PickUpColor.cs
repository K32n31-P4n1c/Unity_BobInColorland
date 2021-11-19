using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpColor : MonoBehaviour
{
    [SerializeField]
    int ColorAmount;

    [SerializeField]
    AudioClip colorPickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        
        FindObjectOfType<GameSession>().AddColorAmount(ColorAmount);

        AudioSource.PlayClipAtPoint(colorPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
