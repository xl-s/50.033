using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float moveSpeed;
    public float popImpulse;
    public AudioSource bumpAudio;

    private int moveRight;
    private bool collected = false;

    private Vector2 velocity;
    private Rigidbody2D rigidBody;
    private ConsumableInterface consumeController;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.consumeController = GetComponent<ConsumableInterface>();
        this.moveRight = 2 * Random.Range(0, 2) - 1;
        this.rigidBody.AddForce(new Vector2(this.moveRight * this.moveSpeed, this.popImpulse), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.collected) return;
        if (col.gameObject.CompareTag("Player"))
        {
            this.rigidBody.velocity = new Vector2(0, this.rigidBody.velocity.y);
            this.rigidBody.drag = 10.0f;
            this.bumpAudio.PlayOneShot(this.bumpAudio.clip);
            consumeController.Collect();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            this.collected = true;
        }
        else if (col.gameObject.CompareTag("Obstacle"))
        {
            Vector3 normal = col.contacts[0].normal;
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                this.moveRight *= -1;
                this.rigidBody.AddForce(new Vector2(this.moveRight * this.moveSpeed, 0), ForceMode2D.Impulse);
            }
        }
    }
}
