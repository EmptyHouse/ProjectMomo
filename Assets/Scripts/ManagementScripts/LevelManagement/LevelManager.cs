using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script will control the process of transitioning to a different scene. This will handle basic transitions. Like moving to different portions of the level
/// </summary>
public class LevelManager : MonoBehaviour {
    #region static variables
    private static LevelManager instance;


    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }
    #endregion static variables

    #region const variables
    public const string LEVEL_EXIT_TRIGGER = "ExitLevelTrigger";
    public const string LEVEL_ENTER_TRIGGER = "EnterLevelTrigger";
    #endregion const variables

    #region main variables
    public LevelSpawnPointManager levelSpawnPointManager;
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
    }
    #endregion monobehaviour methods
    /// <summary>
    /// This will load a new level and place the player in the spawn point id that is passed in with the spawnPointID value
    /// </summary>
    /// <param name="nameOfScene"></param>
    /// <param name="spawnPointID"></param>
    /// <param name="instantlyLoad"></param>
    public void LoadNewLevel(string nameOfScene, int spawnPointID, bool instantlyLoad = false)
    {
        StartCoroutine(LoadNextLevelInBackground(nameOfScene, spawnPointID));
    }

    private IEnumerator LoadNextLevelInBackground(string nameOfScene, int spawnPointID)
    {
        GameOverseer.Instance.playerCharacterStats.playerController.enabled = false;
        InGameUI.Instance.levelTransitionUIAnimator.gameObject.SetActive(true);
        InGameUI.Instance.levelTransitionUIAnimator.SetTrigger(LEVEL_EXIT_TRIGGER);

        AsyncOperation levelLoadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nameOfScene);
        levelLoadingOperation.allowSceneActivation = false;
        float timeThatHasPassed = 0;
        while (levelLoadingOperation.progress < .9f)
        {
            yield return null;
            timeThatHasPassed += CustomTime.UnscaledDeltaTime;
        }
        yield return new WaitForSeconds(Mathf.Max(.5f - timeThatHasPassed, 0));
        levelLoadingOperation.allowSceneActivation = true;
        while (levelLoadingOperation.progress < 1)
        {
            yield return null;
            timeThatHasPassed += CustomTime.UnscaledDeltaTime;
        }

        if (levelSpawnPointManager)
        {
            GameOverseer.Instance.playerCharacterStats.transform.position = levelSpawnPointManager.listOfAllSpawnPoints[spawnPointID].position;
        }
        
        InGameUI.Instance.levelTransitionUIAnimator.SetTrigger(LEVEL_ENTER_TRIGGER);
        yield return new WaitForSeconds(.5f);

        InGameUI.Instance.levelTransitionUIAnimator.gameObject.SetActive(false);
        GameOverseer.Instance.playerCharacterStats.playerController.enabled = true;
    }
}
