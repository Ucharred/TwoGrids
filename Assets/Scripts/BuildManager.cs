using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    #region Singleton

    private static BuildManager _instance;
    public static BuildManager Instance

    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("BuildManager");
                go.AddComponent<BuildManager>();
            }

            return _instance;
        }
    }

    #endregion

    public GameObject buildUI;
    public GameObject[] buttons;

    public Sprite[] Sprites;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        buildUI = GameObject.FindGameObjectWithTag("BuildUI");
        buttons = GameObject.FindGameObjectsWithTag("BuildButtons");
        buildUI.SetActive(false);
        TurnManager.Instance.AtEndTurn += CloseUI;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildUI(Tile currentTile)
    {
        buildUI.SetActive(true);
        foreach (GameObject button in buttons)
        {
            button.GetComponent<BuildButton>().currentTile = currentTile;
        }
    }

    public void CloseUI()
    {
        buildUI.SetActive(false);
    }

    void BuildButton()
    {

    }



}
