using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;


    public bool requiresKey;

    public bool rR, rB, rY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(requiresKey)
            {
                if(rR && other.GetComponent<PlayerInventory>().hasRed)
                {
                    anim.SetTrigger("Open");

                }

                if (rB && other.GetComponent<PlayerInventory>().hasBlue)
                {
                    anim.SetTrigger("Open");

                }

                if (rY && other.GetComponent<PlayerInventory>().hasYellow)
                {
                    anim.SetTrigger("Open");

                }
            }
            else
            {
                anim.SetTrigger("Open");

            }

           
        }
    }
}
