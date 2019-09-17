using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material defaultMat;
    public Material highlight;
    public Player owner;
    public Building tileBuilding;

    // Start is called before the first frame update
    void Awake()
    {

        defaultMat = transform.GetComponent<Renderer>().material;

        if (tileBuilding != null)
        {
            tileBuilding.owner = owner;
            TurnManager.Instance.AtEndTurn += tileBuilding.EndOfTurnEffect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTile()
    {
        //Telegraph that this tile is selected
        //Bring up UI for tile enhancements/buildings
    }

    public void BuildOnTile(Building building)
    {   
        if (owner.coin >= building.cost)
        {
            owner.coin -= building.cost;
            tileBuilding = building;
            building.owner = owner;
            building.OnBuild();
            TurnManager.Instance.AtEndTurn += building.EndOfTurnEffect;
        }
        else
        {
            Debug.Log("NOT ENOUGH COIN");
            Destroy(building.gameObject);
        }

    }

    public void DestroyOnTile()
    {
        TurnManager.Instance.AtEndTurn -= tileBuilding.EndOfTurnEffect;
        tileBuilding = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().selectedTile = this;
            transform.GetComponent<Renderer>().material = highlight;
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().selectedTile = this;
            transform.GetComponent<Renderer>().material = highlight;
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponent<Renderer>().material = defaultMat; //telegraph it's currently selected
            other.GetComponent<Player>().CloseUI();
        }
    }

}
