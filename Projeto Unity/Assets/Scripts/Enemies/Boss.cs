using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth;
    private bool isAlive = true;

    [Header("Movement")]
    private bool inAgro;
    public float speed;
    public float agroRange;
    public float stoppingDistance;
    public float retreatDistance;

    [Header("Battle")]
    public GameObject projectile;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public Slider healthBar;

    public GameObject winScreen;
    private Transform player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CheckDistance();
            Shooter(inAgro);
        }

        healthBar.value = currentHealth;
    }

    // Movement methods
    void CheckDistance()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        // checking if is in Agro
        if (distance < agroRange)
        {
            inAgro = true;
        } else {
            inAgro = false;
        }

        // checking what to do
        if (distance > stoppingDistance && distance < agroRange)
        {
            Chase();
        } else if (distance < retreatDistance)
        {
            Retreat();
        } else {
            StopChasing();
        }

        // checking side
        if (player.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        } else if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    void Chase()
    {
        //anim.SetBool("isChasing", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void StopChasing()
    {
        //anim.SetBool("isChasing", false);
        transform.position = transform.position;
    }

    void Retreat()
    {
        //anim.SetBool("isChasing", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
    }

    // Health methods
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("dead", true);
        winScreen.SetActive(true);
        Invoke("Win", 2f);
        isAlive = false;
        Destroy(gameObject, 5f);
    }

    // Shoot methods
    void Shooter(bool inAgro)
    {
        if(inAgro)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            } else 
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    void Win()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
