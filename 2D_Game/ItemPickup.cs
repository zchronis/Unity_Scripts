using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public float radius = .92f;

    public Transform player;

    bool itemTouched = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (!itemTouched)
        {
            if (distance <= radius)
            {
                PickUp();
                itemTouched = true;
            }
        }
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        //Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item);
        //remove item from game world
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
