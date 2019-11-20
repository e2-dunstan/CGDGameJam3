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

    [Header("References")]
    [SerializeField] private GameObject model;

    [Header("Settings")]
    [SerializeField] private float speedMultiplier = 5.0f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private MovementAxis horizontalInputAxis = MovementAxis.X;
    [SerializeField] private MovementAxis verticalInputAxis = MovementAxis.Z;


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

        Vector3 newPos = transform.position;
        float xOffset = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
        float xAddition = xOffset * Time.deltaTime * speedMultiplier;
        newPos.x += InputHandler.Instance().GetSprintHold() ? xAddition * sprintMultiplier : xAddition;

        float zOffset = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;
        float zAddtion = zOffset * Time.deltaTime * speedMultiplier;
        newPos.z += InputHandler.Instance().GetSprintHold() ? zAddtion * sprintMultiplier : zAddtion;

        TurnModelToFaceDir(newPos);
        rb.MovePosition(newPos);
    }

    private void TurnModelToFaceDir(Vector3 newPosition)
    {
        newPosition.y = 0.5f;
        model.transform.LookAt(newPosition, Vector3.up);
    }


}
