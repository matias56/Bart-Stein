using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float vertRange = 20f;

    private BoxCollider gunTrigger;

    public EnemyManager enemyManager;

    public float fireRate = 1f;

    private float nextTimeFire;

    public float damage;

    public float smallDamage;

    public LayerMask rayCastLayer;

    public LayerMask enemyLayer;

    public float gunShotRadius = 20f;

    public int maxAmmo;

    private int ammo;

    public Image slingshot;
    public Image spray;

    public Sprite[] gunSprites;  // Drag and drop your 4 sprites into this array in the Inspector
    public Sprite[] spraySprites;

    public float frameDuration = 0.1f;  // Duration of each frame in the shooting animation

    public AudioClip slin;

    public AudioClip spra;

    public AudioSource au;

    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, vertRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);

        ammo = 25;

        CanvasManager.Instance.UpdateAmmo(ammo);

        slingshot.enabled = true;
        spray.enabled = false;

        au = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f && !PlayerMovement.GameIsPaused)
        {
            if (spray.enabled == true)
            {
                SwitchToSlingshot();
            }
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f && !PlayerMovement.GameIsPaused)
        {
            if (slingshot.enabled == true)
            {
                SwitchToSpray();
            }
        }

        if(slingshot.enabled == true)
        {
            gunShotRadius = 20f;
            range = 20f;
            vertRange = 20f;
            if (Input.GetMouseButtonDown(0) && Time.time > nextTimeFire && ammo > 0 && !PlayerMovement.GameIsPaused)
            {
                Fire();
            }
        }
        else if(spray.enabled == true)
        {
            gunShotRadius = 10f;
            range = 10f;
            vertRange = 10f;

            if (Input.GetMouseButtonDown(0) && Time.time > nextTimeFire && !PlayerMovement.GameIsPaused)
            {
                Spray();
            }
        }
       
    }

    void SwitchToSpray()
    {
        slingshot.enabled = false; // Deactivate slingshot
        spray.enabled = true;      // Activate spray
    }

    void SwitchToSlingshot()
    {
        spray.enabled = false;     // Deactivate spray
        slingshot.enabled = true;  // Activate slingshot
    }

    void Fire()
    {
        StartCoroutine(ShootAnimation(slingshot, gunSprites));
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayer);

        foreach(var enemyCol in enemyColliders)
        {
            enemyCol.GetComponent<EnemyAwareness>().isAggro = true;
        }

        au.PlayOneShot(slin);
        foreach(var enemy in enemyManager.enemiesInTrigger)
        {
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;

            if(Physics.Raycast(transform.position, dir, out hit, range * 1.5f, rayCastLayer))
            {
                if(hit.transform == enemy.transform)
                {
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if(dist > range * 0.5f)
                    {
                        enemy.TakeDamage(1f);
                    }
                    else
                    {
                        enemy.TakeDamage(3f);
                    }

                   
                }
            }

            
        }

        nextTimeFire = Time.time + fireRate;
        
        ammo--;
        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    void Spray()
    {
        StartCoroutine(ShootAnimation(spray, spraySprites));
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayer);

        foreach (var enemyCol in enemyColliders)
        {
            enemyCol.GetComponent<EnemyAwareness>().isAggro = true;
        }

        au.PlayOneShot(spra);
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, rayCastLayer))
            {
                if (hit.transform == enemy.transform)
                {
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist <= range)
                    {
                        enemy.TakeDamage(1f);
                    }
                    


                }
            }


        }

        nextTimeFire = Time.time + fireRate;

       
    }

    public void GiveAmmo(int amount, GameObject item)
    {
        if(ammo < maxAmmo)
        {
            ammo += amount;
            Destroy(item);
        }

        if(ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }

        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if(enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }

    IEnumerator ShootAnimation(Image sp, Sprite[] anim)
    {
        for (int i = 0; i < gunSprites.Length; i++)
        {
            sp.sprite = anim[i];  // Set the current frame
            yield return new WaitForSeconds(frameDuration);  // Wait for the frame duration
        }

        // Optionally, reset to the original sprite after the animation
        sp.sprite = anim[0];
    }
}
