using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : SelectableUIManager {

    public Utilities.SceneField sceneToLoadOnStartGame;

    
    /// <summary>
    /// Call this method when you press the start game button in the splash screen
    /// </summary>
    public void OnStartGameButtonPressed()
    {
        SceneManager.LoadScene(sceneToLoadOnStartGame);
    }


    /// <summary>
    /// Call this method when you press the quit game button in the splash screen
    /// </summary>
    public void OnExitGameButtonPressed()
    {
        Application.Quit();
    }
}
