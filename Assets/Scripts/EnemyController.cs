using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f;
    private float patrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;

    void Start()
    {
        this.enemyBody = GetComponent<Rigidbody2D>();
        this.enemySprite = GetComponent<SpriteRenderer>();
        this.originalX = this.transform.position.x;
        this.ComputeVelocity();
    }

    public void Reset()
    {
        this.enemyBody.position = new Vector2(this.originalX, -0.46f);
        this.moveRight = -1;
        this.ComputeVelocity();
    }

    void ComputeVelocity()
    {
        this.velocity = new Vector2(this.moveRight * this.maxOffset / this.patrolTime, 0);
        this.enemySprite.flipX = (this.moveRight == -1);
    }

    void MoveGoomba()
    {
        this.enemyBody.MovePosition(this.enemyBody.position + this.velocity * Time.deltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(this.enemyBody.position.x - this.originalX) < this.maxOffset)
        {
            this.MoveGoomba();
        }
        else
        {
            this.moveRight *= -1;
            this.ComputeVelocity();
            this.MoveGoomba();
        }
    }
}
