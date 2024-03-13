using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Text itemAmountText;

    [HideInInspector] public ItemSO itemData;
    [HideInInspector] public int itemAmount = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(ItemSO newItemData)
    {
        itemData = newItemData;

        UpdateItemSprite(itemData);
        RefreshItemAmount();
    }

    private void UpdateItemSprite(ItemSO itemData)
    {
        switch(itemData.itemType)
        {
            case ItemType.Tool:
                image.sprite = itemData.toolSprite;
                break;

            case ItemType.Crop:
                switch (itemData.cropStage)
                {
                    case CropGrowthStage.Seedling:
                        image.sprite = itemData.seedlingCropSprite;
                        break;

                    case CropGrowthStage.Mature:
                        image.sprite = itemData.matureCropSprite;
                        break;

                    case CropGrowthStage.Harvestable:
                        image.sprite = itemData.harvestableCropSprite;
                        break;
                }
                break;
        }
    }

    public void RefreshItemAmount()
    {
        itemAmountText.text = itemAmount.ToString();

        bool displayItemAmountText = itemAmount > 1;
        itemAmountText.gameObject.SetActive(displayItemAmountText);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
