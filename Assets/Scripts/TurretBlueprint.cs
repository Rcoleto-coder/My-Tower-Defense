using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradedPrefab;

    public int cost;
    public int upgradeCost;
    
    public int GetSellAmount()
    {
        return cost / 2;
    }
}
