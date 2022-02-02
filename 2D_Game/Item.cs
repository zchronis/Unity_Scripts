using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public ItemType itemType;

    [Header("Health/Magic/Food")]
    public int healthUp;
    public int magicUp;
    public int foodUp;

    [Header("Crafting/Quest/Monitary")]
    public int crafting;
    public int quest;
    public int value;

    [Header("Stat Boosts")]
    public int strengthUp;
    public int intellectUp;
    public int connectionUp;
    public int totalHealthUp;
    public int SoulUp;


    public virtual void Use ()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }
}

public enum ItemType { Healing, Magic, Food, Crafting, Quest, StatBoost, Monitary }
