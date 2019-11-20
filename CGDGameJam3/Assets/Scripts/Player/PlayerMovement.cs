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
    [SerializeField] private float rotationDampening = 10.0f;
    [SerializeField] private MovementAxis horizontalInputAxis = MovementAxis.X;
    [SerializeField] private MovementAxis verticalInputAxis = MovementAxis.Z;


    private Rigidbody rb;
    private CapsuleCollider col;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;
    private Vector3 previousPosition;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        previousPosition = transform.position;
        VFXManager.Instance.CreateParticleSystemForObject(VFXManager.Instance.runningPS, VFXManager.Instance.runningListPS);
    }

    private void Update()
    {
        horizontalInput = InputHandler.Instance().GetHorizontalInput();
        verticalInput = InputHandler.Instance().GetVerticalInput();
    }

    private void FixedUpdate()
    {
        if(horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 newPos = transform.position;
            float xOffset = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
            float xAddition = xOffset * Time.deltaTime * speedMultiplier;
            newPos.x += InputHandler.Instance().GetSprintHold() ? xAddition * sprintMultiplier : xAddition;

            float zOffset = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;
            float zAddtion = zOffset * Time.deltaTime * speedMultiplier;
            newPos.z += InputHandler.Instance().GetSprintHold() ? zAddtion * sprintMultiplier : zAddtion;

            VFXManager.Instance.PlayParticleSystemOnGameObject(gameObject, VFXManager.Instance.runningListPS);
            rb.MovePosition(newPos);
            RotateToMovement();
        }
    }

    private void RotateToMovement()
    {
        Vector3 direction = this.transform.position - previousPosition;
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        previousPosition = transform.position;
        Quaternion dersiredRotation = Quaternion.LookRotation(new Vector3(localDirection.x, 0f, localDirection.z));
        model.transform.localRotation = Quaternion.Lerp(model.transform.localRotation, dersiredRotation, Time.deltaTime * rotationDampening);
    }
}
