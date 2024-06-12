using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private AudioClip coinSoundEffect;

    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collected)
        {
            transform.Rotate(0, 0, 0);
            transform.Translate(0, 0.05f, 0);
        }
        else
        {
            transform.Rotate(0, 1f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collected = true;

            if (coinSoundEffect != null)
            {
                AudioSource.PlayClipAtPoint(coinSoundEffect, transform.position, 0.5f);
            }
        }
    }
}
