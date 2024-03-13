using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmTile : MonoBehaviour
{
    [SerializeField] private ItemSO currentCrop;
    public int waterFilled = 0;
    private const int maxWaterFilled = 3;

    public Tile tile;

    private SpriteRenderer tileSprite;
    public bool isPlowed;

    void Start()
    {
        InitSetup();
        UpdateCropState();    
    }

    private void InitSetup()
    {
        tileSprite = GetComponent<SpriteRenderer>();
    }

    public void PlantCrop(ItemSO newCrop)
    {
        currentCrop = newCrop;
        tileSprite.sprite = newCrop.seedlingCropSprite;
        UpdateCropState();
    }

    public void FillWater()
    {
        if (waterFilled < maxWaterFilled)
        {
            waterFilled++;
            UpdateCropState();
        }
        else
        {
            Debug.Log("This crop is ready to be harvested!");
        }
    }

    private void UpdateCropState()
    {
        switch (waterFilled)
        {
            case 0:
                currentCrop.cropStage = CropGrowthStage.Seedling;
                tileSprite.sprite = currentCrop.seedlingCropSprite;
                break;

            case 1:
                currentCrop.cropStage = CropGrowthStage.Seedling;
                tileSprite.sprite = currentCrop.seedlingCropSprite;
                break;

            case 2:
                currentCrop.cropStage = CropGrowthStage.Mature;
                tileSprite.sprite = currentCrop.matureCropSprite;
                break;

            case 3:
                currentCrop.cropStage = CropGrowthStage.Harvestable;
                tileSprite.sprite = currentCrop.harvestableCropSprite;
                break;
        }
    }

    public ItemSO HarvestCrop()
    {
        return currentCrop;
    }
}
