using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject Menu; // Componente a activar
    public GameObject IntroUI; // Componente a activar
    public AudioSource backgroundMusic; // Referencia al AudioSource de la música
    public RectTransform loadingImage;

    void Start()
    {
        // Desactivar el icono de carga al inicio del juego
        loadingImage.gameObject.SetActive(false);

        // Invocar el método para mostrar el icono de carga después de 2 segundos
        Invoke("MostrarIconoDeCarga", 2f);
    }

    void MostrarIconoDeCarga()
    {
        // Activar el icono de carga
        loadingImage.gameObject.SetActive(true);

        // Rotar la imagen durante 3 segundos (puedes ajustar este valor)
        Invoke("finishLoading", 5f);
        InvokeRepeating("rotateLoading", 0f, 0.01f); // Comienza a rotar la imagen
    }

    void rotateLoading()
    {
        // Rotar la imagen en el eje Z (ajusta el valor de velocidad según tus necesidades)
        loadingImage.Rotate(0f, 0f, -120f * Time.deltaTime);
    }

    void finishLoading()
    {
        // Desactivar el icono de carga
        loadingImage.gameObject.SetActive(false);
        // Desactiva la imagen de carga
        Menu.SetActive(true);

        backgroundMusic.Play();

        IntroUI.SetActive(false);
        // Detener la rotación
        CancelInvoke("RotarIconoDeCarga");
    }
}
