using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int whichPlayer;

    public bool isBuilding;

    //control varsBuildManager
    public GameObject tint;

    public Tile selectedTile;

    public GameObject previouslySelectedButton;
    public GameObject selectedButton;

    public Rigidbody2D rb;
    public float speed;


    //Player vars
    public int coin;

    //Turn management stuff
    public bool isMyTurn;
    public float turnLength;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMyTurn)
        {
            ButtonHighlightManager();
            BuildControls();
            MovePlayer();
            if (isBuilding == true)
            {
                ScrollButtons();
                ButtonSelect();
            }
        }
    }

    void MovePlayer()
    {
        if (whichPlayer == 1)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
            Vector2 moveVelocity = moveInput.normalized * speed;

            rb.velocity += moveVelocity;

        } else if (whichPlayer == 2)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
            Vector2 moveVelocity = -moveInput.normalized * speed;

            rb.velocity += moveVelocity;
        }
    }

    void BuildControls()
    {
        if (whichPlayer == 1)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button15))
            {
                //bring up UI find out if UPGRADING or just BUILDING, etc
                BuildUI();
                isBuilding = true;
            }
            else if (Input.GetKeyUp(KeyCode.Joystick1Button15))
            {
                CloseUI();
            }
        }
        else if (whichPlayer == 2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button15))
            {
                //bring up UI find out if UPGRADING or just BUILDING, etc
                BuildUI();
            }
            else if (Input.GetKeyUp(KeyCode.Joystick2Button15))
            {
                CloseUI();
            }
        }
    }

    public int index;

    void ScrollButtons()
    {
        if (whichPlayer == 1)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                for (int i = 0; i < BuildManager.Instance.buttons.Length; i++)
                {
                    if (selectedButton == BuildManager.Instance.buttons[i])
                    {
                        index = i;
                    }
                }
                if ((index - 1) >= 0)
                {
                    selectedButton = BuildManager.Instance.buttons[index - 1];
                }
                else
                {
                    selectedButton = BuildManager.Instance.buttons[BuildManager.Instance.buttons.Length - 1];
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                for (int j = 0; j < BuildManager.Instance.buttons.Length; j++)
                {
                    if (selectedButton == BuildManager.Instance.buttons[j])
                    {
                        index = j;
                    }
                }
                if (index + 1 <= (BuildManager.Instance.buttons.Length - 1))
                {
                    selectedButton = BuildManager.Instance.buttons[index + 1];
                }
                else
                {
                    selectedButton = BuildManager.Instance.buttons[0];
                }
            }
        }

        if (whichPlayer == 2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                for (int i = 0; i < BuildManager.Instance.buttons.Length; i++)
                {
                    if (selectedButton == BuildManager.Instance.buttons[i])
                    {
                        index = i;
                    }
                }
                if ((index - 1) >= 0)
                {
                    selectedButton = BuildManager.Instance.buttons[index - 1];
                }
                else
                {
                    selectedButton = BuildManager.Instance.buttons[BuildManager.Instance.buttons.Length - 1];
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
            {
                for (int j = 0; j < BuildManager.Instance.buttons.Length; j++)
                {
                    if (selectedButton == BuildManager.Instance.buttons[j])
                    {
                        index = j;
                    }
                }
                if (index + 1 <= (BuildManager.Instance.buttons.Length - 1))
                {
                    selectedButton = BuildManager.Instance.buttons[index + 1];
                }
                else
                {
                    selectedButton = BuildManager.Instance.buttons[0];
                }
            }
        }
    }

    void ButtonHighlightManager()
    {
        if (selectedButton != null)
        {
            selectedButton.GetComponent<Button>().Select();
            previouslySelectedButton = selectedButton;

            if (previouslySelectedButton != selectedButton)
            {
                previouslySelectedButton.GetComponent<Button>().OnSelect(null);
            }
        }
    }

    void ButtonSelect()
    {
        if (whichPlayer == 1)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                selectedButton.GetComponent<BuildButton>().BuildThis();
                CloseUI();
            }
        } else if (whichPlayer == 2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                selectedButton.GetComponent<BuildButton>().BuildThis();
                CloseUI();
            }
        }


    }





    void BuildUI()
    {
        //brings up UI screen
        isBuilding = true;
        BuildManager.Instance.BuildUI(selectedTile);
        selectedButton = BuildManager.Instance.buttons[0];
    }

    public void CloseUI()
    {
        isBuilding = false;
        BuildManager.Instance.CloseUI();
    }

    

}
