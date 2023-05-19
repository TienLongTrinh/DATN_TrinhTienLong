using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField]private AudioClip fireballSound;
    private Animator anim;
    private Player playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool andan = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dan"))
        {
            andan = true;
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if(andan)
        {
            if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            {
                Attack();
            }
            cooldownTimer += Time.deltaTime;
        }   
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projecttile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }


    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
