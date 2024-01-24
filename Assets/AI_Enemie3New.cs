using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemie3New : MonoBehaviour
{
    public Transform[] puntosRuta;
    public float tiempoEsperaMin = 1f;
    public float tiempoEsperaMax = 3f;
    public int indicePuntoActual = 0;

    public float maxTime = 10f;
    public float currentTime = 0f;
    public float tiempoRestante = 20f;
    public float tiempoPresionando = 0f;

    public bool salvado = false;
    public bool isMoving = false;
    public bool isPuerta = false;
    public bool puertaCerrada = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
