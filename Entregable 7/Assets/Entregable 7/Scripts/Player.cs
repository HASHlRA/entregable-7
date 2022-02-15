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
}
