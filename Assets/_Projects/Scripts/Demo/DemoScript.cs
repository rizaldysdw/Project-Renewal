using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemSO[] itemDataToAdd;

    public void AddItemToInventory(int id)
    {
       bool canAddItem = inventoryManager.AddItem(itemDataToAdd[id]);

        if (canAddItem)
        {
            Debug.Log("Item added to inventory!");
        }
        else
        {
            Debug.Log("Item cannot be added. Inventory is full!");
        }
    }

    public void GetSelectedItem()
    {
        ItemSO itemDataReceived = inventoryManager.GetSelectedItemData();

        if (itemDataReceived)
        {
            Debug.Log(itemDataReceived.name + " is selected!");
        }
        else
        {
            Debug.Log("No item selected!");
        }
    }
}
