using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Banana : MonoBehaviour
{
    private float fruits = 0;
    [SerializeField] private Text Bananas;

    [SerializeField] private AudioClip collectionSound;
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bananas"))
        {
            SoundManager.instance.PlaySound(collectionSound);
            Destroy(collision.gameObject);
            fruits++;
            Bananas.text = "Bananas: " + fruits;
        }
    }
}
