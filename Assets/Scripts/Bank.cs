using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Building
{

    

    // Start is called before the first frame update
    void Awake()
    {
        cost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EndOfTurnEffect()
    {
        Debug.Log(name + " generated gold.");
        owner.coin += 1;
    }

}
