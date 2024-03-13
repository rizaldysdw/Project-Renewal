using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{

    [Header("Generic")]
    public ItemType itemType;
    public ActionType actionType;
    public Sprite sprite;
    public Vector2Int range = new Vector2Int(5, 4);
    public bool stackable = true;

    [Header("Tool")]
    public ToolType toolType;
    public Sprite toolSprite;


    [Header("Crop")]
    public CropType cropType;
    public CropGrowthStage cropStage;
    public Sprite seedlingCropSprite;
    public Sprite matureCropSprite;
    public Sprite harvestableCropSprite;
}

public enum ItemType
{
    Tool,
    Crop
}

public enum ToolType
{
    None,
    Hoe,
    Pickaxe,
    WateringCan
}

public enum CropType
{
    None,
    Wheat,
    Corn,
    Tomato
}

public enum CropGrowthStage
{
    Seedling,
    Mature,
    Harvestable
}

public enum ActionType
{
    Use,
    Plant,
    Drop
}
