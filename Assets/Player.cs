using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }

    public Transform movementDirection;

    protected CharacterController characterController;
    [SerializeField]
    private float movementSpeed, localGravity;
    private float velocityY;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        characterController.Move((movementDirection.forward * Input.GetAxis("Vertical") + movementDirection.right * Input.GetAxis("Horizontal") ) * movementSpeed +Vector3.up * GetVelocityY());//(new Vector3(Input.GetAxis("Vertical"), GetVelocityY(), Input.GetAxis("Horizontal")) * movementSpeed));
    }

    public float GetVelocityXZ()
    {
        Vector3 xzVelocity = characterController.velocity;
        xzVelocity.y = 0;
        return xzVelocity.magnitude;
    }

    float GetVelocityY()
    {
        if (characterController.isGrounded)
        {
            return 0;
        }
        velocityY += localGravity * Time.deltaTime;
        return velocityY;
    }
}
