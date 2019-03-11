using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Overseer object. Manages important references in the game
/// </summary>
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
    public Utilities.SceneField sceneToLoadOnQuitGame;
    public PlayerCharacterStats playerCharacterStats;
    public GameState currentGameState { get; private set; }
    public Dictionary<CustomTime.TimeLayer, List<TimeManagedObject>> allTimeMangedObjectDictionary { get; private set; }

    public List<GameObject> listOfObjectsInDontDestroyOnLoad { get; private set; }
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        
        instance = this;
        if (allTimeMangedObjectDictionary == null)
        {
            InitializeTimeList();
        }

        AddObjectToDontDestroyOnLoad(this.gameObject);
    }
    #endregion monobehaviour methods

    #region time mangement methods
    private void InitializeTimeList()
    {
        allTimeMangedObjectDictionary = new Dictionary<CustomTime.TimeLayer, List<TimeManagedObject>>();
        foreach (CustomTime.TimeLayer tLayer in System.Enum.GetValues(typeof(CustomTime.TimeLayer)))
        {
            allTimeMangedObjectDictionary.Add(tLayer, new List<TimeManagedObject>());
        }
    }

    /// <summary>
    /// Add a time managed object to our Game Overseer
    /// </summary>
    /// <param name="timeObjectToAdd"></param>
    public void AddTimeMangedObjectToList(TimeManagedObject timeObjectToAdd)
    {
        if (timeObjectToAdd == null)
        {
            return;
        }

        if (allTimeMangedObjectDictionary == null) InitializeTimeList();

        List<TimeManagedObject> timeManagedObjectList = allTimeMangedObjectDictionary[timeObjectToAdd.timeLayer];

        if (timeManagedObjectList.Contains(timeObjectToAdd))
        {
            Debug.LogWarning("This object was already added to our list of time management objects");
            return;
        }
        timeManagedObjectList.Add(timeObjectToAdd);
    }

    /// <summary>
    /// Remove a time Manged object. This should happen if we despawn or destroy an object
    /// </summary>
    /// <param name="timeObjectToRemove"></param>
    public void RemoveTimeManagedObjectFromList(TimeManagedObject timeObjectToRemove)
    {
        if (timeObjectToRemove == null)
        {
            return;
        }

        List<TimeManagedObject> timeManagedObjectList = allTimeMangedObjectDictionary[timeObjectToRemove.timeLayer];
        if (!timeManagedObjectList.Contains(timeObjectToRemove))
        {
            Debug.LogWarning("The TimeManagedObject you are attempting to remove was not found.");
            return;
        }
        timeManagedObjectList.Remove(timeObjectToRemove);
    }

    /// <summary>
    /// Returns a list of time managed items that are in the timeLayer
    /// </summary>
    /// <param name="timeLayer"></param>
    /// <returns></returns>
    public List<TimeManagedObject> GetTimeMangedList(CustomTime.TimeLayer timeLayer)
    {
        return allTimeMangedObjectDictionary[timeLayer];
    }
    #endregion time management methods
    /// <summary>
    /// Sets the current game state of our game. Certain actions may need to be taken for certain states
    /// </summary>
    public void SetCurrentGameState(GameState gameStateToSet)
    {
        if (gameStateToSet == this.currentGameState)
        {
            Debug.LogWarning("You have set the state " + gameStateToSet.ToString() + " when it is already the current state");
            return;
        }
        this.currentGameState = gameStateToSet;
    }

    /// <summary>
    /// In some cases we will want to persist the objects that are persistent throughout different levels
    /// For example the in game UI and the game overseer should be persisted
    /// </summary>
    /// <param name="gameObjectToAddToDontDestroyOnLoad"></param>
    public void AddObjectToDontDestroyOnLoad(GameObject gameObjectToAddToDontDestroyOnLoad)
    {
        if (listOfObjectsInDontDestroyOnLoad == null)
        {
            listOfObjectsInDontDestroyOnLoad = new List<GameObject>();
        }
        DontDestroyOnLoad(gameObjectToAddToDontDestroyOnLoad);
        listOfObjectsInDontDestroyOnLoad.Add(gameObjectToAddToDontDestroyOnLoad);
    }

    /// <summary>
    /// 
    /// </summary>
    public void DestroyAllGameObjectsInDontDestroyOnLoad()
    {
        if (listOfObjectsInDontDestroyOnLoad == null) return;

        foreach (GameObject objectToDestroy in listOfObjectsInDontDestroyOnLoad)
        {
            Destroy(objectToDestroy);
        }
    }

    public void QuitGameAndReturnToMainMenu()
    {
        DestroyAllGameObjectsInDontDestroyOnLoad();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoadOnQuitGame);
    }
}
