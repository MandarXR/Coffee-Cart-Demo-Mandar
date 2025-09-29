using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public GameObject customerPrefab;
    public Transform customerSpawnPoint;
    public UIManager uiManager;
    public int money = 0;


    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }


    void Start()
    {
        SpawnCustomer();
        uiManager.UpdateMoney(money);
    }


    public void SpawnCustomer()
    {
        if (customerPrefab != null && customerSpawnPoint != null)
        {
            Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.identity);
        }
    }


    public void AddMoney(int amount, Vector3 fromWorldPos)
    {
        money += amount;
        if (uiManager != null)
        {
            uiManager.PlayCurrencyFly(fromWorldPos);
            uiManager.UpdateMoney(money);
        }
    }
}
