using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        this.player.GetComponent<PlayerController>().Reset();
        this.enemy.GetComponent<EnemyController>().Reset();
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        Time.timeScale = 1.0f;
    }

    public void ResetMenu()
    {
        this.gameObject.SetActive(true);
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }
        Time.timeScale = 0.0f;
    }
}
