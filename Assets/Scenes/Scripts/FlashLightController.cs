using UnityEngine;
using System.Collections;

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
    private GameObject objecto;
    private bool canToggleFlashlight = true; // Variable para controlar el retraso

    private void Start()
    {
        // Desactiva la linterna al principio
        flashlightObject.SetActive(false);
        StartCoroutine(EnableAudio());
    }

    private void Update()
    {
        // Coge el currentView de la cámara
        objecto = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;

        if (flashlightCamera.activeSelf && objecto.name == "CameraView2")
        {
            AnimatronicLight += Time.deltaTime * 0.5f;
        }
        else
        {
            AnimatronicLight -= Time.deltaTime * 0.5f;
        }

        // Verifica si está la cámara en la derecha o izquierda
        if (objecto.name == "LeftView" || objecto.name == "RightView")
        {
            if (Input.GetKeyDown(KeyCode.F) && canToggleFlashlight)
            {
                if (!flashlightObject.activeSelf)
                {
                    ToggleFlashlight(true);
                    StartCoroutine(DelayToggleFlashlight(0.7f)); // Retraso de 0.7 segundos
                }
            }
            if (Input.GetKeyUp(KeyCode.F) && flashlightObject.activeSelf)
            {
                ToggleFlashlight(false);
            }
        }
        else
        {
            if (objecto.name == "FrontView" || objecto.name == "DeskView")
            {
                // Verifica si está encendida la linterna para apagarla
                if (isFlashlightOn || flashlightCamera.activeSelf)
                {
                    ToggleFlashlight(false);
                    ToggleCameraFlashlight(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F) && canToggleFlashlight)
                {
                    if (!flashlightCamera.activeSelf)
                    {
                        ToggleCameraFlashlight(true);
                        StartCoroutine(DelayToggleFlashlight(0.7f)); // Retraso de 0.7 segundos
                    }
                }
                if (Input.GetKeyUp(KeyCode.F) && flashlightCamera.activeSelf)
                {
                    ToggleCameraFlashlight(false);
                }
            }
        }
    }

    private IEnumerator EnableAudio()
    {
        yield return new WaitForSeconds(1.0f);
        audioManager.SetActive(true);
    }

    private IEnumerator DelayToggleFlashlight(float delayTime)
    {
        canToggleFlashlight = false; // Desactiva la posibilidad de togglear la linterna

        yield return new WaitForSeconds(delayTime); // Espera el tiempo especificado

        canToggleFlashlight = true; // Reactiva la posibilidad de togglear la linterna
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
        // Verifica si está apagada la linterna para encenderla
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



