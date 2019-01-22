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

    public void LoadNewLevel()
    {

    }
}
