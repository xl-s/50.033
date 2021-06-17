using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float upSpeed;
    public float maxSpeed = 10;

    public Transform enemyLocation;
    public Text scoreText;
    public GameObject startMenu;
    public ParticleSystem dustCloud;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource jumpAudio;

    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool countScoreState = false;
    private int score = 0;
    private float originalX;

    void Start()
    {
        Application.targetFrameRate = 30;
        this.marioBody = GetComponent<Rigidbody2D>();
        this.marioSprite = GetComponent<SpriteRenderer>();
        this.marioAnimator = GetComponent<Animator>();
        this.jumpAudio = GetComponent<AudioSource>();
        this.originalX = this.transform.position.x;
    }

    public void Reset()
    {
        this.score = 0;
        this.scoreText.text = $"SCORE: {this.score.ToString().PadLeft(4, '0')}";
        this.marioBody.velocity = Vector2.zero;
        this.marioBody.MovePosition(new Vector2(this.originalX, 0.0f));
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            if (this.marioBody.velocity.magnitude < maxSpeed)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0.0f);
                this.marioBody.AddForce(movement * this.speed);
            }
        }

        if (Mathf.Abs(moveHorizontal) < 0.1)
        {
            this.marioBody.velocity = new Vector2(0.0f, this.marioBody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && this.onGroundState)
        {
            this.marioBody.AddForce(Vector2.up * this.upSpeed, ForceMode2D.Impulse);
            this.onGroundState = false;
            this.countScoreState = true;
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal < 0 && this.faceRightState)
        {
            this.faceRightState = false;
            this.marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.01) this.marioAnimator.SetTrigger("onSkid");
        }

        if (moveHorizontal > 0 && !this.faceRightState)
        {
            this.faceRightState = true;
            this.marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.01) this.marioAnimator.SetTrigger("onSkid");
        }

        if (!this.onGroundState && this.countScoreState)
        {
            if (Mathf.Abs(this.transform.position.x - this.enemyLocation.position.x) < 0.5f)
            {
                this.countScoreState = false;
                this.score++;
            }
        }

        this.marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        this.marioAnimator.SetBool("onGround", onGroundState);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground") || collision2D.gameObject.CompareTag("Obstacle"))
        {
            this.onGroundState = true;
            this.countScoreState = false;
            this.scoreText.text = $"SCORE: {this.score.ToString().PadLeft(4, '0')}";

            this.dustCloud.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.startMenu.GetComponent<MenuController>().ResetMenu();
        }
    }

    void PlayJumpSound()
    {
        this.jumpAudio.PlayOneShot(this.jumpAudio.clip);
    }
}
