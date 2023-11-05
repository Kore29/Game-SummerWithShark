using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRightScript : MonoBehaviour
{
    private Quaternion closedRotation;
    private Quaternion openRotation;

    public bool isOpen = false;
    private bool isAnimating = false;
    private float animationDuration = 0.1f; // Ajusta la duraci�n de la animaci�n
    GameObject currentView;
    public int currentCamera;
    // AudioSource asignado en el objeto door
    public AudioSource audioSource;

    // Clips de audio
    public AudioClip closeDoorAudio;
    public AudioClip openDoorAudio;


    void Start()
    {
        // Define las rotaciones cerrada y abierta de la puerta
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, -180, 0); // Ajusta los valores seg�n tu necesidad
    }

    void Update()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;
        // Verifica si se mantiene presionada la tecla de espacio
        if (Input.GetKey(KeyCode.Space) && currentView.name == "RightView")
        {
            if (!isOpen && !isAnimating)
            {
                animationDuration = 0.2f;
                StartCoroutine(AnimateDoor(openRotation));

                audioSource.clip = closeDoorAudio;
                audioSource.Play();
            }
        }
        else
        {
            if (isOpen && !isAnimating)
            {
                animationDuration = 0.5f;
                StartCoroutine(AnimateDoor(closedRotation));

                audioSource.clip = openDoorAudio;
                audioSource.Play();
            }
        }
    }

    // Corutina para animar la puerta de manera fluida
    private IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        isAnimating = true;
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;

        while (elapsedTime < animationDuration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = !isOpen;
        isAnimating = false;
    }
}
