using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Variables
    public float timeInterval = 10f;
    public TextMeshProUGUI clockText;
    private int currentHour = 0;
    private bool isRunning = true;
    public TextMeshProUGUI textDisappear;
    public TextMeshProUGUI textAppear;
    public GameObject CamerasUI;
    public GameObject WinUI;

    public float fadeDuration = 1.0f;

    private Color startColor;
    private Color endColor;

    private void Start()
    {
        // Bucle para actualizar el reloj
        StartCoroutine(updateClock());
    }

    IEnumerator updateClock()
    {
        // Verifica si esta corriendo el reloj y que sea debajo o igual que 6
        while (isRunning && currentHour <= 6)
        {
            // Tiempo para ejecutar la función
            yield return new WaitForSeconds(timeInterval);

            currentHour++;

            clockText.text = currentHour + " AM";

            // Si la hora es 0, entonces se muestra 12 AM
            if (currentHour == 0)
            {
                clockText.text = "12 AM";
            }

            // Si la hora es 6, entonces muestra el final y desactiva el reloj
            if (currentHour == 6)
            {
                final();
                isRunning = false;
            }
        }
    }

    // La función del final
    private void final()
    {
        WinUI.SetActive(true);
        CamerasUI.SetActive(false);
        // Realizar animación
        StartCoroutine(AnimateTextSwap());
    }

    IEnumerator AnimateTextSwap()
    {
        yield return new WaitForSeconds(3.0f);
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;

            textDisappear.color = Color.Lerp(Color.white, Color.clear, normalizedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textDisappear.color = Color.clear;

        // Pausa de un segundo antes de mostrar el nuevo texto
        yield return new WaitForSeconds(1.0f);

        // Aparece el nuevo texto
        textAppear.gameObject.SetActive(true);
        elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            textAppear.color = Color.Lerp(Color.clear, Color.white, normalizedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textAppear.color = Color.white;

        PlayerData playerData = DataScript.LoadPlayerData();
        playerData.night = 2;
        DataScript.SavePlayerData(playerData);

        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Menu");
    }
}
