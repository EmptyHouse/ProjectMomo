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

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
    }
    #endregion monobehaviour methods

    public void LoadNewLevel(string nameOfScene, int spawnPointID)
    {
        StartCoroutine(LoadNextLevelInBackground(nameOfScene));
    }

    private IEnumerator LoadNextLevelInBackground(string nameOfScene)
    {
        InGameUI.Instance.levelTransitionUIAnimator.gameObject.SetActive(true);

        AsyncOperation levelLoadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nameOfScene);
        levelLoadingOperation.allowSceneActivation = false;
        float timeThatHasPassed = 0;
        while (levelLoadingOperation.progress < .9f)
        {
            yield return null;
            timeThatHasPassed += CustomTime.UnscaledDeltaTime;
        }
        levelLoadingOperation.allowSceneActivation = true;
        while (levelLoadingOperation.progress < 1)
        {
            yield return null;
            timeThatHasPassed += CustomTime.UnscaledDeltaTime;
        }

        InGameUI.Instance.levelTransitionUIAnimator.gameObject.SetActive(false);
    }
}
