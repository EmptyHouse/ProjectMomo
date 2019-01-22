using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class that will handle enemy AI. This should not be used for NPC's that are 
/// </summary>
public class BaseEnemyStateMachine : MonoBehaviour {
    public CharacterStats associatedCharacterStats;
    public EnemyState currentState { get; private set; }

    #region monobehaviour methods
    private void Awake()
    {

    }

    private void Update()
    {
        currentState.UpdateState(CustomTime.GetTimeLayerAdjustedDeltaTime(associatedCharacterStats.timeManagedObject.timeLayer));
    }
    #endregion monobehaviour methods


    public void SetNewState(EnemyState newEnemyState)
    {
        if (currentState != null)
        {
            currentState.EndState();
        }
        currentState = newEnemyState;
        currentState.StartState();
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class EnemyState
    {
        public abstract void UpdateState(float deltaTime);
        public abstract void StartState();
        public abstract void EndState();
    }
}
