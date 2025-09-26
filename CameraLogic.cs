using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
public class CameraLogic : MonoBehaviour
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();

    static CinemachineCamera ActiveCamera = null;





    

    public static bool isActivCamera (CinemachineCamera cam)
    {
        return cam == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineCamera newCam, bool fadingIsActive = false, float fadeTime = 0.5f )
    {
        if (fadingIsActive) { ScreenFader.Fade(fadeTime); }
        newCam.Priority = 10;
        ActiveCamera = newCam;
        

        foreach (CinemachineCamera c in cameras)
        {

            if(c != newCam)
            {
                c.Priority = 1;
            }
            
        }
    }

    public static void CamRegister(CinemachineCamera cam)
    {
        cameras.Add(cam);
        Debug.Log(cam.name);
    }
    public static void CamUnRegister(CinemachineCamera cam)
    {
        cameras.Remove(cam);
    }
}