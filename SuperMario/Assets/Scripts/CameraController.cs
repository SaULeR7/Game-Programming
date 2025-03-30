using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
            transform.position = cameraPosition;
        }
    }
}
