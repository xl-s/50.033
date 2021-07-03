using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource collectAudio;

    void Start()
    {
        collectAudio = GetComponent<AudioSource>();    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectAudio.PlayOneShot(collectAudio.clip);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            CentralManager.Instance.increaseScore();
            CentralManager.Instance.spawnEnemy();
        }
    }

    IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
