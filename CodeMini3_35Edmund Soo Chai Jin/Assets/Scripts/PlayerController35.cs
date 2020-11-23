using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController35 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    

    bool onGround = true;
    public Animator playerAnim;
    public GameObject bridge;

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
        JumpKey();
    }

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
            Debug.Log("Activated Bridge");
            bridge.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

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
