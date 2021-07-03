using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;

    private float originalX;
    private bool isDead = false;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private AudioSource flattenAudio;

    private bool playerDead;
    private float enemyRejoiceDelta = 0.5f;

    void Start()
    {
        this.enemyBody = GetComponent<Rigidbody2D>();
        this.enemySprite = GetComponent<SpriteRenderer>();
        this.flattenAudio = GetComponent<AudioSource>();
        this.originalX = this.transform.position.x;
        this.ComputeVelocity();
        GameManager.OnPlayerDeath += EnemyRejoice;
    }

    void OnEnable()
    {
        this.transform.localScale = Vector3.one;
        isDead = false;
    }

    public void Reset()
    {
        this.enemyBody.position = new Vector2(this.originalX, -0.46f);
        this.moveRight = -1;
        this.ComputeVelocity();
    }

    void ComputeVelocity()
    {
        this.velocity = new Vector2(this.moveRight * gameConstants.enemyMaxOffset / gameConstants.enemyPatrolTime, 0);
        this.enemySprite.flipX = (this.moveRight == -1);
    }

    void MoveGoomba()
    {
        this.enemyBody.MovePosition(this.enemyBody.position + this.velocity * Time.deltaTime);
    }

    void Update()
    {
        if (playerDead)
        {
            if (enemyBody.position.y <= -0.45)
            {
                enemyRejoiceDelta = 0.5f;
            }

            enemyRejoiceDelta -= 0.05f;

            enemyBody.position += new Vector2(0, enemyRejoiceDelta);
        }
        else
        {
            if (Mathf.Abs(this.enemyBody.position.x - this.originalX) < gameConstants.enemyMaxOffset)
            {
                this.MoveGoomba();
            }
            else if (this.enemyBody.position.x - this.originalX > gameConstants.enemyMaxOffset)
            {
                this.moveRight = -1;
                this.ComputeVelocity();
            }
            else if (this.originalX - this.enemyBody.position.x > gameConstants.enemyMaxOffset)
            {
                this.moveRight = 1;
                this.ComputeVelocity();
            }
            this.MoveGoomba();
        }
    }

    void GoodbyeCruelWorld()
    {
        if (isDead) return;
        isDead = true;
        CentralManager.Instance.increaseScore();
        this.flattenAudio.PlayOneShot(this.flattenAudio.clip);
        CentralManager.Instance.spawnEnemy();
        StartCoroutine(flatten());
    }

    IEnumerator flatten()
    {
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(
                this.transform.localScale.x,
                this.transform.localScale.y - stepper,
                this.transform.localScale.z
            );
            this.transform.position = new Vector3(
                this.transform.position.x,
                gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y,
                this.transform.position.z
            );
            yield return null;
        }

        this.gameObject.SetActive(false);
        yield break;
    }

    void EnemyRejoice()
    {
        playerDead = true;
        // jump up and down animation
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float yoffset = other.transform.position.y - this.transform.position.y;
            if (yoffset > 0.75f)
            {
                GoodbyeCruelWorld();
            }
            else
            {
                CentralManager.Instance.damagePlayer();
            }
        }
    }
}
