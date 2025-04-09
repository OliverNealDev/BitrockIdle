using System.Collections.Generic;
using TMPro;
using Unity.Collections;
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

    [SerializeField] private GameObject stoneImage;
    [SerializeField] private GameObject coalImage;
    [SerializeField] private GameObject copperImage;
    [SerializeField] private GameObject ironImage;
    [SerializeField] private GameObject goldImage;
    [SerializeField] private GameObject diamondImage;
    [SerializeField] private GameObject painiteImage;
    
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI coalText;
    [SerializeField] private TextMeshProUGUI copperText;
    [SerializeField] private TextMeshProUGUI ironText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private TextMeshProUGUI painiteText;

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
                switch (ore.Key)
                {
                    case "stone":
                        stoneText.text = Inventory[ore.Key].ToString();
                        break;
                    case "coal":
                        coalText.text = Inventory[ore.Key].ToString();
                        break;
                    case "copper":
                        copperText.text = Inventory[ore.Key].ToString();
                        break;
                    case "iron":
                        ironText.text = Inventory[ore.Key].ToString();
                        break;
                    case "gold":
                        goldText.text = Inventory[ore.Key].ToString();
                        break;
                    case "diamond":
                        diamondText.text = Inventory[ore.Key].ToString();
                        break;
                    case "painite":
                        painiteText.text = Inventory[ore.Key].ToString();
                        break;
                }
                Debug.Log("Ore added: " + ore.Key);
                return;
            }
        }
    }
}