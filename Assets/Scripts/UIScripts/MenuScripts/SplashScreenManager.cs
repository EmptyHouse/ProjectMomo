using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : SelectableUIManager {
#if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneToLoadOnStartGame;

    private void OnValidate()
    {
        sceneToLoadOnStartGameString = sceneToLoadOnStartGame.name;
    }
#endif


    private string sceneToLoadOnStartGameString;

    

    public void OnStartGameButtonPressed()
    {
        SceneManager.LoadScene(sceneToLoadOnStartGameString);
    }

    public void OnExitGameButtonPressed()
    {
        Application.Quit();
    }
}
