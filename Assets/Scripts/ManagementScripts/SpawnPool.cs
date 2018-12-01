using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour {
    #region static varaibles

    public static SpawnPool Instance;
    #endregion static variables



    private Dictionary<string, List<PoolableObject>> spawnPoolDictionary = new Dictionary<string, List<PoolableObject>>();


    
}
