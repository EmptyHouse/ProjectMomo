using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour {

#if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneToLoad;

    private void OnValidate()
    {
        if (sceneToLoad != null)
            sceneNameToLoad = sceneToLoad.name;
    }
#endif
    [Tooltip("The ID of the spawn point that our character will be placed at when the next level loads")]
    public int spawnPointID = 0;
    [Tooltip("Since we can't use SceneAsset in builds we will have to persist the name as a string.")]
    private string sceneNameToLoad;
    

    #region monobehaviour methods
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<PlayerCharacterStats>())
        {
            return;
        }
        print(LevelManager.Instance);
        LevelManager.Instance.LoadNewLevel(sceneNameToLoad, spawnPointID);
    }
    #endregion monobehaviour methods
}
