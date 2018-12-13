using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is an extension of the base hitbox class that handles hitboxes that contain 2d colliders.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ColliderHitbox : Hitbox {
    [System.NonSerialized]
    /// <summary>
    /// 
    /// </summary>
    public MeleeMechanics associateMeleeMechanics;
    #region monobehaviour methods
    protected override void Awake()
    {
        base.Awake();
        associateMeleeMechanics = GetComponentInParent<MeleeMechanics>();
        associateMeleeMechanics.AddAssociatedHitbox(this);
        onHitboxCollisionEnteredEvent += associateMeleeMechanics.OnHitboxEnteredEnemyHurtbox;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        

        ColliderHitbox hitbox = collider.GetComponent<ColliderHitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox && hitbox.associateMeleeMechanics != this.associateMeleeMechanics)
        {
            OnEnteredHitbox(hitbox);
        }
        if (hurtbox && hurtbox.associatedCharacterstats != this.associateMeleeMechanics.associatedCharacterStats)
        {
            OnEnteredHurtbox(hurtbox, collider.transform.position);//Probably wanna fix this up in the future
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {

        ColliderHitbox hitbox = collider.GetComponent<ColliderHitbox>();

        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox && hitbox.associateMeleeMechanics != this.associateMeleeMechanics)
        {
            OnExitHitbox(hitbox);
        }
        if (hurtbox && hurtbox.associatedCharacterstats != this.associateMeleeMechanics.associatedCharacterStats)
        {
            OnExitHurtbox(hurtbox);
        }
    }

    protected override void OnDestroy()
    {
        if (associateMeleeMechanics != null)
        {
            associateMeleeMechanics.RemoveAssociatedHitbox(this);
        }
    }
    #endregion monobehaviour methods
}
