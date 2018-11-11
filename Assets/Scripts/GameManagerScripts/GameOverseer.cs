using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverseer : MonoBehaviour {
    #region enums
    public enum GameState
    {
        GamePlaying,
        MenuOpen,
    }

    #endregion enums

    #region static variables
    private static GameOverseer instance;

    public static GameOverseer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameOverseer>();
            }
            return instance;
        }
    }
    #endregion static variables

    #region main variables
    public CharacterStats playerCharacterStats;
    public GameState currentGameState { get; private set; }
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
    }
    #endregion monobehaviour methods
    /// <summary>
    /// Sets the current game state of our game. Certain actions may need to be taken for certain states
    /// </summary>
    public void SetCurrentGameState(GameState gameStateToSet)
    {
        if (gameStateToSet == this.currentGameState)
        {
            Debug.LogWarning("You are set the state " + gameStateToSet.ToString() + " when it is already the current state");
            return;
        }
        this.currentGameState = gameStateToSet;
    }

}
