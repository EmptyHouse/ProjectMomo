using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour {
    #region static varaibles
    private static SpawnPool instance;
    public static SpawnPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SpawnPool>();
            }
            return instance;
        }
    }
    #endregion static variables
    private Dictionary<string, Queue<PoolableObject>> spawnPoolDictionary = new Dictionary<string, Queue<PoolableObject>>();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectToPool"></param>
    /// <param name="numberOfItemsToPool"></param>
    public void InitializeSpawnPoolWithObject(PoolableObject prefabOfObjectToPool, int numberOfItemsToPool)
    {
        string objectName = prefabOfObjectToPool.attachedMonobehaviour.name;
        if (!spawnPoolDictionary.ContainsKey(objectName))
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectToPool"></param>
    private void CreateNewObjectForPool(PoolableObject objectToPool)
    {
        string objectName = objectToPool.attachedMonobehaviour.name;

        PoolableObject newlyCreatedObject = (PoolableObject)Instantiate(objectToPool.attachedMonobehaviour);
    }

    #region handle spawning and despawning
    /// <summary>
    /// Takes in a prefab of the object that you would like to spawn in. This will get the next item to spawn pool in our queue that matches the prefab
    /// of the object that is passed in. If there is not one in the queue this method will instantiate a new one and return it to you. Please be sure to properly
    /// despawn items to avoid creating new objects as often as possible
    /// </summary>
    /// <param name="prefabOfObjectToSpawn"></param>
    public PoolableObject SpawnObject(PoolableObject prefabOfObjectToSpawn)
    {
        return null;
    }

    /// <summary>
    /// Unlike spawn, this method takes in the object that you would like to be despawned. This will deactivate the object
    /// and run the OnObjectDespawned event that is is found in all PoolableObjects
    /// </summary>
    /// <param name="objectToDespawn"></param>
    public void DespawnObject(PoolableObject objectToDespawn)
    {

    }
    #endregion handle spawning and despawning


}
