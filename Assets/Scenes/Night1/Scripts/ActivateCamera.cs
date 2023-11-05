using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class ActivateCamera : MonoBehaviour
{

    // Variables
    public float transitionSpeed;
    public GameObject mainCamera;
    public GameObject uiPanel;
    public Sprite[] imageState;
    GameObject currentView;
    public int currentCamera;
    Transform[] views;

    public GameObject BatteryScript;
    void Start()
    {
        // uiPanel.SetActive(false);
    }

    void Update()
    {
        // Coge el currentView 
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;

        // Si activa el Escape y esta en las camaras, entonces se cierra
        if (Input.GetKeyDown(KeyCode.Escape) && uiPanel.activeSelf)
        {
            closeUI();
        }
    }

    void OnMouseDown()
    {
        // Cuando aprete al TV debe estar en frente para abrir las camaras
        if (currentView.name == "FrontView")
        {
            GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(3, false);
            // El tiempo para abrir las camaras y hacer la animación
            Invoke("openUI", 0.3f);
        }

    }

    // Funciones para abrir las camaras (se puede mejorar)
    void OnButton1Clicked()
    {
        // Cambia de camara
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(0, true);

        // Cambia la camara al que esta usando
        currentCamera = 0;
        // Cambia la imagen del botón a la versión seleccionada
        Button button1 = GameObject.Find("Cam1Btn").GetComponent<Button>();
        button1.GetComponent<Image>().sprite = imageState[1];

        // Restaura la imagen del otro botón a la versión no seleccionada
        Button button2 = GameObject.Find("Cam2Btn").GetComponent<Button>();
        button2.GetComponent<Image>().sprite = imageState[0];
    }

    void OnButton2Clicked()
    {
        // Cambia de camara
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(1, true);

        // Cambia la camara al que esta usando
        currentCamera = 1;
        // Cambia la imagen del botón a la versión seleccionada
        Button button1 = GameObject.Find("Cam1Btn").GetComponent<Button>();
        button1.GetComponent<Image>().sprite = imageState[0];

        // Restaura la imagen del otro botón a la versión no seleccionada
        Button button2 = GameObject.Find("Cam2Btn").GetComponent<Button>();
        button2.GetComponent<Image>().sprite = imageState[1];
    }

    // Función para abrir la UI
    void openUI()
    {
        uiPanel.SetActive(true);

        // Activa el efecto de TV
        //GameObject.Find("MainCamera").GetComponent<CRTPostEffecter>().enabled = true;

        // Las funciones de los botones

        Button BackButton = GameObject.Find("BackCamera").GetComponent<Button>();
        BackButton.onClick.AddListener(closeUI);

        Button button1 = GameObject.Find("Cam1Btn").GetComponent<Button>();
        button1.onClick.AddListener(OnButton1Clicked);

        Button button2 = GameObject.Find("Cam2Btn").GetComponent<Button>();
        button2.onClick.AddListener(OnButton2Clicked);

        // Cambia a la camara donde estaba antes
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(currentCamera, true);
        BatteryScript.GetComponent<BatteryScript>().ModifyUsage(true);
    }

    // Función para cerrar la UI
    void closeUI()
    {
        // Desactiva el efecto de TV
        //GameObject.Find("MainCamera").GetComponent<CRTPostEffecter>().enabled = false;
        // Desactiva la UI
        uiPanel.SetActive(false);
        // Se pone en la posición de la TV instantáneo para luego hacer la función de cerrar.
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(3, true, true);
        // Espera hasta que haga la animación de cerrar
        Invoke("closeAnimation", 0.05f);
        BatteryScript.GetComponent<BatteryScript>().ModifyUsage(false);
    }

    void closeAnimation()
    {
        // Hace la función de cerrar
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(0, false);
    }
 }

