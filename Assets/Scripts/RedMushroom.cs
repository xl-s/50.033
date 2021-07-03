using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;
    public AudioSource expireAudio;

    public void consumedBy(GameObject player)
    {
        player.GetComponent<PlayerController>().upSpeed += 10;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().upSpeed -= 10;
        expireAudio.PlayOneShot(expireAudio.clip);
    }

    public void Collect()
    {
        CentralManager.Instance.addPowerup(t, 0, this);
    }
}
