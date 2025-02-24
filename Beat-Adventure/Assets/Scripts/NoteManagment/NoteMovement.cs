using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la nota

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
