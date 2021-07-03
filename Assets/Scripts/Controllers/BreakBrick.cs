using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public GameObject debris;
    public GameObject coin;
    public GameConstants gameConstants;

    private AudioSource breakAudio;
    private bool broken = false;

    void Start()
    {
        this.breakAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !this.broken)
        {
            this.broken = true;
            this.breakAudio.PlayOneShot(this.breakAudio.clip);
            for (int x = 0; x < gameConstants.breakBrickNumDebris; x++)
            {
                Instantiate(debris, this.transform.position, Quaternion.identity);
            }
            this.gameObject.transform.parent.Find("TopCollider").GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;

            Instantiate(coin, this.transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity);
        }
    }

}
