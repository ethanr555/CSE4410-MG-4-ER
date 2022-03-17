using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public BaseTower tower;
    public BaseTower[] towers;
    //public float cost;
    // Start is called before the first frame update

    public Transform[] waypoints;

    public float maxHealth;

    public Image healthImage;

    public float currentTowerCost;
    [HideInInspector]
    public float money;

    public float timeBetweenSpawnLow;
    public float timeBetweenSpawnHigh;

    float cools;
    public GameObject spawnPosition;
    public GameObject[] enemies;

    public Text moneyText;

    public bool gameOver;
    public GameObject gameOverUI;


    [SerializeField]
    float health;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "MONEY: " + money.ToString();

        healthImage.fillAmount = health / maxHealth;
        if (!gameOver)
        {
            if (cools > 0)
            {
                cools -= Time.deltaTime;
            }
            else
            {
                SpawnEnemy();
            }
        }
        if (gameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }

    }

    private void Awake()
    {
        health = maxHealth;
        healthImage.fillAmount = health / maxHealth;
        UpdateTower(0);
        gameOver = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameOver = true;
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GiveMoney(float amt)
    {
        money += amt;
    }

    public void UpdateTower(int i)
    {
        tower = towers[i];
        currentTowerCost = towers[i].cost;
    }

    void SpawnEnemy()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition.transform.position, Quaternion.Euler(0, 0, -90));
        cools = Random.Range(timeBetweenSpawnLow, timeBetweenSpawnHigh);
    }
}
