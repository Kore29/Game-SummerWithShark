using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;
using Unity.VisualScripting;

public class ActivateCamera : MonoBehaviour
{
    public float transitionSpeed;
    public GameObject mainCamera;
    public GameObject uiPanel;
    public GameObject postProcessing;
    public Sprite[] imageState;
    GameObject currentView;
    public int currentCamera;
    Transform[] views;
    public GameObject BatteryScript;
    public AudioClip[] sounds;
    public AudioSource cameraAudio;
    public RawImage glitchImage;
    public GameObject glitchVideo;
    public GameObject TVCube;
    public GameObject CameraLight;

    public AudioSource TvStaticSound;
    public GameObject canvas;
    public bool disabledCamera;

    void Start()
    {
        if (sounds == null || sounds.Length == 0)
        {
            Debug.LogError("La matriz de sonidos no está inicializada o está vacía.");
        }
    }

    void Update()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;

        if (Input.GetKeyDown(KeyCode.Escape) && uiPanel.activeSelf)
        {
            CloseUI();
        }

        if (BatteryScript.GetComponent<BatteryScript>().EnergiaActual <= 0 && disabledCamera == false)
        {
            CloseUI();
            disabledCamera = true;
            CameraLight.SetActive(false);
            TVCube.SetActive(false);
            Debug.Log("Closed UI");
        }

    }

    void OnMouseDown()
    {
        if (currentView.name == "FrontView")
        {
            GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(3, false);
            Invoke("OpenUI", 0.3f);
        }
    }

    void OpenUI()
    {
        uiPanel.SetActive(true);
        postProcessing.SetActive(true);
        Camera.main.fieldOfView = 39f;
        TvStaticSound.Play();


        canvas.gameObject.SetActive(false);

        Button backBtn = GameObject.Find("BackCamera").GetComponent<Button>();
        backBtn.onClick.AddListener(CloseUI);

        for (int i = 1; i <= 7; i++)
        {
            Button camBtn = GameObject.Find($"Cam{i}Btn").GetComponent<Button>();
            int index = i - 1;
            camBtn.onClick.AddListener(() => OnButtonClicked(index));
        }

        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(currentCamera, true);
        BatteryScript.GetComponent<BatteryScript>().ModifyUsage(true);
    }

    public void CloseUI()
    {
        uiPanel.SetActive(false);
        postProcessing.SetActive(false);
        Camera.main.fieldOfView = 60f;
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(3, true, true);
        Invoke("CloseAnimation", 0.05f);
        BatteryScript.GetComponent<BatteryScript>().ModifyUsage(false);

        canvas.gameObject.SetActive(true);
        TvStaticSound.Stop();
    }

    void CloseAnimation()
    {
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(0, false);
    }

    void OnButtonClicked(int index)
    {
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(index, true);
        currentCamera = index;

        StartCoroutine(changeCameraEffect());
        for (int i = 1; i <= 7; i++)
        {
            Button button = GameObject.Find($"Cam{i}Btn").GetComponent<Button>();
            button.GetComponent<Image>().sprite = (i == index + 1) ? imageState[1] : imageState[0];
        }
    }



    public IEnumerator changeCameraEffect()
    {
        glitchImage.color = new Color(glitchImage.color.r, glitchImage.color.g, glitchImage.color.b, 1f);
        yield return new WaitForSeconds(0.2f);
        glitchImage.color = new Color(glitchImage.color.r, glitchImage.color.g, glitchImage.color.b, 0.2f);
    }
}