using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI armor;

    public Image healthInd;
   
    public Sprite health1;
    public Sprite health2;
    public Sprite health3;
    public Sprite health4;
    public Sprite health5;
    public Sprite health6;

    public GameObject rk;
    public GameObject bk;
    public GameObject yk;

    private static CanvasManager _instance;
    public static CanvasManager Instance { get { return _instance; } }

    

    
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

       
    }

    private void Update()
    {
        
    }

    public void UpdateHealth(int hV)
    {
        health.text = hV.ToString();
        UpdateHealthInd(hV);
    }

    public void UpdateAmmo(int ammoV)
    {
        ammo.text = ammoV.ToString();
    }

    public void UpdateArmor(int armorV)
    {
        armor.text = armorV.ToString();
    }

    public void UpdateHealthInd(int healthB)
    {
        if(healthB >= 75)
        {
            healthInd.sprite = health1;
        }


        if (healthB < 75 && healthB >= 60)
        {
            healthInd.sprite = health2;
        }

        if (healthB < 60 && healthB >= 45)
        {
            healthInd.sprite = health3;
        }

        if (healthB < 45 && healthB >= 30)
        {
            healthInd.sprite = health4;
        }


        if (healthB < 30 && healthB > 0)
        {
            healthInd.sprite = health5;
        }

        if (healthB <= 0)
        {
            healthInd.sprite = health6;
        }
    }

    public void UpdateKeys(string keyColor)
    {
        if(keyColor == "red")
        {
            rk.SetActive(true);
        }

        if (keyColor == "blue")
        {
            bk.SetActive(true);
        }

        if (keyColor == "yellow")
        {
            yk.SetActive(true);
        }
    }

    public void ClearKeys()
    {
        
            rk.SetActive(false);
        

        
            bk.SetActive(false);
        

        
            yk.SetActive(false);
        
    }
}
