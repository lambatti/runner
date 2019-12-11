using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusObject;

    private Vector2 focusPosition;

    public float followDistance = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        focusPosition = focusObject.transform.position;

        Vector3 distance = focusPosition - (Vector2)transform.position;

        if (distance.magnitude > followDistance)
        {
            Vector3 moveDistance = Vector2.ClampMagnitude(distance, distance.magnitude - followDistance);
            transform.position += new Vector3(moveDistance.x, moveDistance.y, 0);
        }

        //transform.position = new Vector3(focusPosition.x, focusPosition.y, transform.position.z);
    }
}