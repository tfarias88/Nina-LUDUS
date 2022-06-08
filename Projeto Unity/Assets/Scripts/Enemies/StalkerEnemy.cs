using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerEnemy : MonoBehaviour
{
    [Header("Combat Variables")]
    public int currentHealth;
    public int attackDamage;
    bool isAlive = true;

    [Header("Movement Variables")]
    public float speed;
    public float chaseDistance;

    [Header("Component Variables")]
    private Transform player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CheckDistance();
        }
    }

    // movement methods
    void CheckDistance()
    {
        if (player == null)
            return;
        
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            Chase();
        } else if (distance > chaseDistance)
        {
            StopChasing();
        }
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        anim.SetBool("isChasing", true);

        if (player.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        } else if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    void StopChasing()
    {
        anim.SetBool("isChasing", false);
        transform.position = transform.position;
    }


    // Combat methods
    void Die()
    {
        anim.SetBool("death", true);
        isAlive = false;
        Destroy(gameObject, 1f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlive)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
    }
}
