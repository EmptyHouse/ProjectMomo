using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Melee mechanics. Uses 
/// </summary>
public class MeleeMechanics : MonoBehaviour {
    [HideInInspector]
    public List<Hitbox> allAssociatedHitboxes = new List<Hitbox>();
    [HideInInspector]
    public List<Hurtbox> allAssociatedHurtboxes = new List<Hurtbox>();

    #region monobehaviour methods
    private void Awake()
    {
        
    }
    #endregion monobehaviour methods


    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitbox"></param>
    public void AddAssociatedHitbox(Hitbox hitbox)
    {
        if (allAssociatedHitboxes.Contains(hitbox))
        {
            Debug.LogWarning("This hitbox was already added.", hitbox);
            return;
        }
        allAssociatedHitboxes.Add(hitbox);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitbox"></param>
    public void RemoveAssociatedHitbox(Hitbox hitbox)
    {
        if (!allAssociatedHitboxes.Contains(hitbox))
        {
            Debug.LogWarning("This hitbox was not found in the list. Perhaps it was already removed", hitbox);
            return;
        }
        allAssociatedHitboxes.Remove(hitbox);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtbox"></param>
    public void AddAssociateHurtbox(Hurtbox hurtbox)
    {
        if (allAssociatedHurtboxes.Contains(hurtbox))
        {
            Debug.LogWarning("This hurtbox was already added.", hurtbox);
            return;
        }
        allAssociatedHurtboxes.Add(hurtbox);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveAssociatedHurtbox(Hurtbox hurtbox)
    {
        if (!allAssociatedHurtboxes.Contains(hurtbox))
        {
            Debug.LogWarning("This hurtbox was not found in the list. Perhaps it was already removed?", hurtbox);
            return;
        }
        allAssociatedHurtboxes.Remove(hurtbox);
    }
}
