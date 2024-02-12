using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage;

    [SerializeField] private AudioClip impactSound;
    private Animator anim;
    private BoxCollider2D boxCollier;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollier = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if( lifetime >1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gây sats thương cho nhân vật
        if(collision.tag == "Enemy")
        {
            hit = true;
            anim.SetTrigger("explode");
            SoundManager.instance.PlaySound(impactSound);
            boxCollier.enabled = false;
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        else if (collision.tag == "TileMap" && collision.tag == "Trap")
        {
            hit = true;
            boxCollier.enabled = false;
            gameObject.SetActive(false); // When this hits a wall, deactivate arrow
        }
        
    }
    public void SetDirection(float _direction)
    {
        //Set hướng đi của mũi tên
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollier.enabled = true;
        float localScaleX = transform.localScale.x;
        if ( Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
        
}
