using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for our hitboxes
/// </summary>
public abstract class Hitbox : MonoBehaviour {
    public const string DEFAULT_LAYER = "Hitbox";

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
        associateMeleeMechanics = GetComponentInParent<MeleeMechanics>();
        associateMeleeMechanics.AddAssociatedHitbox(this);
        onHitboxCollisionEnteredEvent += associateMeleeMechanics.OnHitboxOnEnemyHurtbox;

    }

    protected virtual void OnDestroy()
    {
        if (associateMeleeMechanics != null)
        {
            associateMeleeMechanics.RemoveAssociatedHitbox(this);
        }
        
    }


    private void OnValidate()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer(DEFAULT_LAYER))
        {
            this.gameObject.layer = LayerMask.NameToLayer(DEFAULT_LAYER);
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
    public virtual void OnEnteredHurtbox(Hurtbox hurtboxThatWasEntered, Vector3 positionOfHit)
    {
        onHitboxCollisionEnteredEvent(hurtboxThatWasEntered.associatedCharacterstats, positionOfHit);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtboxThatWasExited"></param>
    public virtual void OnExitHurtbox(Hurtbox hurtboxThatWasExited)
    {

    }

    public void HurttboxEnteredEvent(CharacterStats stats, Vector3 pointOfImpact)
    {
        onHitboxCollisionEnteredEvent(stats, pointOfImpact);
    }
}
