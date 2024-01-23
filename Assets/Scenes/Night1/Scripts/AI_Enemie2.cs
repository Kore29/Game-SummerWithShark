using System.Collections;
using UnityEngine;

public class AI_Enemie2 : MonoBehaviour
{
    public Transform[] puntosRuta;
    public float tiempoEsperaMin = 1f;
    public float tiempoEsperaMax = 3f;
    public int indicePuntoActual = 0;
    public bool isPuerta = false;

    public float maxTime = 3f;
    public float currentTime = 0f;
    public float tiempoRestante = 20f;
    public bool salvado = false;
    public bool isMoving = false;

    public GameObject FlashLight;
    public AudioSource Jumpscare;
    public float tiempoEspera = 30f;  // tiempo en segundos antes de poder reproducir el sonido nuevamente
    private bool puedeReproducir = true;
    private float tiempoUltimaReproduccion;

    public AI_Enemie3 ai3;

    public AudioSource stepsAudio;

    GameObject currentView;
    public bool puertaCerrada = false;
    public ActivateCamera activateCameraScript;

    public bool animation = false;
    public Light Light;
    void Start()
    {

    }

    void Update()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;
        if (isPuerta)
        {
            if (tiempoRestante > 0f && !salvado)
            {
                tiempoRestante -= Time.deltaTime;

                if (currentView.name == "DeskView")
                {
                    salvado = true;
                    StartCoroutine(DeskAnimation());
                }
            }
            else if (!salvado)
            {
                puertaCerrada = false;
                Debug.Log("perdiste");
            }
        }

        // Verificar
        if (Input.GetKey(KeyCode.F) && currentView.name == "LeftView" && isPuerta == true)
        {
            // Verificar si puede reproducir el sonido
            if (puedeReproducir && !Jumpscare.isPlaying)
            {
                // Reproducir el sonido
                Jumpscare.Play();

                // Actualizar el tiempo de la �ltima reproducci�n
                tiempoUltimaReproduccion = Time.time;

                // Desactivar la capacidad de reproducci�n hasta que haya pasado el tiempo de espera
                puedeReproducir = false;
            }
        }

        if (indicePuntoActual == puntosRuta.Length - 1)
        {
            isPuerta = true;
        }
        else
        {
            if (!isMoving)
            {
                StartCoroutine(MoveAnimatronic());
            }
        }
    }



    IEnumerator MoveAnimatronic()
    {
        isMoving = true;
        //activateCameraScript.PlayRandomSound();
        StartCoroutine(activateCameraScript.changeCameraEffect());
        float tiempoEspera = Random.Range(tiempoEsperaMin, tiempoEsperaMax);
        yield return new WaitForSeconds(tiempoEspera);
        indicePuntoActual = (indicePuntoActual + 1) % puntosRuta.Length;
        transform.position = new Vector3(puntosRuta[indicePuntoActual].position.x, transform.position.y, puntosRuta[indicePuntoActual].position.z);
        isMoving = false;
    }

    IEnumerator DeskAnimation()
    {
        //ai3.disabledd = true;
        Debug.Log("salvado");
        animation = true;
        Light.enabled = false;
        yield return new WaitForSeconds(5f);
        stepsAudio.Play();
        yield return new WaitForSeconds(10f);
        Light.enabled = true;
        indicePuntoActual = 0;
        transform.position = new Vector3(puntosRuta[indicePuntoActual].position.x, transform.position.y, puntosRuta[indicePuntoActual].position.z);
        tiempoRestante = 20f;
        isPuerta = false;
        salvado = false;
        animation = false;
    }

}