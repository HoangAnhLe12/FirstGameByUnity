using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private AudioClip arrowSound;

    private Animator anim;
    private Player_movement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
        playerMovement = GetComponent<Player_movement>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown) //&& playerMovement.canAttack()
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        float facingDirection = playerMovement.FacingDirection;
        arrowPoint.localPosition = new Vector3(arrowPoint.localPosition.x * facingDirection, arrowPoint.localPosition.y, arrowPoint.localPosition.z);
        arrows[FindArrow()].transform.position = arrowPoint.position;
        arrows[FindArrow()].GetComponent<projectile>().SetDirection(facingDirection);
        arrowPoint.localPosition = new Vector3(arrowPoint.localPosition.x * facingDirection, arrowPoint.localPosition.y, arrowPoint.localPosition.z);
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
