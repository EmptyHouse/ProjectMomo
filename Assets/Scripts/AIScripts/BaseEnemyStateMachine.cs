using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class that will handle enemy AI. This should not be used for NPC's that are 
/// </summary>
public class BaseEnemyStateMachine : MonoBehaviour {
    public CharacterStats associatedCharacterStats;
    public NPCState initialNPCState;

    private NPCState currentNPCState;

    #region monobehaviour methods
    private void Start()
    {
        ChangeCurrentState(initialNPCState);
    }

    private void Update()
    {
        UpdateNPCStateMachine();
    }
    #endregion monobehaviour methods

    /// <summary>
    /// Method to start a new state machine
    /// </summary>
    public void StartNPCStateMachine()
    {

    }

    /// <summary>
    /// This method will be called every frame
    /// </summary>
    private void UpdateNPCStateMachine()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void EndNPCStateMachine()
    {

    }

    public void ChangeCurrentState(NPCState updatedState)
    {
        if (updatedState == null)
        {
            return;
        }

        if (currentNPCState != null)
        {
            currentNPCState.EndState();
        }
        currentNPCState = updatedState;
        currentNPCState.StartState();
    }



    public abstract class NPCState
    { 
        #region state methods
        /// <summary>
        /// Any initialization that needs to occur when we start a new state should
        /// take place here
        /// </summary>
        public void StartState()
        {

        }

        /// <summary>
        /// Anything that needs to be updated every frame when our character is in this state
        /// should take place here.
        /// </summary>
        public void UpdateState()
        {

        }

        /// <summary>
        /// Any cleanup that should occur in this state should take place here.
        /// </summary>
        public void EndState()
        {

        }
        #endregion state methods
    }
}
