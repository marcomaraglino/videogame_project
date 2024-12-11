using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlueprint
{
    public string itemName;

    public string Req1;
    public string Req2;

    public int Req1Amount;
    public int Req2Amount;

    public int numOfRequirements;

    public ItemBlueprint(string itemName, int numOfRequirements, string r1, int r1num, string r2, int r2num)
    {
        this.itemName = itemName;
        this.numOfRequirements = numOfRequirements;

        Req1 = r1;
        Req1Amount = r1num;
        
        Req2 = r2;
        Req2Amount = r2num;
    }
}
