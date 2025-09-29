using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSource : MonoBehaviour
{
    // Infinite supply. On interact, give one bag to player if possible.
    public void PickBean(PlayerController player)
    {
        var stack = player.GetComponent<BeanStack>();
        if (stack != null && stack.CanAdd)
        {
            stack.AddOne();
            // optional sound or VFX here
        }
    }
}
