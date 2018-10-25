using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for our hitboxes
/// </summary>
public abstract class Hitbox : MonoBehaviour {
    public bool hitboxActive { get; set; }
    [System.NonSerialized]
    /// <summary>
    /// 
    /// </summary>
    public HitboxManager associateMeleeMechanics;

    #region monobehaiovur methods
    protected virtual void Awake()
    {
        associateMeleeMechanics.AddAssociatedHitbox(this);
    }

    protected virtual void OnDestroy()
    {
        associateMeleeMechanics.RemoveAssociatedHitbox(this);
    }

    
    #endregion monobehaviour methods

    /// <summary>
    /// This method will be called anytime 
    /// </summary>
    /// <param name="hitboxThatWasEntered"></param>
    public void OnEnteredHitbox(Hitbox hitboxThatWasEntered)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitboxThatWasExited"></param>
    public void OnExitHitbox(Hitbox hitboxThatWasExited)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtboxThatWasEntered"></param>
    public void OnEnteredHurtbox(Hurtbox hurtboxThatWasEntered)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtboxThatWasExited"></param>
    public void OnExitHurtbox(Hurtbox hurtboxThatWasExited)
    {

    }
}
