using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class NightsFolder : MonoBehaviour
{
    public Button openButton;
    public Button exitButton;
    public Button deleteButton;
    public Button night1Button;
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
        deleteButton.GetComponent<Button>().onClick.AddListener(DataScript.DeletePlayerData);
        if (night == 2)
        {
            Debug.Log("Night 2");
        }
        // Nights
        night1Button.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(loadNight("Night1")));
    }


    IEnumerator loadNight(string night)
    {
        blackScreen.enabled = true;
        float tiempoInicio = Time.time;
        while (Time.time - tiempoInicio < 0.5f)
        {
            float t = (Time.time - tiempoInicio) / 0.5f;
            blackScreen.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, t);
            yield return null;
        }
        blackScreen.color = Color.black;
        yield return new WaitForSeconds(1.0f);
        loadingImage.gameObject.SetActive(true);
        InvokeRepeating("rotateLoading", 0f, 0.01f);
        yield return new WaitForSeconds(1.0f);
        Cursor.visible = true;
        StartCoroutine(LoadScene(night));
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

    void rotateLoading()
    {
        // Rotar la imagen en el eje Z (ajusta el valor de velocidad según tus necesidades)
        loadingImage.Rotate(0f, 0f, -120f * Time.deltaTime);
    }

    IEnumerator LoadScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
