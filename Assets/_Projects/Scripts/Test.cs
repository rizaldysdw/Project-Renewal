using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
   public Tilemap tileMap;
    [SerializeField] 
    Vector3 mousePosPixels;

    [SerializeField]
    Vector3Int tileMapPosition;

    public Tile TomatoTreeGrow;

    public bool hoveringItem;

    void Start()
    {
      
    }

    
    void Update()
    {
        mousePosPixels = Input.mousePosition;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosPixels);

        tileMapPosition = tileMap.WorldToCell(worldPosition);

           



        //mouse is hovering over an item
        if(tileMap.HasTile(tileMapPosition))
        {
            var tile  = tileMap.GetTile(tileMapPosition);


            
            hoveringItem = true;
        }


        //mouse is not hovering over an item
        else
        {


            hoveringItem = false;
        }






        //clicked on an item
        if(Input.GetMouseButtonDown(0) && tileMap.HasTile(tileMapPosition))
        {

            var tile = tileMap.GetTile(tileMapPosition);



            if(tile.name.Contains("TomatoTreeGrow") && haveWater())
            {
                //remove one count from wateringcan

                Debug.Log("watering tomatotreegrow");

            }

            else if (tile.name.Contains("TomatoTreeGrow") && !haveWater())
            {

                //we dont have any water to water this tree
                Debug.Log("need water");
            }

        }




    }

    public bool haveWater()
    {
        //if we dont have water in the watering can
        return false;


    }

}
