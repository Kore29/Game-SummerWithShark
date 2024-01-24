using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class IdiomeFolder : MonoBehaviour
{
    public Button openButton;
    public Button exitButton;
    public Button night1Button;
    public Button night2Button;
    public Image blackScreen;
    public RectTransform loadingImage;
    public bool isPanelOpen = false;

    void Start()
    {
        PlayerData loadedData = DataScript.LoadPlayerData();
        int night = loadedData.night;
        transform.localScale = Vector3.zero;
        openButton.GetComponent<Button>().onClick.AddListener(openFolder);
        exitButton.GetComponent<Button>().onClick.AddListener(closeFolder);
    }

    public void openFolder()
    {
        if (!isPanelOpen)
        {
            transform.LeanScale(new Vector3(1f, 1f, 1f), 0.2f);
            isPanelOpen = true;
        }
    }

    public void closeFolder()
    {
        if (isPanelOpen)
        {
            // Abre el panel (hace que vuelva al tamaño normal)
            transform.LeanScale(Vector3.zero, 0.2f);
            isPanelOpen = false;
        }
    }
}
