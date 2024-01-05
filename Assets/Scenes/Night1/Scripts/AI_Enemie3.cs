using System.Collections;
using UnityEngine;

public class AI_Enemie3 : MonoBehaviour
{
    public Transform[] puntosRuta;
    public float tiempoEsperaMin = 1f;
    public float tiempoEsperaMax = 3f;
    public int indicePuntoActual = 0;
    public bool isPuerta = false;

    public float maxTime = 10f;
    public float currentTime = 0f;
    public float tiempoRestante = 20f;
    public bool salvado = false;
    public bool isMoving = false;

    public float tiempoPresionando = 0f;
    GameObject currentView;
    public bool puertaCerrada = false;
    public ActivateCamera activateCameraScript;
    void Start()
    {
    }

    void Update()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;
        if (isPuerta)
        {
            if (Input.GetKey(KeyCode.F) && currentView.name == "RightView" && puertaCerrada == false)
            {
                Debug.Log("perdiste");
            }
            if (tiempoRestante > 0f && !salvado)
            {
                tiempoRestante -= Time.deltaTime;

                if (Input.GetKey(KeyCode.Space) && currentView.name == "RightView")
                {
                    puertaCerrada = true;
                    tiempoPresionando += Time.deltaTime;

                    if (tiempoPresionando >= 5f)
                    {
                        salvado = true;
                        Debug.Log("salvado");
                        indicePuntoActual = 0;
                        transform.position = new Vector3(puntosRuta[indicePuntoActual].position.x, transform.position.y, puntosRuta[indicePuntoActual].position.z);
                        tiempoRestante = 20f;
                        tiempoPresionando = 0f;
                        isPuerta = false;
                        salvado = false;
                    }
                }
                else
                {
                    puertaCerrada = false;
                    // Reiniciar el tiempo de presionado si la tecla no está siendo presionada
                    tiempoPresionando = 0f;
                }
            }
            else if (!salvado)
            {
                if (Input.GetKey(KeyCode.Space) && currentView.name == "RightView")
                {
                    puertaCerrada = true;
                    tiempoPresionando += Time.deltaTime;

                    if (tiempoPresionando >= 5f)
                    {
                        salvado = true;
                        Debug.Log("salvado");
                        indicePuntoActual = 0;
                        transform.position = new Vector3(puntosRuta[indicePuntoActual].position.x, transform.position.y, puntosRuta[indicePuntoActual].position.z);
                        tiempoRestante = 20f;
                        tiempoPresionando = 0f;
                        isPuerta = false;
                        salvado = false;
                    }
                }
                else
                {
                    puertaCerrada = false;
                    Debug.Log("perdiste");
                }
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
        activateCameraScript.PlayRandomSound();
        StartCoroutine(activateCameraScript.changeCameraEffect());
        float tiempoEspera = Random.Range(tiempoEsperaMin, tiempoEsperaMax);
        yield return new WaitForSeconds(tiempoEspera);
        indicePuntoActual = (indicePuntoActual + 1) % puntosRuta.Length;
        transform.position = new Vector3(puntosRuta[indicePuntoActual].position.x, transform.position.y, puntosRuta[indicePuntoActual].position.z);
        Vector3 targetRotationEuler = new Vector3(transform.rotation.x, puntosRuta[indicePuntoActual].rotation.y, transform.rotation.z);
        Quaternion targetRotation = Quaternion.Euler(targetRotationEuler);
        transform.rotation = targetRotation;
        isMoving = false;
    }

}