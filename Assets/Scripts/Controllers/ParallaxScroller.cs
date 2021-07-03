using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public Renderer[] layers;
    public float[] speedMultiplier;
    public Transform mario;
    public Transform mainCamera;

    private float previousXPositionMario;
    private float previousXPositionCamera;
    private float[] offset;


    void Start()
    {
        this.offset = new float[this.layers.Length];
        for (int i = 0; i < this.layers.Length; i++)
        {
            this.offset[i] = 0.0f;
        }

        this.previousXPositionMario = this.mario.transform.position.x;
        this.previousXPositionCamera = this.mainCamera.transform.position.x;
    }

    void Update()
    {
        if (Mathf.Abs(this.previousXPositionCamera - this.mainCamera.transform.position.x) > 0.001f)
        {
            for (int i = 0; i < this.layers.Length; i++)
            {
                if (this.offset[i] > 1.0f || offset[i] < -1.0f)
                {
                    offset[i] = 0.0f;
                }
                float newOffset = this.mario.transform.position.x - this.previousXPositionMario;
                this.offset[i] = this.offset[i] + newOffset * this.speedMultiplier[i];
                this.layers[i].material.mainTextureOffset = new Vector2(this.offset[i], 0);
            }
        }

        this.previousXPositionMario = this.mario.transform.position.x;
        this.previousXPositionCamera = this.mainCamera.transform.position.x;
    }
}
