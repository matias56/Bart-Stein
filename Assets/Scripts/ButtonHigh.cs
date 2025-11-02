using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHigh : MonoBehaviour
{
    public GameObject icon;  // Drag your icon GameObject here
    public float flashSpeed = 1f;  // Adjust the speed of the flashing

    private Image iconImage;
    private Coroutine flashCoroutine;

    void Start()
    {
        iconImage = icon.GetComponent<Image>();
        icon.SetActive(false);  // Start with the icon hidden
    }

    public void OnButtonHover(BaseEventData eventData)
    {
        icon.SetActive(true);
        

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashIcon());
    }

    public void OnButtonExit(BaseEventData eventData)
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        icon.SetActive(false);
    }

    private IEnumerator FlashIcon()
    {
        while (true)
        {
            iconImage.enabled = !iconImage.enabled;
            yield return new WaitForSeconds(1f / flashSpeed);
        }
    }

    
}
