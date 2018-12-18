using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class that will handle enemy AI. This should not be used for NPC's that are 
/// </summary>
public class BaseEnemyStateMachine : MonoBehaviour {
    public NPCState initialNPCState;

    private NPCState currentNPCState;
    public CharacterStats associatedCharacterStats { get; private set; }

    #region monobehaviour methods
    private void Start()
    {
        associatedCharacterStats = GetComponent<CharacterStats>();
        ChangeCurrentState(initialNPCState);
    }

    private void Update()
    {
        UpdateNPCStateMachine();
    }

    private void OnValidate()
    {
        if(initialNPCState == null)
        {
            initialNPCState = ScriptableObject.CreateInstance<PatrolState>();
        }
    }
    #endregion monobehaviour methods

    /// <summary>
    /// This method will be called every frame
    /// </summary>
    private void UpdateNPCStateMachine()
    {
        if (currentNPCState != null)
        {
            currentNPCState.UpdateState();
        }
    }

    public void ChangeCurrentState(NPCState newNPCState)
    {
        if (newNPCState == null)
        {
            return;
        }

        if (currentNPCState != null)
        {
            currentNPCState.EndState();
        }
        currentNPCState = newNPCState;
        currentNPCState.StartState();
        currentNPCState.enemyStateMachine = this;
    }



    public abstract class NPCState : ScriptableObject
    {
        
        public BaseEnemyStateMachine enemyStateMachine { get; set; }
        #region state methods
        /// <summary>
        /// Any initialization that needs to occur when we start a new state should
        /// take place here
        /// </summary>
        public abstract void StartState();

        /// <summary>
        /// Anything that needs to be updated every frame when our character is in this state
        /// should take place here.
        /// </summary>
        public abstract void UpdateState();

        /// <summary>
        /// Any cleanup that should occur in this state should take place here.
        /// </summary>
        public abstract void EndState();
        #endregion state methods
    }

    

    #region custom editor

    #endregion custom editor
}
