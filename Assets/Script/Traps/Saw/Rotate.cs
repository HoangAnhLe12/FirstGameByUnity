using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float damage;
    private void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player_Life>().TakeDamage(damage);
        }
    }
}
