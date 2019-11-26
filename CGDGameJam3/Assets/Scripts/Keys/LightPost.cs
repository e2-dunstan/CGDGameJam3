using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPost : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Light pointLight;
    [SerializeField] private MeshRenderer post;
    [SerializeField] private MeshRenderer postTop;

    private Material postMatInstance;
    private Material postTopMatInstance;

    void Start()
    {
        postMatInstance = post.material;
        postTopMatInstance = postTop.material;
        postMatInstance.DisableKeyword("_EMISSION");
        postTopMatInstance.DisableKeyword("_EMISSION");
    }

    public void SetLightActive(bool isActive)
    {
        pointLight.gameObject.SetActive(isActive);

        if (isActive)
        {

            postMatInstance.EnableKeyword("_EMISSION");
            postTopMatInstance.EnableKeyword("_EMISSION");
        }
        else
        {
            postMatInstance.DisableKeyword("_EMISSION");
            postTopMatInstance.DisableKeyword("_EMISSION");
        }
    }
}
