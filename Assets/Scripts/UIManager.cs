using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text moneyText;
    public RectTransform currencyFlyPrefab; // UI element that flies to score
    public Transform canvasTransform;


    public void UpdateMoney(int money)
    {
        if (moneyText != null) moneyText.text = money.ToString();
    }


    public void PlayCurrencyFly(Vector3 worldFrom)
    {
        StartCoroutine(CurrencyFlyRoutine(worldFrom));
    }


    IEnumerator CurrencyFlyRoutine(Vector3 worldFrom)
    {
        if (currencyFlyPrefab == null || canvasTransform == null || moneyText == null) yield break;
        // convert world to canvas position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldFrom);
        RectTransform fly = Instantiate(currencyFlyPrefab, canvasTransform);
        fly.position = screenPos;
        Vector3 targetPos = moneyText.transform.position;
        float t = 0f;
        float dur = 0.8f;
        while (t < dur)
        {
            t += Time.deltaTime;
            float p = t / dur;
            fly.position = Vector3.Lerp(screenPos, targetPos, p);
            yield return null;
        }
        Destroy(fly.gameObject);
    }
}
