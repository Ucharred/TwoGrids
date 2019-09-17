using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    //public Building building;
    public int buildingId;
    public Tile currentTile;

    public void BuildThis()
    {
        if (currentTile.tileBuilding != null)
        {
            Debug.Log("Tile already has a building...");
        } else if (currentTile.tileBuilding == null)
        {
            currentTile.BuildOnTile(CreateNewBuilding());
        }
    }

    Building CreateNewBuilding()
    {
        //This creates the gameobject holding the building itself, which will later be attatched to a Tile.
        GameObject go = new GameObject("Building");
        go.transform.position = currentTile.transform.position + new Vector3(0,0,-1);

        //picking which TYPE of building will be added to the newly instantiated GameObject
        switch (buildingId)
        {
            case 1:
                go.AddComponent<Bank>();
                go.AddComponent<SpriteRenderer>();
                go.GetComponent<SpriteRenderer>().sprite = BuildManager.Instance.Sprites[0];
                return go.GetComponent<Building>();
        }

        return null;
    }

}
