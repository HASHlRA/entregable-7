using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    // Velocidad
    public float directionSpeed = 8f;

    // Límite en X
    private float xRange = 14f;

    void Update()
    {
        // Mueve el objeto en el eje horizontal
        transform.Translate(Vector3.right * directionSpeed * Time.deltaTime);

        // Si el objeto supera los límites
        if ((transform.position.x > xRange && directionSpeed > 0)
            || (transform.position.x < -xRange && directionSpeed < 0))
        {
            // Destruye el objeto
            Destroy(gameObject);
        }
    }
}
