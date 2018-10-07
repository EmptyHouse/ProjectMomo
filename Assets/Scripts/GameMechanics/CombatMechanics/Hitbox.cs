using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour {
    public bool hitboxActive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    private HitboxManager associateMeleeMechanics;

    #region monobehaiovur methods
    protected virtual void Awake()
    {
        associateMeleeMechanics.AddAssociatedHitbox(this);
    }

    protected virtual void OnDestroy()
    {
        associateMeleeMechanics.RemoveAssociatedHitbox(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (!hitboxActive)
        {
            return;
        }
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox && hitbox.associateMeleeMechanics != this.associateMeleeMechanics)
        {
            OnEnteredHitbox(hitbox);
        }
        if (hurtbox && hurtbox.associatedMeleeMechanics != this.associateMeleeMechanics)
        {
            OnEnteredHurtbox(hurtbox);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (!hitboxActive)
        {
            return;
        }
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox && hitbox.associateMeleeMechanics != this.associateMeleeMechanics)
        {
            OnExitHitbox(hitbox);
        }
        if (hurtbox && hurtbox.associatedMeleeMechanics != this.associateMeleeMechanics)
        {
            OnExitHurtbox(hurtbox);
        }
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
