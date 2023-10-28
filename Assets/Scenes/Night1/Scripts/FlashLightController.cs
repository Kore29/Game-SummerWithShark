using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashlightController : MonoBehaviour
{
    // Variables
    public SystemCamera cameraSwitcher;

    public GameObject flashlightObject;
    public GameObject flashlightCamera;
    public GameObject DarkObject;
    public GameObject audioManager;
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;
    public float rotationSpeed = 2f;
    private bool hasPlayed = false;

    public float AnimatronicLight = 100.9f;


    public GameObject BatteryScript;

    private bool isFlashlightOn = false;
    GameObject objecto;

    private void Start()
    {
        // Desactiva la linterna al principio
        flashlightObject.SetActive(false);
        StartCoroutine(enableAudio());
    }

    private void Update()
    {
        // Coge el currentView de la camara
        objecto = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;

        if (flashlightCamera.activeSelf && objecto.name == "CameraView2")
        {
            AnimatronicLight += Time.deltaTime * 0.5f;
        }
        else
        {
            AnimatronicLight -= Time.deltaTime * 0.5f;
        }

        // Verifica si está la camara en la derecha o izquierda
        if (objecto.name == "LeftView" || objecto.name == "RightView")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!flashlightObject.activeSelf)
                {
                    ToggleFlashlight(true);
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (flashlightObject.activeSelf)
                {
                    ToggleFlashlight(false);
                }
            }
        }
        else
        {
            if (objecto.name == "FrontView" || objecto.name == "DeskView")
            {
                // Verifica si esta encendida la linterna para apagarla
                if (isFlashlightOn || flashlightCamera.activeSelf)
                {
                    ToggleFlashlight(false);
                    ToggleCameraFlashlight(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!flashlightCamera.activeSelf)
                    {
                        ToggleCameraFlashlight(true);
                    }
                }
                if (Input.GetKeyUp(KeyCode.F))
                {
                    if (flashlightCamera.activeSelf)
                    {
                        ToggleCameraFlashlight(false);
                    }
                }
            }
        }
    }

    private IEnumerator enableAudio()
    {
        yield return new WaitForSeconds(1.0f);
        audioManager.SetActive(true);
    }

    private void ToggleCameraFlashlight(bool active)
    {
        if (active)
        {
            flashlightCamera.SetActive(true);
            hasPlayed = false;
            // Verifica si ya se reprodujo el audio
            if (!hasPlayed)
            {
                audioManager.GetComponent<AudioSource>().PlayOneShot(turnOnSound);
            }
            hasPlayed = true;
            BatteryScript.GetComponent<BatteryScript>().ModifyUsage(true);
        }
        else
        {
            flashlightCamera.SetActive(false);
            hasPlayed = false;
            // Verifica si ya se reprodujo el audio
            if (!hasPlayed)
            {
                audioManager.GetComponent<AudioSource>().PlayOneShot(turnOffSound);
            }
            hasPlayed = true;
            BatteryScript.GetComponent<BatteryScript>().ModifyUsage(false);
        }
    }

    private void ToggleFlashlight(bool active)
    {
        // Verifica si esta apagada la linterna para encenderla
        if (active)
        {
            isFlashlightOn = true;
            hasPlayed = false;
            // Verifica si ya se reprodujo el audio
            if (!hasPlayed)
            {
                audioManager.GetComponent<AudioSource>().PlayOneShot(turnOnSound);
            }
            hasPlayed = true;
            DarkObject.SetActive(false);
            flashlightObject.SetActive(true);
            BatteryScript.GetComponent<BatteryScript>().ModifyUsage(true);
        }
        else
        {
            audioManager.GetComponent<AudioSource>().enabled = true;
            isFlashlightOn = false;
            hasPlayed = false;
            // Verifica si ya se reprodujo el audio
            if (!hasPlayed)
            {
                audioManager.GetComponent<AudioSource>().PlayOneShot(turnOffSound);
            }
            hasPlayed = true;
            DarkObject.SetActive(true);
            flashlightObject.SetActive(false);
            BatteryScript.GetComponent<BatteryScript>().ModifyUsage(false);

        }
    }

}


