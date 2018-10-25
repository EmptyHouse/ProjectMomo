using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {
    [System.NonSerialized]
    public HitboxManager associatedMeleeMechanics;
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
