using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool disableInput = false;

    enum MovementAxis
    {
        X,
        Z
    }

    [Header("References")] //Made public for footsteps FX
    /*[SerializeField]*/ public GameObject model;

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
    private bool isMoving = false;
    private float animSpeed = 0.0f;
    private Vector3 previousPosition;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = model.GetComponent<Animator>();
        previousPosition = transform.position;
        VFXManager.Instance().CreateParticleSystemForObject(VFXManager.Instance().runningPS, VFXManager.Instance().runningPSList);
        VFXManager.Instance().CreateParticleSystemForObject(VFXManager.Instance().sprintingPS, VFXManager.Instance().sprintingPSList);
    }

    private void Update()
    {
        if (disableInput) return;

        horizontalInput = InputHandler.Instance().GetLeftStickX(playerNum);
        verticalInput = InputHandler.Instance().GetLeftStickY(playerNum);
        sprintActive = InputHandler.Instance().GetSprintHold();
        isMoving = (horizontalInput != 0 || verticalInput != 0);

        if (isMoving && AnimatorCanMove()){
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
            anim.SetBool("Naruto", sprintActive && isMoving);
        }
            if (InputHandler.Instance().GetSprintHold())
            {
                //StopEffect(VFXManager.Instance().runningPSList);
                PlayEffect(VFXManager.Instance().sprintingPSList);
            }
            else
            {
               // StopEffect(VFXManager.Instance().sprintingPSList);
                PlayEffect(VFXManager.Instance().runningPSList);
            }
    }

    private void FixedUpdate()
    {
        if (disableInput) return;

        if ((horizontalInput != 0 || verticalInput != 0) && AnimatorCanMove())
        {
            Vector3 newPos = transform.position;
            float xOffset = horizontalInputAxis == MovementAxis.X ? horizontalInput : verticalInput;
            float xAddition = xOffset * Time.deltaTime * speedMultiplier;
            newPos.x += sprintActive ? xAddition * sprintMultiplier : xAddition;

            float zOffset = horizontalInputAxis == MovementAxis.Z ? horizontalInput : verticalInput;
            float zAddtion = zOffset * Time.deltaTime * speedMultiplier;
            newPos.z += sprintActive ? zAddtion * sprintMultiplier : zAddtion;

            rb.MovePosition(newPos);
            RotateToMovement();
        }
    }

    private void SetAnimatorSpeed()
    {
        animSpeed += Time.deltaTime * 4;
        animSpeed = Mathf.Clamp(animSpeed, 0.0f, sprintActive ? 1.0f : 0.5f);
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


    //For particle systems
    public void StopEffect(List<VFXManager.PartSys> _particleSystemList)
    {
        VFXManager.Instance().StopParticleSystemOnGameObject(gameObject, _particleSystemList);
    }
    public void PlayEffect(List<VFXManager.PartSys> _particleSystemList)
    {
        VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, _particleSystemList);
    }
    public void PlayEffect(List<VFXManager.PartSys> _particleSystemList, Vector3 offset)
    {
        VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, _particleSystemList, offset);
    }
}
