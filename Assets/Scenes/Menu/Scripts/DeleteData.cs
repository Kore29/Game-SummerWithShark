using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using TMPro;

public class DeleteData : MonoBehaviour
{
    public Button openButton;
    public Button exitButton;
    public Button yesButton;
    public Button noButton;
    public TextMeshProUGUI deleteText;
    public bool isPanelOpen = false;

    void Start()
    {
        PlayerData loadedData = DataScript.LoadPlayerData();
        int night = loadedData.night;
        transform.localScale = Vector3.zero;
        openButton.GetComponent<Button>().onClick.AddListener(openFolder);
        exitButton.GetComponent<Button>().onClick.AddListener(closeFolder);
        noButton.GetComponent<Button>().onClick.AddListener(closeFolder);
        yesButton.GetComponent<Button>().onClick.AddListener(deleteData);
    }

    public void openFolder()
    {
        if (!isPanelOpen)
        {
            transform.LeanScale(new Vector3(1f, 1f, 1f), 0.2f);
            isPanelOpen = true;
        }
    }

    public void deleteData()
    {
        DataScript.DeletePlayerData();
        deleteText.text = "All data has been deleted.";
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    public void closeFolder()
    {
        if (isPanelOpen)
        {
            // Abre el panel (hace que vuelva al tamaño normal)
            transform.LeanScale(Vector3.zero, 0.2f).setOnComplete(() => {
                isPanelOpen = false;
                deleteText.text = "Are you sure to delete all progress?";
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
            });
        }
    }
}
