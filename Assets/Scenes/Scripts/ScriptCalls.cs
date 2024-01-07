using UnityEngine;

public class ControlDeSonido : MonoBehaviour
{
    public AudioSource audioSourceInicio;   // AudioSource para el sonido inicial
    public AudioSource audioSourceFinal;    // AudioSource para el sonido final
    public GameObject triggerObject;        // Objeto que al hacer clic realiza las acciones
    public GameObject objetoADesactivar;    // Objeto que se desactiva al hacer clic
    public GameObject objetoAActivar;       // Objeto que se activa al hacer clic

    private bool sonidoDetenido = false;

    private void Start()
    {
        // Asigna AudioSource desde el Inspector o agrega uno si no se asigna
        audioSourceInicio = audioSourceInicio != null ? audioSourceInicio : gameObject.AddComponent<AudioSource>();
        audioSourceInicio.playOnAwake = false;
        audioSourceInicio.Play();

        // Asigna AudioSource para el sonido final desde el Inspector o agrega uno si no se asigna
        audioSourceFinal = audioSourceFinal != null ? audioSourceFinal : gameObject.AddComponent<AudioSource>();
        audioSourceFinal.playOnAwake = false;
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
}











