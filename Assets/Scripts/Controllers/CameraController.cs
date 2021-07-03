using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform startLimit;
    public Transform endLimit;

    private float halfWidth;
    private float y;
    private float z;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        this.halfWidth = cam.orthographicSize * cam.aspect;
        this.y = this.transform.position.y;
        this.z = this.transform.position.z;
    }

    float Clip(float pos)
    {
        pos = Mathf.Max(this.startLimit.position.x + this.halfWidth, pos);
        pos = Mathf.Min(this.endLimit.position.x - this.halfWidth, pos);
        return pos;
    }

    void Update()
    {
        this.transform.position = new Vector3(
            Clip(playerTransform.position.x),
            this.y,
            this.z
        );
    }
}
