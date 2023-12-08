using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemie3 : MonoBehaviour
{
    public Transform[] puntosRuta; // Array de puntos de la ruta
    public float tiempoEsperaMin = 1f; // Tiempo m�nimo de espera
    public float tiempoEsperaMax = 3f; // Tiempo m�ximo de espera

    public int indicePuntoActual = 0;
    public bool puertaAbierta = false;

    void Start()
    {
        // Inicia el movimiento del animatr�nico
        StartCoroutine(MoverAnimatronico());
    }

    IEnumerator MoverAnimatronico()
    {
        while (true)
        {
            // Teletransporta al siguiente punto de la ruta
            transform.position = puntosRuta[indicePuntoActual].position;

            // Si es el �ltimo punto de la ruta, activa la variable puerta
            if (indicePuntoActual == puntosRuta.Length - 1)
            {
                puertaAbierta = true;
                yield return new WaitForSeconds(10f); // Espera 10 segundos antes de reiniciar
            }
            else
            {
                // Espera un tiempo aleatorio antes de pasar al siguiente punto
                float tiempoEspera = Random.Range(tiempoEsperaMin, tiempoEsperaMax);
                yield return new WaitForSeconds(tiempoEspera);
            }

            // Si la puerta est� abierta, reinicia el �ndice y la variable puerta
            if (puertaAbierta)
            {
                indicePuntoActual = 0;
                puertaAbierta = false;
            }
            else
            {
                // Avanza al siguiente punto de la ruta
                indicePuntoActual = (indicePuntoActual + 1) % puntosRuta.Length;
            }
        }
    }
}
