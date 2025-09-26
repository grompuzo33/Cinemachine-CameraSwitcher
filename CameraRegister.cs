using Unity.Cinemachine;
using UnityEngine;

public class CameraRegister : MonoBehaviour
{
    
    
    
    private void OnEnable()
    {
        CameraLogic.CamRegister(GetComponent<CinemachineCamera>());
    }

    private void OnDisable()
    {
        CameraLogic.CamUnRegister(GetComponent<CinemachineCamera>());
    }
}
