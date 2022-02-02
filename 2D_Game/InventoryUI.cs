using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    //
    public GameObject tabs;
    bool otherTabOpen = false;
    //inventory
    public GameObject inventoryUI;
    public Button inventoryTab;
    public bool inventoryTabOpen;

    //equipment
    public GameObject equipmentUI;
    public Button equipmentTab;
    public bool armorTabOpen;

    //stats
    public GameObject statsUI;
    public Button statsTab;
    public bool statsTabOpen;

    //map
    public GameObject mapUI;
    public Button mapTab;
    public bool mapTabOpen;

    //settings
    public GameObject settingsUI;
    public Button settingsTab;
    public bool settingsTabOpen;

    //

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        //if any tab is open this will cause UI to close if tab is pressed
        if (armorTabOpen || statsTabOpen || mapTabOpen || settingsTabOpen)
        {
            otherTabOpen = true;
        }
        else if (inventoryTabOpen)
            otherTabOpen = false;
        //
        
        
        if (!otherTabOpen)
        {
            //enters inventory if tab is pressed
            if (Input.GetButtonDown("Inventory"))
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                tabs.SetActive(!tabs.activeSelf);
            }
        }
        //
        //closes other UI
        if (Input.GetButtonDown("Inventory"))
        {
            if (equipmentUI.activeSelf || statsUI.activeSelf || mapUI.activeSelf || settingsUI.activeSelf)
            {
                inventoryUI.SetActive(false);
                equipmentUI.SetActive(false);
                statsUI.SetActive(false);
                mapUI.SetActive(false);
                settingsUI.SetActive(false);
                inventoryTabOpen = false;
                armorTabOpen = false;
                statsTabOpen = false;
                mapTabOpen = false;
                settingsTabOpen = false;
                otherTabOpen = false;
                tabs.SetActive(!tabs.activeSelf);
            }
        }
        //
        

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    
    //logic for tab buttons
    public void InventoryTab()
    {
        if (inventoryTab)
        {
            inventoryTabOpen = true;
            armorTabOpen = false;
            statsTabOpen = false;
            mapTabOpen = false;
            settingsTabOpen = false;
            inventoryUI.SetActive(true);
            equipmentUI.SetActive(false);
            statsUI.SetActive(false);
            mapUI.SetActive(false);
            settingsUI.SetActive(false);
        }
    }
    public void EquipmentTab()
    {
        if (equipmentTab)
        {
            inventoryTabOpen = false;
            armorTabOpen = true;
            statsTabOpen = false;
            mapTabOpen = false;
            settingsTabOpen = false;
            inventoryUI.SetActive(false);
            equipmentUI.SetActive(true);
            statsUI.SetActive(false);
            mapUI.SetActive(false);
            settingsUI.SetActive(false);
        }
    }
    public void StatsTab()
    {
        if (statsTab)
        {
            inventoryTabOpen = false;
            armorTabOpen = false;
            statsTabOpen = true;
            mapTabOpen = false;
            settingsTabOpen = false;
            inventoryUI.SetActive(false);
            equipmentUI.SetActive(false);
            statsUI.SetActive(true);
            mapUI.SetActive(false);
            settingsUI.SetActive(false);
        }
    }
    public void MapTab()
    {
        if (mapTab)
        {
            inventoryTabOpen = false;
            armorTabOpen = false;
            statsTabOpen = false;
            mapTabOpen = true;
            settingsTabOpen = false;
            inventoryUI.SetActive(false);
            equipmentUI.SetActive(false);
            statsUI.SetActive(false);
            mapUI.SetActive(true);
            settingsUI.SetActive(false);
        }
    }
    public void SettingsTab()
    {
        if (settingsTab)
        {
            inventoryTabOpen = false;
            armorTabOpen = false;
            statsTabOpen = false;
            mapTabOpen = false;
            settingsTabOpen = true;
            inventoryUI.SetActive(false);
            equipmentUI.SetActive(false);
            statsUI.SetActive(false);
            mapUI.SetActive(false);
            settingsUI.SetActive(true);
        }
    }
}
