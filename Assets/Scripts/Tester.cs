using UnityEngine;


public class Tester : MonoBehaviour
{
        private IInventory _inventory;

        public void Awake()
        {
            var inventoryCapacity = 10;
            _inventory = new InventoryWithSlot(inventoryCapacity);
            Debug.Log($"Inventory initialized, capacity: {inventoryCapacity}");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                AddRandomApples();
            if (Input.GetKeyDown(KeyCode.R))
                RemoveRandomApples();
        }

        void AddRandomApples()
        {
            var rnd = Random.Range(1, 5);
            Apple apple = new Apple(_inventory.GetItem(typeof(Apple)).info);
            apple.state.amount = rnd;
            _inventory.TryToAdd(this, apple);
            Debug.Log($"     GetItemAmount  =  {_inventory.GetItemAmount(typeof(Apple))}");
        }
        void RemoveRandomApples()
        {
            int rnd = Random.Range(1, 10);
            _inventory.Remove(this, typeof(Apple), rnd);
            Debug.Log($"     GetItemAmount  =  {_inventory.GetItemAmount(typeof(Apple))}");
        }
}
