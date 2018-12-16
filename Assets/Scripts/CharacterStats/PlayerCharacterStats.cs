using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An extension of the the CharacterStats class, but this will contain references that are only relevant to the player
/// </summary>
public class PlayerCharacterStats : CharacterStats {

    #region main variables
    public PlayerController playerController;
    #endregion main variables

    #region monobehaviour methods
    protected override void Start()
    {
        base.Start();
        playerController = GetComponent<PlayerController>();
        GameOverseer.Instance.playerCharacterStats = this;
    }
    #endregion monobehaviour methods
}
