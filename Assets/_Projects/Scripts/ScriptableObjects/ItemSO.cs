using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{

    [Header("Gameplay")]
    public ItemType itemType;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite sprite;

}

public enum ItemType
{
    Tool,
    Crop,
    Terrain
}

public enum ActionType
{
    Use,
    Drop
}
