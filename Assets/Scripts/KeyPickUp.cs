using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public bool isRed, isBlue, isYellow;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isRed)
            {
                other.GetComponent<PlayerInventory>().hasRed=true;
                CanvasManager.Instance.UpdateKeys("red");
            }

            if (isBlue)
            {
                other.GetComponent<PlayerInventory>().hasBlue = true;
                CanvasManager.Instance.UpdateKeys("blue");

            }

            if (isYellow)
            {
                other.GetComponent<PlayerInventory>().hasYellow = true;
                CanvasManager.Instance.UpdateKeys("yellow");

            }

            Destroy(gameObject);
        }
    }
}
