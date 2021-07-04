using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public AudioSource changeSceneSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changeSceneSound.PlayOneShot(changeSceneSound.clip);
            StartCoroutine(LoadYourAsyncScene("MarioScene2"));
        }
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        yield return new WaitUntil(() => !changeSceneSound.isPlaying);
        CentralManager.Instance.ChangeScene(sceneName);
    }
}
