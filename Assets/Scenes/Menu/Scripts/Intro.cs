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
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public float fadeInOutTime = 5f;

    void Start()
    {
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        float timer = 0f;

        while (timer < fadeInOutTime)
        {
            // Calcula la opacidad basada en el tiempo transcurrido
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInOutTime);
            text1.alpha = alpha;
            text2.alpha = alpha;

            timer += Time.deltaTime;
            yield return null;
        }

        // Espera durante el tiempo de visualización
        yield return new WaitForSeconds(3f);

        timer = 0f;

        while (timer < fadeInOutTime)
        {
            // Calcula la opacidad basada en el tiempo transcurrido
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeInOutTime);
            text1.alpha = alpha;
            text2.alpha = alpha;

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        Menu.SetActive(true);

        backgroundMusic.Play();

        IntroUI.SetActive(false);
    }
}
