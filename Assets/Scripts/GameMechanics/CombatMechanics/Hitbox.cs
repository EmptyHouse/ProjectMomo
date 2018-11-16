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
    public MeleeMechanics associateMeleeMechanics;
    public delegate void HitboxCollisionEnteredEvent(CharacterStats stats, Vector3 pointOfImpact);
    public event HitboxCollisionEnteredEvent onHitboxCollisionEnteredEvent;

    #region monobehaiovur methods
    protected virtual void Awake()
    {
        //associateMeleeMechanics.AddAssociatedHitbox(this);
    }

    protected virtual void OnDestroy()
    {
        if (associateMeleeMechanics != null)
        {
            associateMeleeMechanics.RemoveAssociatedHitbox(this);
        }
        
    }

    
    #endregion monobehaviour methods

    /// <summary>
    /// This method will be called anytime 
    /// </summary>
    /// <param name="hitboxThatWasEntered"></param>
    public virtual void OnEnteredHitbox(Hitbox hitboxThatWasEntered)
    {
        onHitboxCollisionEnteredEvent(null, Vector3.zero);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitboxThatWasExited"></param>
    public virtual void OnExitHitbox(Hitbox hitboxThatWasExited)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtboxThatWasEntered"></param>
    public virtual void OnEnteredHurtbox(Hurtbox hurtboxThatWasEntered)
    {
        onHitboxCollisionEnteredEvent(null, Vector3.zero);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtboxThatWasExited"></param>
    public virtual void OnExitHurtbox(Hurtbox hurtboxThatWasExited)
    {

    }

    public void HitboxEnteredEvent(CharacterStats stats, Vector3 pointOfImpact)
    {
        onHitboxCollisionEnteredEvent(stats, pointOfImpact);
    }
}
