using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will contain all the logic necessary for our character's hitbox
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour {
    public MeleeMechanics associatedMeleeMechanics { get; set; }
    #region monobehaviour methods
    private void Awake()
    {
        associatedMeleeMechanics.AddAssociateHurtbox(this);
    }

    private void OnDestroy()
    {
        associatedMeleeMechanics.RemoveAssociatedHurtbox(this);
    }
    #endregion monobehaviour methods
}
