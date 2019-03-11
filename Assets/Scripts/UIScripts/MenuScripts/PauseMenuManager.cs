using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PauseMenuManager : MonoBehaviour {

    private static PauseMenuManager instance;

    public static PauseMenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PauseMenuManager>();
            }
            return instance;
        }
    }
    #region main variables

    public Transform pauseMenuContainer;

    /// <summary>
    /// The string name of the scene that we will load if the player chooses to quit the game
    /// </summary>
    private string quitGameSceneToLoadName;
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
        this.enabled = false;
        this.pauseMenuContainer.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonUp(PlayerController.PAUSE_GAME_INPUT))
        {
            StartCoroutine(CloseAfterOneFrame());
        }
    }
    #endregion monobehaviour methods

    /// <summary>
    /// OPens the pause menu and stops the game from progressing. This should block all player input and 
    /// </summary>
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        GameOverseer.Instance.SetCurrentGameState(GameOverseer.GameState.MenuOpen);
        pauseMenuContainer.gameObject.SetActive(true);
        this.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        GameOverseer.Instance.SetCurrentGameState(GameOverseer.GameState.GamePlaying);
        pauseMenuContainer.gameObject.SetActive(false);
        this.enabled = false;
    }

    #region event methods
    /// <summary>
    /// 
    /// </summary>
    public void OnResumeButtonPressed()
    {
        StartCoroutine(CloseAfterOneFrame());
    }

    public void OnQuitGameButtonPressed()
    {
        Time.timeScale = 1;
        GameOverseer.Instance.QuitGameAndReturnToMainMenu();
        
    }

    public void OnOptionsButtonPressed()
    {

    }
    #endregion event methods

    private IEnumerator CloseAfterOneFrame()
    {
        yield return null;
        ClosePauseMenu();
    }
}
