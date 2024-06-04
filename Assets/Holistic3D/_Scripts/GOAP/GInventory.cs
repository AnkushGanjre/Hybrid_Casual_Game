using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    public List<GameObject> items = new List<GameObject>();

    public void AddItems(GameObject go)
    {
        items.Add(go);
    }

    public GameObject FindItemWithTag(string tag)
    {
        foreach (GameObject item in items)
        {
            if (item.tag == tag)
            {
                return item;
            }
        }
        return null;
    }

    public void RemoveItems(GameObject go)
    {
        int indexToRemove = -1;
        foreach (GameObject item in items)
        {
            indexToRemove++;
            if (item == go)
                break;
        }
        if (indexToRemove >= -1)
            items.RemoveAt(indexToRemove);
    }
}
