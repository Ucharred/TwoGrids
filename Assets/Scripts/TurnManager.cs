using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    #region Singleton

    private static TurnManager _instance;
    public static TurnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("TurnManager");
                go.AddComponent<TurnManager>();
            }

            return _instance;
        }
    }

    #endregion

    public delegate void Turn();
    public event Turn AtEndTurn;
    public event Turn AtStartTurn;

    //Player vars

    public Player[] players = new Player[2];
    public Player curPlayer;
    //public Player nextPlayer;

    [SerializeField]
    private float turnLength;

    public int testNum;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnTimer());

        FindPlayers();
        Debug.Log("number of players: " + players.Length);
        AtStartTurn += StartOfTurn;
        AtEndTurn += EndOfTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public IEnumerator TurnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);

            AtStartTurn();

            yield return new WaitForSeconds(turnLength);

            AtEndTurn();
        }
    }

    public void StartOfTurn()
    {
        Debug.Log("Turn is starting...");

        if (curPlayer != null)
        {
            curPlayer = FindNextPlayer();
        }
        else
        {
            curPlayer = players[0];
        }

        curPlayer.isMyTurn = true;
        curPlayer.tint.SetActive(false);
        foreach (Player player in players)
        {
            if (player != curPlayer)
            {
                player.tint.SetActive(true);
            }
        }

        turnLength = curPlayer.turnLength;
    }

    public void EndOfTurn()
    {
        Debug.Log("Turn is ending...");

        //curPlayer.tint.SetActive(true);
        curPlayer.isMyTurn = false;
    }

    public Player FindNextPlayer()
    {
        int i;

        for (i = 0; i < players.Length; i++)
        {
            //Finds the index of the current player, then if there is a player above it return that player. Else returns the first player in array.
            if (players[i] == curPlayer)
            {
                if ((i + 1) < players.Length)
                {
                    return players[i + 1];
                }
                else
                {
                    return players[0];
                }
            }
        }

        return null;

    }

    //Preparation stuff

    private void FindPlayers()
    {
        int index = 0;
        var people = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject person in people)
        {
            players[index] = person.GetComponent<Player>();
            index++;
        }
    }

}
