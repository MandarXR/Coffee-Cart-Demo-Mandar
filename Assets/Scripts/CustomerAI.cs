using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public Transform serveSpot;
    public int price = 5;


    void Start()
    {
        if (serveSpot != null)
            transform.position = serveSpot.position;
    }


    public void ReceiveCoffee(PlayerController player)
    {
        // ensure player carries a cup
        var cup = player.stackAnchor.GetComponentInChildren<CoffeeCup>();
        if (cup != null)
        {
            cup.DeliverToCustomer(this);
            OnServed();
        }
    }


    void OnServed()
    {
        GameManager.Instance.AddMoney(price, transform.position);
        // simple vanish and respawn
        Destroy(gameObject);
        GameManager.Instance.SpawnCustomer();
    }
}
