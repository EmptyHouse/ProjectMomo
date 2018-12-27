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

    private List<InventoryItem> orderedInventoryItemList = new List<InventoryItem>();

    /// <summary>
    /// The index of the currently seleted item in our orderedInventoryItemList
    /// </summary>
    private int currentlySelectedItemIndex = -1;

    #region monobehaviour methods
    private void Start()
    {
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
            if (!orderedInventoryItemList.Contains(inventoryItemToAdd))
            {
                orderedInventoryItemList.Add(inventoryItemToAdd);
                
            }
            if (currentlySelectedItemIndex < 0)
            {
                SelectNextItem();
            }
        }
        allCollectedItems[inventoryItemToAdd] += numberOfItemsToAdd;

        allCollectedItems[inventoryItemToAdd] = Mathf.Min(allCollectedItems[inventoryItemToAdd], inventoryItemToAdd.maxNumberOfItemsToHold);



        VisuallyUpdateInventoryItem();
    }

    private void VisuallyUpdateInventoryItem()
    {
        if (orderedInventoryItemList.Count == 0)
        {
            return;
        }
        InventoryItem currentItem = orderedInventoryItemList[currentlySelectedItemIndex];
        InventoryUIManager.Instance.SetCurrentlySelectedItem(currentItem, GetItemsRemaining(currentItem));
    }

    /// <summary>
    /// Returns the currently selected item if there is one available
    /// </summary>
    /// <returns></returns>
    public InventoryItem GetCurrentlySelectedItem()
    {
        if (orderedInventoryItemList.Count == 0 )
        {
            return null;
        }
        else
        {
            return orderedInventoryItemList[currentlySelectedItemIndex];
        }
    }

    /// <summary>
    /// This will return the total number of items we have in our inventory. If we have never collected an item
    /// we will return a size of 0
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetItemsRemaining(InventoryItem item)
    {
        if (allCollectedItems.ContainsKey(item))
        {
            return allCollectedItems[item];
        }
        return 0;
    }

    /// <summary>
    /// Returns a bool value indicating if we have any instances of an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool ContainsItem(InventoryItem item)
    {
        return GetItemsRemaining(item) > 0;
    }

    /// <summary>
    /// This method simply discards items from your inventory if there are any remaining. It will not use the item
    /// </summary>
    /// <param name="inventoryItemToRemove"></param>
    /// <param name="numberOfItemsToRemove"></param>
    public void RemoveItemFromInventory(InventoryItem inventoryItemToRemove, int numberOfItemsToRemove)
    {
        if (ContainsItem(inventoryItemToRemove))
        {
            allCollectedItems[inventoryItemToRemove] -= numberOfItemsToRemove;
            allCollectedItems[inventoryItemToRemove] = Mathf.Max(0, allCollectedItems[inventoryItemToRemove]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directionOfNextItem"></param>
    public void SelectNextItem(int directionOfNextItem = 1)
    {
        if (orderedInventoryItemList.Count == 0)
        {
            return;
        }

        currentlySelectedItemIndex += directionOfNextItem;
        currentlySelectedItemIndex %= orderedInventoryItemList.Count;
        currentlySelectedItemIndex += orderedInventoryItemList.Count;
        currentlySelectedItemIndex %= orderedInventoryItemList.Count;

        VisuallyUpdateInventoryItem();
    }
    
    /// <summary>
    /// Uses an instance of the currenctly selected item
    /// </summary>
    public void UseItem()
    {
        if (orderedInventoryItemList.Count == 0) return;


        InventoryItem itemToUse = orderedInventoryItemList[currentlySelectedItemIndex];
        itemToUse.UseItem(associateCharacterstats);

        RemoveItemFromInventory(itemToUse, 1);
        VisuallyUpdateInventoryItem();
    }
}
