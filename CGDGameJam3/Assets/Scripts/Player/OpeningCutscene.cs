using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutscene : MonoBehaviour
{
    [System.Serializable]
    public class CutsceneCamera
    {
        public Vector3 position;
        public Vector3 rotation;
        public float time;
    }
    public CutsceneCamera[] cutscenePositions;
    private int currentPosition = 0;
    private float timeToUseThisPosition;

    private Camera defaultCamera;
    private Camera cutsceneCamera;
    [SerializeField] private Animator playerAnim;

    void Start()
    {
        defaultCamera = Camera.main;
        defaultCamera.enabled = false;
        cutsceneCamera = GetComponentInChildren<Camera>();
        cutsceneCamera.enabled = true;

        timeToUseThisPosition = cutscenePositions[currentPosition].time;
        cutsceneCamera.transform.localPosition = cutscenePositions[currentPosition].position;
        cutsceneCamera.transform.localEulerAngles = cutscenePositions[currentPosition].rotation;

        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        float timeElapsed = 0;
        while (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StandUp"))
        {
            timeElapsed += Time.deltaTime;
            UpdateCamera(timeElapsed);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        cutsceneCamera.enabled = false;
        defaultCamera.enabled = true;

        Destroy(gameObject, 1.0f);
    }

    private void UpdateCamera(float timeElapsed)
    {
        if (timeElapsed > timeToUseThisPosition)
        {
            currentPosition++;
            if (currentPosition >= cutscenePositions.Length) return;
            timeToUseThisPosition += cutscenePositions[currentPosition].time;

            cutsceneCamera.transform.localPosition = cutscenePositions[currentPosition].position; 
            cutsceneCamera.transform.localEulerAngles = cutscenePositions[currentPosition].rotation;
        }
    }
}
