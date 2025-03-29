using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    public PlayerController pc;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pc.OnLevelComplete();
        }
    }
}
