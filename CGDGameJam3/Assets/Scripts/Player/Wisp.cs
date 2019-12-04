using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    public Transform torch;
    public float followSpeed = 1;

    void Update()
    {
        Vector3 targetLoc = torch.position + torch.forward;
        targetLoc.y = transform.position.y;

        Vector3 targetLookVec = targetLoc - torch.position;
        Vector3 currentLookVec = transform.position - torch.position;

        float angle = Vector3.SignedAngle(currentLookVec, targetLookVec, Vector3.up);

        if (Mathf.Abs(angle) > 5f)
        {
            transform.RotateAround(torch.position, torch.up, angle * Time.deltaTime * followSpeed);
        }
    }
}
