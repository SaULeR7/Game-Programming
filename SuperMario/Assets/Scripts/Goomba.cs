using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatPic;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform playerTransform = collision.gameObject.transform;

            if (playerTransform.position.y > transform.position.y + 0.10f)
            {
                GetComponent<SpriteRenderer>().sprite = flatPic;
                GetComponent<EnemyMovement>().enabled = false;
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
