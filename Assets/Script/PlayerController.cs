using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare and Initialise variables
    float speed = 10.0f;
    float limit = 19f;
    float jumpForce = 10f;

    float gravityModifier = 2.5f;
    int jumpTimes = 0;
    bool onGround = false;

    Rigidbody playerRb;
    MeshRenderer playerRbr;

    public Material[] mtrls;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRbr = GetComponent<MeshRenderer>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Declare and In it variables to reference to User Interaction inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move Player (GameObject) according to user interactions
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        // Plane A
        if (transform.position.z < -limit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -limit);
            playerRbr.material.color = mtrls[2].color;
        }
        if (transform.position.z > limit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limit);
            playerRbr.material.color = mtrls[3].color;
        }
        if (transform.position.x < -limit)
        {
            transform.position = new Vector3(-limit, transform.position.y, transform.position.z);
            playerRbr.material.color = mtrls[4].color;
        }
        if (transform.position.x > limit)
        {
            transform.position = new Vector3(limit, transform.position.y, transform.position.z);
            playerRbr.material.color = mtrls[5].color;
        }

        JumpPlayer();
    }   

    // Jump Code
    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpTimes < 2)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Track my jump if single jump or double jump
            jumpTimes++;

            // Change color
            playerRbr.material.color = mtrls[0].color;
        }
    }

    // Event Listener for a collision by the GameObject "Player" with another possible GameObject
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpTimes = 0;

            playerRbr.material.color = mtrls[1].color;
        }
    }

}