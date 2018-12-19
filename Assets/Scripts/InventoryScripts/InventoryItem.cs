using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for all Inventory Items. These are passive or active items that the player can use to give them
/// benefits
/// </summary>
public class InventoryItem : MonoBehaviour {
    public enum ItemType
    {
        PassiveItem,
        ActiveItem,
    }

    #region main variables
    [Tooltip("The name of the item that will be displayed to our players. This will also be used when checking for duplicate items in our list")]
    public string inventoryItemName;
    [Tooltip("The maximum number of items that we can hold of this particular item. Used for stacking certain items")]
    public int maxNumberOfItemsToHold = 1;
    public ItemType itemType;
    #endregion main variables

    #region monobehaviour methods
    
    #endregion monobehaviour methods
    /// <summary>
    /// Method that will be called anytime we need to use this item
    /// </summary>
    /// <param name="playerStats"></param>
    public void UseItem(CharacterStats playerStats)
    {

    }

    
    public override bool Equals(object other)
    {
        if (!(other is InventoryItem)) return false;
        return ((InventoryItem)other).inventoryItemName == this.inventoryItemName;
    }

    public override int GetHashCode()
    {
        return this.inventoryItemName.GetHashCode();
    }
}
