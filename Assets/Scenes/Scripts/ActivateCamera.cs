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
    void Start()
    {
        // uiPanel.SetActive(false);
    }

    void Update()
    {
        currentView = GameObject.Find("MainCamera").GetComponent<SystemCamera>().currentView.gameObject;

        if (Input.GetKeyDown(KeyCode.Escape) && uiPanel.activeSelf)
        {
            CloseUI();
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

    void CloseUI()
    {
        uiPanel.SetActive(false);
        postProcessing.SetActive(false);
        Camera.main.fieldOfView = 60f;
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(3, true, true);
        Invoke("CloseAnimation", 0.05f);
        BatteryScript.GetComponent<BatteryScript>().ModifyUsage(false);
    }

    void CloseAnimation()
    {
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(0, false);
    }

    void OnButtonClicked(int index)
    {
        PlayRandomSound();
        GameObject.Find("MainCamera").GetComponent<SystemCamera>().SetCurrentView(index, true);
        currentCamera = index;

        for (int i = 1; i <= 7; i++)
        {
            Button button = GameObject.Find($"Cam{i}Btn").GetComponent<Button>();
            button.GetComponent<Image>().sprite = (i == index + 1) ? imageState[1] : imageState[0];
        }
    }

    private void PlayRandomSound()
    {
        if (sounds.Length > 0)
        {
            int randomIndex = Random.Range(0, sounds.Length);
            AudioClip randomSound = sounds[randomIndex];
            cameraAudio.clip = randomSound;
            cameraAudio.time = 1.0f;
            cameraAudio.Play();
        }
        else
        {
            Debug.LogError("No hay sonidos asignados en el array.");
        }
    }
}