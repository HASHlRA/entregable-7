using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Posiciona al player al principio
    private Vector3 defaultPosition = new Vector3(0, 9, 0);

    // Almacena componentes del player y la cámara
    private AudioSource playerAudioSource;
    private Rigidbody playerRigidbody;
    private AudioSource cameraAudioSource;

    // Almacena las partículas
    public ParticleSystem explosionParticles;
    public ParticleSystem fireworksParticles;

    // Almacena los SFX
    public AudioClip BlipAudio;
    public AudioClip BoingAudio;
    public AudioClip BoomAudio;

    // Límite de pantalla en Y
    private float yLimit = 14f;

    // Fuerza del impulso hacia arriba
    private float forceValue = 10f;

    // Total de monedas
    private int TotalMoney;

    // Indica la finalización del juego
    public bool gameOver;


    // Principio del juego
    void Start()
    {
        // Pone el juego en marcha
        gameOver = false;

        // Posiciona al player
        transform.position = defaultPosition;

        // Accede a los componentes necesarios
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

    }

    // Por cada frame del juego
    void Update()
    {
        // (Juego en marcha), pulsamos el espacio
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            // Aplica una fuerza vertical de modo impulso
            playerRigidbody.AddForce(Vector3.up * forceValue, ForceMode.Impulse);
        }

        // Si supera el límite en Y
        if (transform.position.y > yLimit)
        {
            // Mantiene al player a esa posición
            transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    // Colisión de GameObjects ( Money y Bomb )
    private void OnTriggerEnter(Collider other)
    {
        // Colisiona con el GameObject "Money"
        if (other.gameObject.CompareTag("Money") && !gameOver)
        {
            // Suma +1 al número total de "Money" obtenidos
            TotalMoney++;

            // Reproduce el SFX
            playerAudioSource.PlayOneShot(BlipAudio);

            // Posiciona y reproduce las partículas en la posición de ese GameObject
            fireworksParticles.transform.position = other.gameObject.transform.position;
            fireworksParticles.Play();

            // Destruye el otro GameObject
            Destroy(other.gameObject);

            // Muestra por consola el texto con el total de monedas
            Debug.Log($"Tienes " + TotalMoney + " dólares en total.");
        }

        // Si colisiona con una bomba
        if (other.gameObject.CompareTag("Bomb"))
        {
            // Reproduce el SFX
            playerAudioSource.PlayOneShot(BoomAudio);

            // Posiciona y reproduce las partículas en la posición de ese GameObject
            explosionParticles.transform.position = other.gameObject.transform.position;
            explosionParticles.Play();

            // Destruye el otro GameObject
            Destroy(other.gameObject);

            // Llama a la función GameOver
            GameOver();
        }
    }
    // Función que da por finalizado el juego
    private void GameOver()
    {    {
            // Indica que el juego ha finalizado
            gameOver = true;

            // Bajar el volumen de la música de fondo
            cameraAudioSource.volume = 0.01f;

            // Muestra en consola tu resultado
            Debug.Log($"GAME OVER: Has conseguido " + TotalMoney + " monedas.");
        }
    }
}
