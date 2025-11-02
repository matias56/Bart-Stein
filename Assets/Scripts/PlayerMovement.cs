using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private CharacterController cc;
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 moveVector;
    private float gravity = -10f;
    public float momentumDamping = 5f;


    public PlayerHealth pH;

    public CanvasManager can;

    public EnemyAI nelson;

    public GameObject end;

    public AudioSource finish;

    public GameObject pauseMenuPanel;

    // A variable to track the game state
    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        end.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        

        camAnim.SetBool("isWalking", isWalking);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // 1. Hide the menu
        pauseMenuPanel.SetActive(false);

        // 2. Set time scale back to normal
        Time.timeScale = 1f;

        // 3. Update the state
        GameIsPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        // 1. Show the menu
        pauseMenuPanel.SetActive(true);

        // 2. Freeze the game time
        Time.timeScale = 0f;

        // 3. Update the state
        GameIsPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    void GetInput()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
            isWalking = false;
        }
        

        

        moveVector = (inputVector * speed) + (Vector3.up * gravity);
    }

    void MovePlayer()
    {
        cc.Move(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Final"))
        {
            end.SetActive(true);
            
            GameObject gun = GameObject.FindWithTag("Gun");
            gun.GetComponent<Gun>().enabled = false;

            GetComponent<MouseLook>().enabled = false;

            GetComponent<PlayerMovement>().enabled = false;

            finish.Play();

            Time.timeScale = 0;
        }
    }
}
