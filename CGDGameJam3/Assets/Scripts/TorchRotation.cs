using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchRotation : MonoBehaviour
{ 

    InputHandler inputHandler = InputHandler.Instance();
    public float moveSpeed = 5.0f;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.GetActiveInput() == InputHandler.ActiveInput.MOUSE)
        {
            rotateToMouse();
        }
        else
        {
            rotateWithController();
        }
    }

    private void rotateToMouse()
    {
        var playerPlane = new Plane(Vector3.up, transform.position);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            var targetPoint = ray.GetPoint(hitdist);

            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }
    }

    private void rotateWithController()
    {
        if (inputHandler.GetHorizontal2Input(1) != 0.0f || inputHandler.GetVertical2Input(1) != 0.0f)
        {

            Vector3 targetPoint;

            var x = transform.position.x + inputHandler.GetHorizontal2Input(player.playerNum);
            var z = transform.position.z + inputHandler.GetVertical2Input(player.playerNum);
            targetPoint = new Vector3(x, transform.position.y, z);

            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }
    }
}
