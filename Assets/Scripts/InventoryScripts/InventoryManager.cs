using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    /// <summary>
    /// The associated character stats
    /// </summary>
    public CharacterStats associateCharacterstats { get; set; }

    /// <summary>
    /// This is a dictionary collection of all our collected items. The key is our inventory item properties
    /// The value is the number of items we have. Values equal to 0 will be ignored and treated as not contained
    /// in our inventory
    /// </summary>
    private Dictionary<InventoryItem, int> allCollectedItems = new Dictionary<InventoryItem, int>();
    public InventoryItem[] itemsToAdd;

    #region monobehaviour methods
    private void Start()
    {
        foreach (InventoryItem item in itemsToAdd)
        {
            AddItemToInventory(item);
        }

    }
    #endregion monobehaviour methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="inventoryItemToAdd"></param>
    /// <param name="numberOfItemsToAdd"></param>
    public void AddItemToInventory(InventoryItem inventoryItemToAdd, int numberOfItemsToAdd = 1)
    {
        if (!allCollectedItems.ContainsKey(inventoryItemToAdd))
        {
            allCollectedItems.Add(inventoryItemToAdd, 0);
        }
        allCollectedItems[inventoryItemToAdd] += numberOfItemsToAdd;

        allCollectedItems[inventoryItemToAdd] = Mathf.Min(allCollectedItems[inventoryItemToAdd], inventoryItemToAdd.maxNumberOfItemsToHold);
    }

    /// <summary>
    /// This method simply discards items from your inventory if there are any remaining. It will not use the item
    /// </summary>
    /// <param name="inventoryItemToRemove"></param>
    /// <param name="numberOfItemsToRemove"></param>
    public void RemoveItemFromInventory(InventoryItem inventoryItemToRemove, int numberOfItemsToRemove)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToUse"></param>
    public void UseItem(InventoryItem itemToUse)
    {


        RemoveItemFromInventory(itemToUse, 1);
    }
}
