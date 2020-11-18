using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        transform.Translate((Vector3.forward * speed) * Time.deltaTime);
    }
}