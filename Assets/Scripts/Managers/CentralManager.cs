using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameObject powerupManagerObject;
    public GameObject spawnManagerObject;
    
    private GameManager gameManager;
    private PowerupManager powerupManager;
    private SpawnManager spawnManager;
    
    public static CentralManager Instance;

    void Awake()
    {
        CentralManager.Instance = this;
    }

    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerupManager = powerupManagerObject.GetComponent<PowerupManager>();
        spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerupManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        powerupManager.addPowerup(t, i, c);
    }

    public void spawnEnemy()
    {   
        ObjectType i = Random.value >= 0.5 ? ObjectType.goombaEnemy : ObjectType.greenEnemy;
        spawnManager.spawnFromPooler(i);
    }
}
