using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Rigidbody2D enemyRigidBody;
    public float speed;
    public float maxHealth;
    float health;

    Transform target;
    public int currentWaypoint;
    GameController cont;
    public float rotationSpeed;
    float distance;

    public float damage;

    bool canMove = true;

    public float money;

    public GameObject explosion;

    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        cont = FindObjectOfType<GameController>();
        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    private void OnEnable()
    {
        health = maxHealth;

        currentWaypoint = 0;
        target = cont.waypoints[currentWaypoint];
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        enemyRigidBody.AddForce(transform.up * speed * Time.deltaTime);

        if (canMove) enemyRigidBody.AddForce(transform.up * speed * Time.deltaTime);

        distance = Vector2.Distance(transform.position, target.position);

        if (distance <= 0.5f)
        {
            if (currentWaypoint < cont.waypoints.Length - 1)
            {
                canMove = false;
                Invoke("CanMove", 1f);
                currentWaypoint++;
                target = cont.waypoints[currentWaypoint];
            }
            else
            {
                cont.TakeDamage(damage);
                Destroy(gameObject);
            }

        }
       
    }

    void CanMove()
    {
        canMove = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //Destroy(gameObject);
            cont.GiveMoney(money);
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
