using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private bool hit;
    private BoxCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if(hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = true;
            base.OnTriggerEnter2D(collision); // Execute logic from parent script first
            coll.enabled = false;
            gameObject.SetActive(false); // When this hits the player, deactivate arrow
        }
        else if (collision.CompareTag("TileMap"))
        {
            hit = true;
            coll.enabled = false;
            gameObject.SetActive(false); // When this hits a wall, deactivate arrow
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}