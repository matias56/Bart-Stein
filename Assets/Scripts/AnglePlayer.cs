using UnityEngine;

public class AnglePlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 tarPos;
    private Vector3 tarDir;


    public SpriteRenderer spr;
    public Sprite[] s;


    public float angle;
    public int lastIndex;

    public Sprite dead;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        tarPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        tarDir = tarPos - transform.position;

        angle = Vector3.SignedAngle(tarDir, transform.forward, Vector3.up);

        Vector3 tempScale = Vector3.one;

        if(angle > 0)
        {
            tempScale.x *= -1f;
        }

        spr.transform.localScale = tempScale;

        lastIndex = GetIndex(angle);

        spr.sprite = s[lastIndex];

        if(isDead)
        {
            spr.sprite = dead;
        }
    }

    private int GetIndex(float angle)
    {
        if(angle > -22.5f && angle < 22.6f)
        {
            return 0;
        }

        if (angle >= 22.5f && angle < 67.5f)
        {
            return 7;
        }

        if (angle >= 67.5f && angle < 112.5f)
        {
            return 6;
        }

        if (angle >= 112.5f && angle < 157.5f)
        {
            return 5;
        }

        if (angle <= -157.5f || angle >= 157.5f)
        {
            return 4;
        }

        if (angle >= -157.4f || angle < -112.5f)
        {
            return 3;
        }

        if (angle >= -112.5f || angle < -67.5f)
        {
            return 2;
        }

        if (angle >= -67.5f || angle < -22.5f)
        {
            return 1;
        }

        return lastIndex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, tarPos);
    }
}
