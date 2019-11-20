using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchRotation : MonoBehaviour
{ 

    InputHandler inputHandler = InputHandler.Instance();
    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotateToMouse();
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
}
