using UnityEngine;
using Unity.Cinemachine;



public class RandomSceneTransition : MonoBehaviour
{
    // Example


    [Header("List of cinemachine cameras")]
    [SerializeField] public CinemachineCamera[] targetCameras;

    [Header("Use Fade Effect?")]
    [SerializeField] bool fadingIsActive = false;
    [SerializeField] public float fadeTime = 1f;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) { CameraLogic.SwitchCamera(targetCameras[Random.Range(0,targetCameras.Length)], fadingIsActive, fadeTime); }
       
    }
}
