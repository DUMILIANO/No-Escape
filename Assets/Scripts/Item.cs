using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject {
    new public string name = "Item";
    public Sprite icon = null;
    public bool important = false;

    public virtual void Use()
    {
        Debug.Log("Using" + name);
    }
}
