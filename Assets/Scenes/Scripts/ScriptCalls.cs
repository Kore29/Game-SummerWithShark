using UnityEngine;

public class ControlDeSonido : MonoBehaviour
{
    public AudioSource audioSourceInicio;   // AudioSource para el sonido inicial
    public AudioSource audioSourceFinal;    // AudioSource para el sonido final
    public GameObject triggerObject;        // Objeto que al hacer clic realiza las acciones
    public GameObject objetoADesactivar;    // Objeto que se desactiva al hacer clic
    public GameObject objetoAActivar;       // Objeto que se activa al hacer clic

    public AudioClip[] audioClipsIdioma1;   // Array de clips de audio para el primer idioma
    public AudioClip[] audioClipsIdioma2;   // Array de clips de audio para el segundo idioma

    private bool sonidoDetenido = false;
    private int idiomaActual = 0;           // Variable para rastrear el idioma actual

    private void Start()
    {
        idiomaActual = GameManager.Instance.IdiomaSeleccionado;

        // Asigna AudioSource desde el Inspector o agrega uno si no se asigna
        audioSourceInicio = audioSourceInicio != null ? audioSourceInicio : gameObject.AddComponent<AudioSource>();
        audioSourceInicio.playOnAwake = false;

        // Asigna AudioSource para el sonido final desde el Inspector o agrega uno si no se asigna
        audioSourceFinal = audioSourceFinal != null ? audioSourceFinal : gameObject.AddComponent<AudioSource>();
        audioSourceFinal.playOnAwake = false;

        // Establece el clip de audio inicial
        EstablecerClipIdioma();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == triggerObject && !sonidoDetenido)
                {
                    sonidoDetenido = true;
                    PausarOReanudarSonido();
                    DesactivarYActivarObjetos();
                    ReproducirSonidoFinal();
                }
            }
        }
    }

    private void EstablecerClipIdioma()
    {
        // Selecciona el clip de audio según el idioma actual
        if (idiomaActual == 0 && audioClipsIdioma1.Length > 0)
        {
            audioSourceInicio.clip = audioClipsIdioma1[Random.Range(0, audioClipsIdioma1.Length)];
        }
        else if (idiomaActual == 1 && audioClipsIdioma2.Length > 0)
        {
            audioSourceInicio.clip = audioClipsIdioma2[Random.Range(0, audioClipsIdioma2.Length)];
        }

        // Reproduce el sonido inicial
        audioSourceInicio.Play();
    }

    private void PausarOReanudarSonido()
    {
        if (audioSourceInicio.isPlaying)
        {
            audioSourceInicio.Pause();
        }
        else
        {
            audioSourceInicio.UnPause();
        }
    }

    private void DesactivarYActivarObjetos()
    {
        // Desactiva el objeto actual
        if (objetoADesactivar != null)
        {
            objetoADesactivar.SetActive(false);
        }

        // Activa el nuevo objeto
        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }
    }

    private void ReproducirSonidoFinal()
    {
        // Reproduce el sonido final
        if (audioSourceFinal.clip != null)
        {
            audioSourceFinal.Play();
        }
    }

    // Método para cambiar el idioma (puedes llamar a este método desde tu menú)
    public void CambiarIdioma(int nuevoIdioma)
    {
        if (nuevoIdioma >= 0 && nuevoIdioma <= 1)  // Asegúrate de que el nuevo idioma sea válido
        {
            idiomaActual = nuevoIdioma;
            EstablecerClipIdioma();  // Cambia el clip de audio según el nuevo idioma
        }
    }
}


