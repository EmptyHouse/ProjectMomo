﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
/// <summary>
/// Script that handles any type of hitbox functionality. This will check for interactions with hitboxes or hurtboxes
/// </summary>
public class Hitbox : MonoBehaviour
{
    public CharacterStats associatedCharacterStats { get; set; }
    public Collider2D associatedCollider { get; private set; }
    public UnityEvent<Hurtbox> onHurtboxEnteredEvent = new HitboxOnHurtboxEvent();
    public UnityEvent<Hitbox> onHitboxEnteredEvent = new HitboxOnHitboxEvent();
    public UnityEvent<Collider2D> onCollisionEvent = new HitboxOnCollider2DEvent();

    #region monobehaviour methods
    private void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Hitbox");
        associatedCollider = GetComponent<Collider2D>();
        associatedCollider.isTrigger = true;
    }

    /// <summary>
    /// Sets off the appropritate triggers when our hitboxes enter a hitbox, hurtbox or generic collider2D
    /// </summary>
    /// <param name="collider"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox != null)
        {
            //if (hitbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hitbox.name + " contains a null associated CharacterStats reference");
            //    return;
            //}
            if (hitbox.associatedCharacterStats.hitboxLayer != this.associatedCharacterStats.hitboxLayer) { return; }
            onHitboxEnteredEvent.Invoke(hitbox);
            return;
        }
        if (hurtbox != null)
        {
            //if (hurtbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hurtbox.name + " contains a null associated CharacterStats reference");
            //    return;
            //}
            if (hurtbox.associatedCharacterStats.hitboxLayer != this.associatedCharacterStats.hitboxLayer) { return; }
            onHurtboxEnteredEvent.Invoke(hurtbox);
            return;
        }
        onCollisionEvent.Invoke(collider);

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        
    }
    #endregion monobehaviour methods

    private void SetupHitbox(CharacterStats associatedCharacterStats)
    {
        this.associatedCharacterStats = associatedCharacterStats;
    }

    #region hitbox events
    public class HitboxOnHitboxEvent : UnityEvent<Hitbox>
    {

    }

    public class HitboxOnHurtboxEvent : UnityEvent<Hurtbox>
    {

    }

    public class HitboxOnCollider2DEvent: UnityEvent<Collider2D>
    {

    }
    #endregion hitbox events
}
