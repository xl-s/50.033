using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableTriggerChecker : MonoBehaviour
{
    public Powerup stats;
    public CustomPowerupEvent onCollected;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            onCollected.Invoke(stats);
        }
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
