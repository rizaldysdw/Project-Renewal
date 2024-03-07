using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color normalColor, selectedColor;
    
    void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = normalColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Item Item = eventData.pointerDrag.GetComponent<Item>();
            Item.parentAfterDrag = transform;
        }
    }
}
