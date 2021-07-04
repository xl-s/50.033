using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameObject spawnManagerObject;
    public GameObject playerObject;
    
    private GameManager gameManager;
    private SpawnManager spawnManager;
    
    public static CentralManager Instance;

    void Awake()
    {
        CentralManager.Instance = this;
    }

    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void resetScore()
    {
        gameManager.resetScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void spawnEnemy()
    {   
        ObjectType i = Random.value >= 0.5 ? ObjectType.goombaEnemy : ObjectType.greenEnemy;
        spawnManager.spawnFromPooler(i);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        playerObject.GetComponent<PlayerController>().moveScene(sceneName);
        spawnManager.resetAll();
    }
}
