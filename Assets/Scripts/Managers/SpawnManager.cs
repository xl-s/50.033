using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;

    void Awake()
    {
        spawnFromPooler(ObjectType.goombaEnemy);
        spawnFromPooler(ObjectType.greenEnemy);
    }

    public void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            item.transform.position = new Vector3(Random.Range(gameConstants.spawnMinX, gameConstants.spawnMaxX), -0.45f, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool,");
        }
    }

    public void resetAll()
    {
        ObjectPooler.SharedInstance.DeactivateAllObjects();
    }
}
