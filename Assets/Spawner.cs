using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> spawnPool;
    public int maxEnemies = 25;
    public float lastEnemyCreated = 0.0f;
    public float spawnRate = 1.0f;
    public int spawnIndex = 0;

    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;

    private Sprite[] targetSprites;

    // Start is called before the first frame update
    void Start()
    {
        targetSprites = new Sprite[] {redSprite, greenSprite, blueSprite};
        for(int i = 0; i < maxEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            newEnemy.SetActive(false);
            spawnPool.Add(newEnemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy() 
    {
        float timeToNextSpawn = System.Math.Max(spawnRate - GameStatus.score/1000.0f, 0.2f);
        if(Time.time > lastEnemyCreated + timeToNextSpawn)
        {
            lastEnemyCreated = Time.time;
            foreach (GameObject enemy in spawnPool)
            {  
                if(!enemy.activeSelf)
                {
                    Target enemyTarget = enemy.GetComponent<Target>();
                    enemyTarget.colorCode = Random.Range(0, 3);
                    SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();
                    enemySprite.sprite = targetSprites[enemyTarget.colorCode];
                    enemy.SetActive(true);
                    enemy.transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);
                    enemy.transform.rotation = Quaternion.identity;
                    Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
                    enemyBody.velocity = enemy.transform.up * Random.Range(3, 8) * -1;
                    break;
                }
            }
        }
    }
}
