using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject cam;
    public Transform startLimit;
    public Transform endLimit;
    public float bgRange;

    private float camWidth;
    private float camRange;
    private float camLeftLimit;
    private float bgWidth;

    void Start()
    {
        Camera camera = cam.GetComponent<Camera>();
        this.camWidth = 2 * camera.orthographicSize * camera.aspect;
        this.camRange = this.endLimit.position.x - this.startLimit.position.x - this.camWidth;
        this.camLeftLimit = this.startLimit.position.x + this.camWidth / 2;
        this.bgWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    float ComputeX()
    {
        float leftX = this.cam.transform.position.x + this.bgWidth / 2 - this.camWidth / 2 - 1;
        float propTraversed = (this.cam.transform.position.x - this.camLeftLimit) / this.camRange;
        float parallaxX = leftX - Mathf.Lerp(0, bgRange, propTraversed);
        return parallaxX;
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(
            this.ComputeX(),
            this.transform.position.y,
            this.transform.position.z
        );
    }
}
