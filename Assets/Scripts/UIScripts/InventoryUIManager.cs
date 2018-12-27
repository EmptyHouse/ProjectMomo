using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour {
    #region static variables
    private static InventoryUIManager instance;

    public static InventoryUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InventoryUIManager>();
            }
            return instance;
        }
    }
    #endregion static variables

    #region main variables
    [Header("UI References")]
    [Tooltip("")]
    public Image currentlySelectedItemImage;
    public Text itemCountText;

    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
    }
    #endregion monobehaviour methods

    public void SetCurrentlySelectedItem(InventoryItem currentlySelectedItem, int countRemaining)
    {
        currentlySelectedItemImage.sprite = currentlySelectedItem.itemIcon;
        itemCountText.text = countRemaining.ToString() + "x";
    }
}
