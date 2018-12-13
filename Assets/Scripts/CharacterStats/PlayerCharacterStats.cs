using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An extension of the the CharacterStats class, but this will contain references that are only relevant to the player
/// </summary>
public class PlayerCharacterStats : CharacterStats {



    #region monobehaviour methods
    protected override void Start()
    {
        base.Start();
        GameOverseer.Instance.playerCharacterStats = this;
    }
    #endregion monobehaviour methods
}
