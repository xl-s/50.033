using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameConstants gameConstants;
    
    private Rigidbody2D rigidBody;
    private Vector3 scaler;

    void Start()
    {
        this.scaler = this.transform.localScale / 30.0f;
        this.rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine("ScaleOut");
    }

    IEnumerator ScaleOut()
    {
        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        this.rigidBody.AddForce(direction.normalized * gameConstants.breakDebrisForce, ForceMode2D.Impulse);
        this.rigidBody.AddTorque(gameConstants.breakDebrisTorque, ForceMode2D.Impulse);
        yield return null;

        for (int step = 0; step < gameConstants.breakTimeStep; step++)
        {
            this.transform.localScale = this.transform.localScale - this.scaler;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
