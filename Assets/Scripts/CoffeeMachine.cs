using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public Transform cupSpawnPoint;
    public GameObject cupPrefab;
    public float processTime = 2f;
    bool isProcessing = false;


    // Called by Player when interacting
    public void InsertBag(PlayerController player)
    {
        var stack = player.GetComponent<BeanStack>();
        if (stack != null && stack.Count > 0 && !isProcessing)
        {
            stack.RemoveOne();
            StartCoroutine(ProcessRoutine());
        }
    }


    IEnumerator ProcessRoutine()
    {
        isProcessing = true;
        // simple processing wait
        yield return new WaitForSeconds(processTime);
        SpawnCup();
        isProcessing = false;
    }


    void SpawnCup()
    {
        if (cupPrefab != null && cupSpawnPoint != null)
        {
            Instantiate(cupPrefab, cupSpawnPoint.position, Quaternion.identity);
        }
    }
}
