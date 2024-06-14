using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(100f * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    Destroy(gameObject);
        //
        //    if (coinSoundEffect != null)
        //    {
        //        AudioSource.PlayClipAtPoint(coinSoundEffect, transform.position, 0.5f);
        //    }
        //}
    }
}
