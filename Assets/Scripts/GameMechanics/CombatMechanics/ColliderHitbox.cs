using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is an extension of the base hitbox class that handles hitboxes that contain 2d colliders.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ColliderHitbox : Hitbox {

    #region monobehaviour methods
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        

        Hitbox hitbox = collider.GetComponent<Hitbox>();
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
        
        Hitbox hitbox = collider.GetComponent<Hitbox>();
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
    #endregion monobehaviour methods
}
