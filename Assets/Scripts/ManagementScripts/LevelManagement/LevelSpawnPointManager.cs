using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnPointManager : MonoBehaviour {
    public Transform[] listOfAllSpawnPoints = new Transform[0];

    private void Awake()
    {
        LevelManager.Instance.levelSpawnPointManager = this;
    }

    #region monobehaviour methods
    private void OnDrawGizmosSelected()
    {
        foreach (Transform t in listOfAllSpawnPoints)
        {
            if (t == null)
            {
                continue;
            }
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(t.position, .2f);
        }
    }
    #endregion monobehaviour methods
}
