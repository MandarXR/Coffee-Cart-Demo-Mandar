using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    public float pickRadius = 1f;


    public void PickUp(PlayerController player)
    {
        // Parent to player stack anchor (keeps as single carried cup)
        transform.SetParent(player.stackAnchor);
        transform.localPosition = new Vector3(0, 0, 1.75f);
        var rb = GetComponent<Rigidbody>();
        if (rb != null) Destroy(rb);
        // mark as carried
        gameObject.tag = "CarriedCup";
    }


    public void DeliverToCustomer(CustomerAI customer)
    {
        // destroy cup on delivery
        Destroy(gameObject);
    }
}
