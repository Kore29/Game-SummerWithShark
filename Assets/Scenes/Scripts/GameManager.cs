using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int IdiomaSeleccionado = 0;
    public GameObject idiom0;
    public GameObject idiom1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (idiom0 != null)
        {
            idiom0.GetComponent<Button>().onClick.AddListener(OnClickIdiom0);
        }
        if (idiom1 != null)
        {
            idiom1.GetComponent<Button>().onClick.AddListener(OnClickIdiom1);
        }
    }

    void OnClickIdiom0()
    {

        IdiomaSeleccionado = 0;
        Debug.Log("Idioma seleccionado: 0");
    }

    void OnClickIdiom1()
    {
        IdiomaSeleccionado = 1;
        Debug.Log("Idioma seleccionado: 1");
    }
}

