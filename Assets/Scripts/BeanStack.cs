using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanStack : MonoBehaviour
{
    public Transform anchor;
    public GameObject beanVisualPrefab; // small bag visual
    public int maxStack = 3;


    List<GameObject> visuals = new List<GameObject>();


    public int Count => visuals.Count;


    public bool CanAdd => visuals.Count < maxStack;


    public void AddOne()
    {
        if (!CanAdd) return;
        Vector3 pos = anchor.position + Vector3.up * (0.25f * visuals.Count);
        GameObject go = Instantiate(beanVisualPrefab, pos, Quaternion.identity, anchor);
        go.transform.localPosition = new Vector3(0, 0.15f * visuals.Count, 0);
        visuals.Add(go);
    }


    public bool RemoveOne()
    {
        if (visuals.Count == 0) return false;
        GameObject top = visuals[visuals.Count - 1];
        visuals.RemoveAt(visuals.Count - 1);
        Destroy(top);
        return true;
    }
}
