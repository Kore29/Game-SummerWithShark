using System.Collections;
using UnityEngine;

public class AI_Enemie2 : MonoBehaviour
{
    public Transform[] puntosRuta;
    public Transform WayPointPerdido;
    public float tiempoEsperaMin = 25f;
    public float tiempoEsperaMax = 30f;
    public int indicePuntoActual = 0;

    public float maxTime = 3f;
    public float currentTime = 0f;
    public float tiempoRestante = 20f;
    public bool salvado = false;
    public bool isMoving = false;
    public bool isPuerta = false;
    public bool puertaCerrada = false;
    private bool puedeReproducir = true;
    public bool animation = false;

    public GameObject FlashLight;
    public Light Light;

    public float tiempoEspera = 15f;
    private float tiempoUltimaReproduccion;

    public AudioSource stepsAudio;
    public AudioSource Jumpscare;
    public AudioSource JumpscareGeneral;

    GameObject currentView;
    public ActivateCamera activateCameraScript;

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

        if (Input.GetKey(KeyCode.F) && currentView.name == "LeftView" && isPuerta == true)
        {
            if (puedeReproducir && !Jumpscare.isPlaying)
            {
                Jumpscare.Play();
                tiempoUltimaReproduccion = Time.time;
                puedeReproducir = true;
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