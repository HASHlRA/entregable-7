using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Posiciona al player al principio
    private Vector3 defaultPosition = new Vector3(0, 9, 0);

    // Almacena componentes del player y la c�mara
    private AudioSource playerAudioSource;
    private Rigidbody playerRigidbody;
    private AudioSource cameraAudioSource;

    // Almacena las part�culas
    public ParticleSystem explosionParticles;
    public ParticleSystem fireworksParticles;

    // Almacena los SFX
    public AudioClip BlipAudio;
    public AudioClip BoingAudio;
    public AudioClip BoomAudio;

    // L�mite de pantalla en Y
    private float yLimit = 14f;

    // Fuerza del impulso hacia arriba
    private float forceValue = 10f;

    // Total de monedas
    private int TotalMoney;

    // Indica la finalizaci�n del juego
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

        // Si supera el l�mite en Y
        if (transform.position.y > yLimit)
        {
            // Mantiene al player a esa posici�n
            transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    // Colisi�n de GameObjects ( Money y Bomb )
    private void OnTriggerEnter(Collider other)
    {
        // Colisiona con el GameObject "Money"
        if (other.gameObject.CompareTag("Money") && !gameOver)
        {
            // Suma +1 al n�mero total de "Money" obtenidos
            TotalMoney++;

            // Reproduce el SFX
            playerAudioSource.PlayOneShot(BlipAudio);

            // Posiciona y reproduce las part�culas en la posici�n de ese GameObject
            fireworksParticles.transform.position = other.gameObject.transform.position;
            fireworksParticles.Play();

            // Destruye el otro GameObject
            Destroy(other.gameObject);

            // Muestra por consola el texto con el total de monedas
            Debug.Log($"Tienes " + TotalMoney + " d�lares en total.");
        }

        // Si colisiona con una bomba
        if (other.gameObject.CompareTag("Bomb"))
        {
            // Reproduce el SFX
            playerAudioSource.PlayOneShot(BoomAudio);

            // Posiciona y reproduce las part�culas en la posici�n de ese GameObject
            explosionParticles.transform.position = other.gameObject.transform.position;
            explosionParticles.Play();

            // Destruye el otro GameObject
            Destroy(other.gameObject);

            // Llama a la funci�n GameOver
            GameOver();
        }
    }
    // Funci�n que da por finalizado el juego
    private void GameOver()
    {    {
            // Indica que el juego ha finalizado
            gameOver = true;

            // Bajar el volumen de la m�sica de fondo
            cameraAudioSource.volume = 0.01f;

            // Muestra en consola tu resultado
            Debug.Log($"GAME OVER: Has conseguido " + TotalMoney + " monedas.");
        }
    }
}
