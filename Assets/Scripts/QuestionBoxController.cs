using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;
    
    private AudioSource coinAudio;
    private bool hit = false;

    void Start()
    {
        this.coinAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !this.hit)
        {
            this.hit = true;
            this.rigidBody.AddForce(new Vector2(0, this.rigidBody.mass * 30), ForceMode2D.Impulse);
            Instantiate(
                this.consummablePrefab,
                new Vector3(
                    this.transform.position.x,
                    this.transform.position.y + 1.0f,
                    this.transform.position.z
                ),
                Quaternion.identity
            );
            this.coinAudio.PlayOneShot(this.coinAudio.clip);
            StartCoroutine(this.DisableHittable());
        }
    }

    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(this.rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!this.ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => this.ObjectMovedAndStopped());
        }

        this.spriteRenderer.sprite = this.usedQuestionBox;
        this.rigidBody.bodyType = RigidbodyType2D.Static;

        this.transform.localPosition = Vector3.zero;
        this.springJoint.enabled = false;
    }
}
