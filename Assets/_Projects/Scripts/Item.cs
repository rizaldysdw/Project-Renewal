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
        image.sprite = newItemData.sprite;

        RefreshItemAmount();
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
