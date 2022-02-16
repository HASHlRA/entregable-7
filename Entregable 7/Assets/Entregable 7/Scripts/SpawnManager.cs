using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Guarda los prefabs
    public GameObject[] objectsPrefab;

    // Guarda el script del player
    private Player playerScript;

    // Guarda la posición
    private Vector3 randomPosition;

    // Guarda la dirección
    private int direction;

    // Guarda el objeto
    private int randomObject;

    // Guarda el objeto que ya ha sido spawneado
    private GameObject spawnedObject;

    // Guarda la pausa entre cada spawn
    private float[] rangeInvoke = { 0, 1 };

    // Guarda la dirección
    private float[] spawnDirection = { 1f, -1f };

    // Guarda la posición
    private float[] spawnSide = { -14f, 14f };

    // Genera valores aleatorios en la posición
    private int[] rangeRandomPosition = { 2, 14 };

    // Genera valores aleatorios en la dirección
    private int[] rangeDirection = { 0, 2 };

    // Genera una aleatoriedad enntre los objetos
    private int[] rangeRandomObject = { 0, 2 };


    void Start()
    {
        // Obtiene la componente del PLayer
        playerScript = FindObjectOfType<Player>();

        // Llama a la función cada cierto tiempo
        InvokeRepeating("spawnRandomObject", rangeInvoke[0], rangeInvoke[1]);
    }


    private void spawnRandomObject()
    {
        // Si el juego sigue en marcha
        if (!playerScript.gameOver)
        {
            // Objeto aleatorio
            randomObject = Random.Range(rangeRandomObject[0], rangeRandomObject[1]);

            // Dirección aleatoria
            direction = Random.Range(rangeDirection[0], rangeDirection[1]);

            // Altura aleatoria
            randomPosition.y = Random.Range(rangeRandomPosition[0], rangeRandomPosition[1]);

            // Posición según la dirección aleatoria
            randomPosition.x = spawnSide[direction];

            // Spawnea el objeto
            spawnedObject = Instantiate(objectsPrefab[randomObject], randomPosition, objectsPrefab[randomObject].transform.rotation);

            // Cambia la dirección del objeto spawneado
            spawnedObject.GetComponent<MoveRight>().directionSpeed *= spawnDirection[direction];
        }
    }
}
