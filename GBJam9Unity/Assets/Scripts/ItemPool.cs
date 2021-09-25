using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemPool", fileName = "ItemPool")]
public class ItemPool : ScriptableObject
{
    public List<ItemDetails> Items = new List<ItemDetails>();

}
