using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController35 : MonoBehaviour
{
    //Float variables
    float speed = 10;
    float jumpForce = 10;
    float timeCount = 5.0f;
    float zLimit = 30;
    float platformSpeed = 4;

    //Integer variables
    int timeCountInt;
    int PowerUpLeft;
    
    //Boolean variables
    bool onGround = true;
    bool startPos = true;
    bool activeBridge = false;
    bool activePlatform = false;

    //References
    public Animator playerAnim;
    public GameObject bridge;
    public GameObject platform;
    public Text timer;

    float gravityModifier = 2.5f;

    Rigidbody playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ForwardKey();
        BackKey();
        LeftKey();
        RightKey();
        JumpKey();

        if(activeBridge)
        {
            bridgeTimer();
        }

        PowerUpLeft = GameObject.FindGameObjectsWithTag("PowerUp").Length;

        if(transform.position.y <= -5)
        {
            SceneManager.LoadScene("EndScene");
        }

        if(activePlatform)
        {
            StartMove();
        }
    }

    //Ground collision for checking Single Jump
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cone"))
        {
            print("You must collect all PowerUp first.");
            
            if(PowerUpLeft == 0)
            {
                print("Activated Bridge.");

                bridge.transform.rotation = Quaternion.Euler(0, 90, 0);

                activeBridge = true;
            }
        }

        if(other.gameObject.CompareTag("Crate"))
        {
            print("Activated Platform");

            activePlatform = true;
        }

        if(other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    //Moving Platform
    private void StartMove()
    {
        if (platform.transform.position.z > zLimit && startPos)
        {
            platform.transform.Translate(Vector3.forward * Time.deltaTime * -platformSpeed);
        }

        if (platform.transform.position.z <= zLimit)
        {
            startPos = false;
        }

        if (!startPos)
        {
            platform.transform.Translate(Vector3.forward * Time.deltaTime * platformSpeed);
        }

        if (platform.transform.position.z >= 43.25f)
        {
            startPos = true;
        }
    }

    //Timer for the Bridge
    private void bridgeTimer()
    {
        if(timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            timeCountInt = Mathf.RoundToInt(timeCount);

            if(timeCount <= 0)
            {
                bridge.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        timer.GetComponent<Text>().text = "Timer Countdown: " + timeCountInt;
    }

    //Forward Movement
    private void ForwardKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            playerAnim.SetBool("isMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

    //Backward Movement
    private void BackKey()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);

            playerAnim.SetBool("isMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

    //Left Movement
    private void LeftKey()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 270, 0);

            playerAnim.SetBool("isMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

    //Right Movement
    private void RightKey()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);

            playerAnim.SetBool("isMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

    //Jump Code
    private void JumpKey()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("isJump");

            onGround = false;
        }
    }
}
