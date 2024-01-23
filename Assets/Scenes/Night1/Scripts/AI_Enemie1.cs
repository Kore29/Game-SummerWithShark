using System.Collections;
using UnityEngine;

public class AI_Enemie1 : MonoBehaviour
{
    public Transform[] camino1Waypoints;
    public Transform[] camino2Waypoints;

    private Transform[] puntosRutaActual;
    private int indicePuntoActual = 0;
    private bool isMoving = false;

    void Start()
    {
        // Seleccionar aleatoriamente el camino al inicio
        if (Random.Range(0, 2) == 0)
            puntosRutaActual = camino1Waypoints;
        else
            puntosRutaActual = camino2Waypoints;

        // Iniciar el movimiento
        StartCoroutine(MoveAnimatronic());
    }

    void Update()
    {
        // Puedes agregar lógica adicional aquí si es necesario
    }

    IEnumerator MoveAnimatronic()
    {
        isMoving = true;

        // Espera aleatoria antes de teletransportarse al siguiente waypoint
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        // Teletransportarse al siguiente waypoint
        indicePuntoActual = (indicePuntoActual + 1) % puntosRutaActual.Length;
        transform.position = new Vector3(puntosRutaActual[indicePuntoActual].position.x, transform.position.y, puntosRutaActual[indicePuntoActual].position.z);

        isMoving = false;

        // Llamar recursivamente a la coroutine para continuar el movimiento
        StartCoroutine(MoveAnimatronic());
    }
}

