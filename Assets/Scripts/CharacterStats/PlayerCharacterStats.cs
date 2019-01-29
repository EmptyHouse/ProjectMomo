using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An extension of the the CharacterStats class, but this will contain references that are only relevant to the player
/// </summary>
public class PlayerCharacterStats : CharacterStats {

    #region main variables
    public InventoryManager inventoryManager { get; set; }
    public PlayerController playerController { get; set; }
    #endregion main variables

    #region monobehaviour methods
    protected override void Awake()
    {
        GameOverseer.Instance.playerCharacterStats = this;

        base.Awake();
        timeManagedObject = GetComponent<TimeManagedPlayer>();
        playerController = GetComponent<PlayerController>();
        inventoryManager = GetComponent<InventoryManager>();

        GameOverseer.Instance.AddObjectToDontDestroyOnLoad(this.gameObject);
    }
    #endregion monobehaviour methods

    #region override methods
    public override void TakeDamage(float damageToTake)
    {
        base.TakeDamage(damageToTake);
        InGameUI.Instance.SetPlayerHealthBar(currentHealth / maxHealth);
    }
    #endregion override methods
}
