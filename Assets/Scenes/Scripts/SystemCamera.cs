using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SystemCamera : MonoBehaviour
{

    // Variables
    public Transform[] views;
    public Transform[] cameras;
    public float transitionSpeed;
    public Transform currentView;

    public AudioClip EsconderseAudioS; // Audio esconderse
    private AudioSource audioSource1;

    public AudioClip EsconderseAudioW; // Audio esconderse
    private AudioSource audioSource2;


    public float sensitivity = 2.0f; // Sensibilidad del movimiento de la camara
    public float maxMovement = 1.0f; // M�ximo desplazamiento lateral permitido

    private Vector3 initialPosition;

    public Image blackScreen;

    public bool isActive = true;

    public bool isCamera = false;
    public bool deskView = false;
    public bool deskViewAnim = false;

    public AI_Enemie2 ai2;
    public TMP_Text BatteryText;
    public Image BatteryIcon;

    void Start()
    {
        // Asigna el currentView a la posici�n
        currentView = transform;
        initialPosition = transform.position;
        currentView = views[0];
        StartCoroutine(Desvanecer());

        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = EsconderseAudioS;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = EsconderseAudioW;

    }

    IEnumerator Desvanecer()
    {
        deskViewAnim = true;
        blackScreen.enabled = true;
        float tiempoInicio = Time.time;
        while (Time.time - tiempoInicio < 2.0f)
        {
            float t = (Time.time - tiempoInicio) / 2.0f;
            blackScreen.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), t);
            yield return null;
        }
        blackScreen.color = new Color(0, 0, 0, 0);
        blackScreen.enabled = false;
        deskViewAnim = false;
    }

    IEnumerator ToDesk()
    {
        deskView = true;
        deskViewAnim = true;
        blackScreen.enabled = true;
        float tiempoInicio = Time.time;
        while (Time.time - tiempoInicio < 0.5f)
        {
            float t = (Time.time - tiempoInicio) / 0.5f;
            blackScreen.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, t);
            yield return null;
        }
        blackScreen.color = Color.black;
        currentView = views[4];
        BatteryText.enabled = false;
        BatteryIcon.enabled = false;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Desvanecer());
    }

    IEnumerator ToFirst()
    {
        deskView = false;
        deskViewAnim = true;
        blackScreen.enabled = true;
        float tiempoInicio = Time.time;
        while (Time.time - tiempoInicio < 0.5f)
        {
            float t = (Time.time - tiempoInicio) / 0.5f;
            blackScreen.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, t);
            yield return null;
        }
        blackScreen.color = Color.black;
        currentView = views[0];
        BatteryText.enabled = true;
        BatteryIcon.enabled = true;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Desvanecer());
    }



    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentView = views[0];
        }
        */

        // Verifica si esta en frente y si no est� en la camara
        if (currentView == views[0] && !isCamera || currentView == views[4])
        {
            // Obtener el movimiento del rat�n en ambas direcciones.
            float mouseX = Input.mousePosition.x / Screen.width;
            float mouseY = Input.mousePosition.y / Screen.height;
            float offsetX = -(mouseX - 0.5f) * 2.0f * maxMovement;
            float offsetY = (mouseY - 0.5f) * 2.0f * maxMovement;

            // Calcular la nueva posici�n de la camara.
            Vector3 newPosition = initialPosition + new Vector3(offsetX, offsetY, 0);

            // Aplicar suavemente el movimiento.
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * sensitivity);
        }

        // Cuando aprete la letra D
        if (Input.GetKeyDown(KeyCode.D))
        {

            // Verifica si esta en frente y si no est� en la camara
            if (currentView == views[0] && !isCamera && !deskView && !ai2.animation)
            {
                currentView = views[1];
            }
            else
            {
                if (!isCamera && !deskView)
                {
                    currentView = views[0];
                }
            }
        }

        // Cuando aprete la letra A
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Verifica si esta en frente y si no est� en la camara
            if (currentView == views[0] && !isCamera && !deskView && !ai2.animation)
            {
                currentView = views[2];
            }
            else
            {
                if (!isCamera && !deskView)
                {
                    currentView = views[0];
                }
            }
        }

        // Cuando aprete la letra W
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (deskView && !deskViewAnim && !ai2.animation)
            {
                StartCoroutine(ToFirst());

                if (audioSource2 != null && !audioSource2.isPlaying)
                {
                    audioSource2.Play();
                }
            }
        }

        // Cuando aprete la letra S
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Verifica si esta en frente y si no est� en la camara
            if (currentView == views[0] && !isCamera && !deskView && !deskViewAnim && !ai2.animation)
            {
                StartCoroutine(ToDesk());

                if (audioSource1 != null && !audioSource1.isPlaying)
                {
                    audioSource1.Play();
                }
            }
        }

    }


    void LateUpdate()
    {
        // Verifica si no es una camara para hacer un movimiento fluido
        if (!isCamera)
        {
            transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.Lerp(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );

            transform.eulerAngles = currentAngle;
        }
        else
        {
            // Hace un movimiento instant�neo
            transform.position = currentView.position;
            transform.rotation = currentView.rotation;
        }
    }

    // Funci�n que se utiliza para cambiar de posici�n la camara dependiendo del view que elijas, se utiliza solo para sacripts de afuera
    public void SetCurrentView(int index, bool isCameraBool, bool instant = false)
    {
        isCamera = isCameraBool;

        // Si no es una camara o si lo quiere hacer instant�neo
        if (!isCamera || instant)
        {
            currentView = views[index];
        }
        else
        {
            currentView = cameras[index];
        }
    }

}
