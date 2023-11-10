using UnityEngine;

public class UIInventory : MonoBehaviour
{
    //public InventoryWithSlot inventory { get;  set; }
    //private int capacity = 10;

    //private void Awake()
    //{
    //    inventory = new InventoryWithSlot(capacity);
    //}

    [SerializeField] private InventoryItemInfo _appleInfo;
    [SerializeField] private InventoryItemInfo _pepperInfo;

    public InventoryWithSlot inventory => tester._inventory;
    private UIInventoryTester tester ;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        tester = new UIInventoryTester(_appleInfo, _pepperInfo, uiSlots);
        Debug.Log($" 1. Tester is done");
        //tester.FillSlots();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log($"  Something ++++ ");
            tester.AddRandomApples();
        }
        //if (Input.GetKeyDown(KeyCode.R))
        //    RemoveRandomApples();
    }

}
