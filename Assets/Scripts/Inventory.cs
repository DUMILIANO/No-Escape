using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        public static Inventory instance;

        void Awake() 
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one instance of inventory found");
                return;
            }
            instance = this;
        }
        #endregion

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangeCallback;

        public int space = 20;

        public List<Item> items = new List<Item>();

        public bool Add(Item item)
        {
            if(items.Count >= space)
            {
                Debug.Log("Inventory full");
                return false;
            }
            items.Add(item);

            if(onItemChangeCallback != null)
            {
                onItemChangeCallback.Invoke();
            }

            return true;
        }
        public void Remove (Item item)
        {
            items.Remove(item);
            if(onItemChangeCallback != null)
            {
                onItemChangeCallback.Invoke();
            }
        }
    }
}

