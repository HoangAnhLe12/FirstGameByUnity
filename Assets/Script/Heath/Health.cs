using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    //[SerializeField] private AudioClip hurtSound;

    //LootTable
    [Header("LootItem")]
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField]private int minItems ;
    [SerializeField] private int maxItems;
    private float dropRadius = 1.5f;



    private bool invulnerable;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    
    public void TakeDamage(float _damage)
    {

        if (invulnerable)
        {
           return;
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // anim.SetTrigger("hurt");
            //SoundManager.instance.PlaySound(hurtSound);
           StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("death");
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    
    private void Deactivate()
    {
        //Go around loottable
        //Spawn item
        DropItems();

        gameObject.SetActive(false);
    }

    private void DropItems()
    {
        int itemCount = GetRandomItemCount();

        for (int i = 0; i < itemCount; i++)
        {
            GameObject randomPrefab = GetRandomPrefab();
            if (randomPrefab != null)
            {
                InstantiateItem(randomPrefab);
            }
        }
    }

    private int GetRandomItemCount()
    {
        return Random.Range(minItems, maxItems + 1);
    }

    private GameObject GetRandomPrefab()
    {
        if (itemPrefabs.Count == 0)
        {
            Debug.LogWarning("No item prefabs assigned.");
            return null;
        }

        int randomIndex = Random.Range(0, itemPrefabs.Count );
        return itemPrefabs[randomIndex];       
    }

    private void InstantiateItem(GameObject prefab)
    {
        float playerY = GetPlayerY();
        // Lấy vị trí ngẫu nhiên xung quanh đối thủ
        Vector3 randomOffset = Random.onUnitSphere * dropRadius;
        

        // Đặt vị trí Z bằng 0 để đảm bảo vật phẩm không bị nổi lên trên trục Z
        randomOffset.y = 0f;
        randomOffset.z = 0f;
        
        // Tạo ra một vật phẩm độc lập và đặt nó tại vị trí ngẫu nhiên
        GameObject item = Instantiate(prefab, transform.position + randomOffset, Quaternion.identity);
    }

    private float GetPlayerY()
    {
        // Đảm bảo rằng player tồn tại trong scene
        if (GameObject.FindWithTag("Player") != null)
        {
            // Lấy giá trị trục y của player
            return GameObject.FindWithTag("Player").transform.position.y;
            //Debug.Log(GameObject.FindWithTag("Player").transform.position.y);
        }

        // Trong trường hợp không tìm thấy player, trả về giá trị mặc định (ví dụ: 0)
        return 0f;
    }
}