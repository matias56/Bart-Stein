using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprites : MonoBehaviour
{
    private Transform target;
    public bool canLookVertical;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(canLookVertical)
        {
            transform.LookAt(target);
        }
        else
        {
            Vector3 modTarget = target.position;
            modTarget.y = transform.position.y;

            transform.LookAt(modTarget);
        }
    }
}
