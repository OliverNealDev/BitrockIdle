using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    public Dictionary<string, int> Inventory = new Dictionary<string, int>()
    {
        { "stone", 0},
        { "coal", 0 },
        { "copper", 0 },
        { "iron", 0 },
        { "gold", 0 },
        { "diamond", 0 },
        { "painite", 0 }
    };

    public Dictionary<string, int> oreRarities = new Dictionary<string, int>()
    {
        { "stone", 16224},
        { "coal", 4056 },
        { "copper", 1028 },
        { "iron", 256 },
        { "gold", 64 },
        { "diamond", 16 },
        { "painite", 1 }
    };

    void Awake()
    {
        Instance = this;
    }

    public void PickAndAddOre()
    {
        int totalWeight = 0;
        foreach (KeyValuePair<string, int> ore in oreRarities)
        {
            totalWeight += ore.Value;
        }
        
        int randomWeight = Random.Range(1, totalWeight + 1);

        foreach (KeyValuePair<string, int> ore in oreRarities)
        {
            randomWeight -= ore.Value;
            if (randomWeight <= 0)
            {
                // Add the selected ore to the inventory
                Inventory[ore.Key]++;
                Debug.Log("Ore added: " + ore.Key);
                return;
            }
        }
    }
}