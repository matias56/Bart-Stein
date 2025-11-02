using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxH;

    public int health;

    public int maxArmor;

    private int armor;

    public float timer = 1.2f;

    public AudioSource zero;

    public bool defeated;

    public AudioSource ayCaramba;

    public CharacterController cc;



    // Start is called before the first frame update
    void Start()
    {
        health = maxH;
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
       cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
       
        
      
               
       
        
    }

    public void DamagePlayer(int damage)
    {
        ayCaramba.GetComponent<AudioSource>().Play();

        if (armor > 0)
        {
            if(armor >= damage)
            {
                armor -= damage;
            }
            else if(armor < damage)
            {
                int remainingDamage;

                remainingDamage = damage - armor;

                armor = 0;

                health -= remainingDamage;
            }
        }
        else
        {
            health -= damage;
        }


        if(health <= 0)
        {

            

            

            health = 0;

            cc.enabled = false;

            GameObject gun = GameObject.FindWithTag("Gun");
            gun.GetComponent<Gun>().enabled = false;

            GetComponent<MouseLook>().enabled = false;

            GetComponent<PlayerMovement>().enabled = false;


            GameObject nelson = GameObject.FindWithTag("Enemy");
            nelson.GetComponent<EnemyAI>().enabled = false;

            defeated = true;
            StartCoroutine(ReloadLevel());




        }

        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
    }

    public void GiveHealth(int amount, GameObject item)
    {
        if(health < maxH)
        {
            health += amount;
            Destroy(item);
        }

        

        if(health > maxH)
        {
            health = maxH;
        }

        CanvasManager.Instance.UpdateHealth(health);
    }

    public void GiveArmor(int amount, GameObject item)
    {
        if (armor < maxArmor)
        {
            armor += amount;
            Destroy(item);
        }

        armor += amount;

        if (armor > maxArmor)
        {
            armor = maxArmor;
        }

        CanvasManager.Instance.UpdateArmor(armor);
    }

    IEnumerator ReloadLevel()
    {
        Scene curScene = SceneManager.GetActiveScene();
        

        yield return new WaitForSeconds(0.5f); // wait time
        zero.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(curScene.buildIndex);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            DamagePlayer(5);
        }

    }
}
