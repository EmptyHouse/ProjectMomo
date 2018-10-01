using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class Hitbox : MonoBehaviour {
    /// <summary>
    /// 
    /// </summary>
    private MeleeMechanics associateMeleeMechanics;

    #region monobehaiovur methods
    private void Awake()
    {
        associateMeleeMechanics.AddAssociatedHitbox(this);
    }

    private void OnDestroy()
    {
        associateMeleeMechanics.RemoveAssociatedHitbox(this);
    }
    #endregion monobehaviour methods
}
