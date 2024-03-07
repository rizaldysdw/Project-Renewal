using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public int maxStackPerSlot = 16;
    public GameObject itemPrefab;

    private int selectedSlot = -1;

    void Start()
    {
        ChangeSelectedSlot(0);    
    }

    void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int numberPressed);
            if (isNumber && numberPressed > 0 && numberPressed <= 9)
            {
                ChangeSelectedSlot(numberPressed - 1);
            }
        }
    }

    private void ChangeSelectedSlot(int newSelectedSlot)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newSelectedSlot].Select();
        selectedSlot = newSelectedSlot;
    }

    public bool AddItem(ItemSO itemData)
    {
        // Search for the same item with lower amount than maximum stack capacity
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();

            if (itemInSlot
                && itemInSlot.itemData == itemData
                && itemInSlot.itemAmount < maxStackPerSlot
                && itemInSlot.itemData.stackable)
            {
                itemInSlot.itemAmount++;
                itemInSlot.RefreshItemAmount();
                return true;
            }
        }

        // Search for empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();

            if (itemInSlot == null)
            {
                SpawnNewItem(itemData, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(ItemSO itemData, InventorySlot inventorySlot)
    {
        GameObject newItemGO = Instantiate(itemPrefab, inventorySlot.transform);
        Item item = newItemGO.GetComponent<Item>();
        item.InitializeItem(itemData);
    }
    
    public ItemSO GetSelectedItemData()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        Item itemInSlot = slot.GetComponentInChildren<Item>();

        if (itemInSlot)
        {
            return itemInSlot.itemData;
        }

        return null;
    }
}
