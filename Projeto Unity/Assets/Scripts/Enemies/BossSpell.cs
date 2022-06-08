using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : MonoBehaviour
{
    public float speed;
    public int attackDamage;

    private Transform player;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.gameObject.tag != "ShooterEnemy") && (collider.gameObject.tag != "Enemy")
        && (collider.gameObject.tag != "Orb") && (collider.gameObject.tag != "Boss"))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<Player>().TakeDamage(attackDamage);
            }

            GameObject effect = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
