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
    [SerializeField] private float speedMultiplier = 1000.0f;
    [SerializeField] private MovementAxis horizontalInputAxis = MovementAxis.X;
    [SerializeField] private MovementAxis verticalInputAxis = MovementAxis.Z;
    [SerializeField] private ForceMode rigidbodyForceMode = ForceMode.Force;


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
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
        moveDir.z = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;

        rb.AddForce(moveDir * speedMultiplier * Time.fixedDeltaTime, rigidbodyForceMode);

    }
}
