using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private bool movable = false;

    private float speed = 20f;
    private bool soundPlayed = false;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float playerZPos = PlayerController.Ins.transform.position.z;
        float zDistance = Mathf.Abs(transform.position.z - playerZPos);

        if (movable && zDistance < 100f)
        {
            if (soundPlayed == false)
            {
                PlaySound();
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void PlaySound()
    {
        AudioClip clip = AudioManager.Ins.GetAudioClip("Car");

        if (clip != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.spatialBlend = 1.0f;
            audioSource.Play();
        }

        soundPlayed = true;
    }
}
