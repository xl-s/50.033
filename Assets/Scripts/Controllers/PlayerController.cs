using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float upSpeed;
    public float maxSpeed = 10;

    public GameObject startMenu;
    public ParticleSystem dustCloud;
    public AudioSource jumpAudio;
    public AudioSource dieAudio;
    public AudioSource bgmAudio;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;

    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool isDead = false;
    private float originalX;

    private float deathDelta = 0.5f;

    void Start()
    {
        Application.targetFrameRate = 30;
        this.marioBody = GetComponent<Rigidbody2D>();
        this.marioSprite = GetComponent<SpriteRenderer>();
        this.marioAnimator = GetComponent<Animator>();
        this.originalX = this.transform.position.x;
        GameManager.OnPlayerDeath += Deaded;
    }

    public void Reset()
    {
        this.marioBody.velocity = Vector2.zero;
        this.marioBody.MovePosition(new Vector2(this.originalX, 0.0f));
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            marioBody.position += new Vector2(0, deathDelta);
            deathDelta -= 0.01f;
        }
        else
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
            }
        }
    }

    void Update()
    {
        if (isDead) return;
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

        this.marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        this.marioAnimator.SetBool("onGround", onGroundState);

        if (Input.GetKeyDown("z"))
        {
            CentralManager.Instance.consumePowerup(KeyCode.Z, this.gameObject);
        }
        if (Input.GetKeyDown("x"))
        {
            CentralManager.Instance.consumePowerup(KeyCode.X, this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (isDead) return;

        if (collision2D.gameObject.CompareTag("Ground") || collision2D.gameObject.CompareTag("Obstacle"))
        {
            this.onGroundState = true;

            this.dustCloud.Play();
        }
    }

    void PlayJumpSound()
    {
        this.jumpAudio.PlayOneShot(this.jumpAudio.clip);
    }

    void Deaded()
    {
        if (isDead) return;

        isDead = true;
        marioAnimator.SetBool("isDead", true);

        dieAudio.PlayOneShot(dieAudio.clip);

        marioBody.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        bgmAudio.Stop();
    }

    IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
