using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public float speed;
    public int attackDamage;

    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<StalkerEnemy>().TakeDamage(attackDamage);
        }
        if (collider.gameObject.CompareTag("ShooterEnemy"))
        {
            collider.gameObject.GetComponent<StalkerShooter>().TakeDamage(attackDamage);
        }
        if (collider.gameObject.CompareTag("Boss"))
        {
            collider.gameObject.GetComponent<Boss>().TakeDamage(attackDamage);
        }

        GameObject effect = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
