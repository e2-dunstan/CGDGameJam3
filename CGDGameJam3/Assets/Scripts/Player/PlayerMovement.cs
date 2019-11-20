using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    enum MovementAxis
    {
        X,
        Z
    }

    [Header("Settings")]
    [SerializeField] private float speedMultiplier = 5.0f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    //[SerializeField] private float turnDampener = 0.5f;
    [SerializeField] private MovementAxis horizontalInputAxis = MovementAxis.X;
    [SerializeField] private MovementAxis verticalInputAxis = MovementAxis.Z;
    //[SerializeField] private ForceMode rigidbodyForceMode = ForceMode.Force;


    private Rigidbody rb;
    private CapsuleCollider col;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        horizontalInput = InputHandler.Instance().GetHorizontalInput();
        verticalInput = InputHandler.Instance().GetVerticalInput();

        Vector3 currentPos = transform.position;
        float xOffset = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
        float xAddition = xOffset * Time.deltaTime * speedMultiplier;
        currentPos.x += InputHandler.Instance().GetSprintHold() ? xAddition * sprintMultiplier : xAddition;

        float zOffset = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;
        float zAddtion = zOffset * Time.deltaTime * speedMultiplier;
        currentPos.z += InputHandler.Instance().GetSprintHold() ? zAddtion * sprintMultiplier : zAddtion;

        transform.position = currentPos;
    }

    //private void FixedUpdate()
    //{
    //    Vector3 directVel = rb.velocity;
    //    Vector3 moveDir = Vector3.zero;
        
    //    //For x velocity
    //    float xAcc = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
    //    directVel.x = (rb.velocity.x >= 0) ^ (xAcc < 0) ? directVel.x : directVel.x * turnDampener;
    //    moveDir.x =  xAcc;

    //    float zAcc = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;
    //    directVel.z = (rb.velocity.z >= 0) ^ (zAcc < 0) ? directVel.z : directVel.z * turnDampener;
    //    moveDir.z = zAcc;

    //    rb.velocity = directVel;
    //    rb.AddForce(moveDir * speedMultiplier * Time.fixedDeltaTime, rigidbodyForceMode);
    //}
}
