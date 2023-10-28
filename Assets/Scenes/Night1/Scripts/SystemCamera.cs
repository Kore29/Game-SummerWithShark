using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemCamera : MonoBehaviour
{

    // Variables
    public Transform[] views;
    public Transform[] cameras;
    public float transitionSpeed;
    public Transform currentView;

    public float sensitivity = 2.0f; // Sensibilidad del movimiento de la camara
    public float maxMovement = 1.0f; // Máximo desplazamiento lateral permitido

    private Vector3 initialPosition;

    public Image blackScreen;

    public bool isActive = true;

    public bool isCamera = false;
    public bool deskView = false;
    public bool deskViewAnim = false;

    void Start()
    {
        // Asigna el currentView a la posición
        currentView = transform;
        initialPosition = transform.position;
        currentView = views[0];
        StartCoroutine(Desvanecer());
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

        // Verifica si esta en frente y si no está en la camara
        if (currentView == views[0] && !isCamera || currentView == views[4])
        {
            // Obtener el movimiento del ratón en ambas direcciones.
            float mouseX = Input.mousePosition.x / Screen.width;
            float mouseY = Input.mousePosition.y / Screen.height;
            float offsetZ = -(mouseX - 0.5f) * 2.0f * maxMovement;
            float offsetY = (mouseY - 0.5f) * 2.0f * maxMovement;

            // Calcular la nueva posición de la camara.
            Vector3 newPosition = initialPosition + new Vector3(0, offsetY, offsetZ);

            // Aplicar suavemente el movimiento.
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * sensitivity);
        }

        // Cuando aprete la letra D
        if (Input.GetKeyDown(KeyCode.D))
        {

            // Verifica si esta en frente y si no está en la camara
            if (currentView == views[0] && !isCamera && !deskView)
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
            // Verifica si esta en frente y si no está en la camara
            if (currentView == views[0] && !isCamera && !deskView)
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
            if (deskView && !deskViewAnim)
            {
                StartCoroutine(ToFirst());
            }
        }

        // Cuando aprete la letra S
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Verifica si esta en frente y si no está en la camara
            if (currentView == views[0] && !isCamera && !deskView && !deskViewAnim)
            {
                StartCoroutine(ToDesk());
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
            // Hace un movimiento instantáneo
            transform.position = currentView.position;
            transform.rotation = currentView.rotation;
        }
    }

    // Función que se utiliza para cambiar de posición la camara dependiendo del view que elijas, se utiliza solo para scripts de afuera
    public void SetCurrentView(int index, bool isCameraBool, bool instant = false)
    {
        isCamera = isCameraBool;

        // Si no es una camara o si lo quiere hacer instantáneo
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
