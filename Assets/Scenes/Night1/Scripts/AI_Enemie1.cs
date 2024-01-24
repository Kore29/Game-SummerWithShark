using System.Collections;
using UnityEngine;

public class AI_Enemie1 : MonoBehaviour
{


    public Transform[] puntosRuta;
    public float tiempoEsperaMin = 1f;
    public float tiempoEsperaMax = 3f;
    public int indicePuntoActual = 0;
    public bool isPuerta = false;
    public AudioSource audioSource;

    public float maxTime = 10f;
    public float currentTime = 0f;
    public float tiempoRestante = 20f;
    public bool salvado = false;
    public bool isMoving = false;

    public float tiempoPresionando = 0f;
    public bool puertaCerrada = false;
    public ActivateCamera activateCameraScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {


        if (indicePuntoActual == puntosRuta.Length - 1)
        {
            Debug.Log("Has perdido.");
        }
        else
        {
            //      audioSource.volume = 0f; // Mutea si no est�s en "RightView"
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
        float tiempoEspera = Random.Range(tiempoEsperaMin, tiempoEsperaMax);
        yield return new WaitForSeconds(tiempoEspera);
        indicePuntoActual = (indicePuntoActual + 1) % puntosRuta.Length;
        transform.position = new Vector3(puntosRuta[indicePuntoActual].position.x, transform.position.y, puntosRuta[indicePuntoActual].position.z);
        isMoving = false;
    }

}