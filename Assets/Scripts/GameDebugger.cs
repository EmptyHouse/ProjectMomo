using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// In this method we will have access to a bunch of debuggin methods that will help us along with development to
/// ensure that everything is being implemented correctly
/// </summary>
public class GameDebugger : MonoBehaviour {
    #region static variables
    private static GameDebugger instance;
    public static GameDebugger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameDebugger>();
            }
            return instance;
        }
    }
    #endregion static variables


    public bool displayCharacterHealthbars = false;
	
}
