using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderHitbox : Hitbox {

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
}
