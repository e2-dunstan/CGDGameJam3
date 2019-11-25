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
    [SerializeField] private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    [HideInInspector] public int playerNum = 1;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator anim;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;
    private bool sprintActive = false;
    private float animSpeed = 0.0f;
    private Vector3 previousPosition;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = model.GetComponent<Animator>();
        previousPosition = transform.position;
        VFXManager.Instance().CreateParticleSystemForObject(VFXManager.Instance().runningPS, VFXManager.Instance().runningListPS);
    }

    private void Update()
    {
        horizontalInput = InputHandler.Instance().GetLeftStickX(playerNum);
        verticalInput = InputHandler.Instance().GetLeftStickY(playerNum);
        sprintActive = InputHandler.Instance().GetSprintHold();

        if ((horizontalInput != 0 || verticalInput != 0) && AnimatorCanMove()){
            SetAnimatorSpeed();
        }
        else
        {
            if(animSpeed > 0)
            {
                animSpeed -= Time.deltaTime * 4;
                animSpeed = animSpeed < 0 ? 0 : animSpeed;
            }
            anim.SetFloat("Speed", animSpeed);
        }
    }

    private void FixedUpdate()
    {
        if ((horizontalInput != 0 || verticalInput != 0) && AnimatorCanMove())
        {
            Vector3 newPos = transform.position;
            float xOffset = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
            float xAddition = xOffset * Time.deltaTime * speedMultiplier;
            newPos.x += sprintActive ? xAddition * sprintMultiplier : xAddition;

            float zOffset = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;
            float zAddtion = zOffset * Time.deltaTime * speedMultiplier;
            newPos.z += sprintActive ? zAddtion * sprintMultiplier : zAddtion;

            VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, VFXManager.Instance().runningListPS);
            rb.MovePosition(newPos);
            RotateToMovement();
        }
    }

    private void SetAnimatorSpeed()
    {
        animSpeed += Time.deltaTime * 4;
        animSpeed = Mathf.Clamp(animSpeed, 0.0f, 1.0f);
        anim.SetBool("Naruto", sprintActive);
        anim.SetFloat("Speed", animSpeed);
    }

    private bool AnimatorCanMove()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") || anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion Injured") || anim.GetCurrentAnimatorStateInfo(0).IsName("Naruto");
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
