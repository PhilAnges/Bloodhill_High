using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public GameObject[] items;

    public PlayerData(ItemPickup player)
    {
        items = player.inventory;
    }
}
