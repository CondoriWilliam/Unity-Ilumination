using UnityEngine;

public class OnOffLight : MonoBehaviour
{
    [SerializeField] private GameObject _lightRoom1;
    [SerializeField] private GameObject _lightRoom2;
    [SerializeField] private GameObject _lightRoom3;
    [SerializeField] private Light _linterna;
    private bool isLight1On = true;
    private bool isLight2On = true;
    private bool isLight3On = true;
    private float cooldownTime = 0.5f; // Tiempo de espera en segundos
    private float lastInteractionTime = -0.5f; // Última vez que se realizó la interacción

    private void Update()
    {
        onOffLinterna();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el tiempo de espera ha pasado
        if (Time.time >= lastInteractionTime + cooldownTime)
        {
            // Comprobar si el jugador colisiona con un interruptor
            if (other.CompareTag("SwitchRoom1"))
            {
                isLight1On = !isLight1On;
                _lightRoom1.SetActive(isLight1On);
                lastInteractionTime = Time.time; 
            }
            else if (other.CompareTag("SwitchRoom2"))
            {
                isLight2On = !isLight2On;
                _lightRoom2.SetActive(isLight2On);
                lastInteractionTime = Time.time; 
            }
            else if (other.CompareTag("SwitchRoom3"))
            {
                isLight3On = !isLight3On;
                _lightRoom3.SetActive(isLight3On);
                lastInteractionTime = Time.time; 
            }
        }
    }

    private void onOffLinterna() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _linterna.enabled = !_linterna.enabled;
        }
    }
}
