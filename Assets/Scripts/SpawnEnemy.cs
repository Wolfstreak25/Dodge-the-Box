using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class SpawnEnemy : Singleton<SpawnEnemy>
{
    //variable region
    [SerializeField]
    private float minX = 0.0f;
    [SerializeField]
    private float maxX = 0.0f;
    [SerializeField]
    private int minEnemy = 1;
    [SerializeField]
    private int maxEnemy = 6;
    [SerializeField]
    private int enemyLimit = 10;
    [SerializeField]
    private EnemyCollision enemyPrefab; // potential array of Enemy
    [SerializeField]
    private float timeBetweenSpawn = 0.0f;
    private bool canSpawn = false;
    private int amountOfEnemyToSpawn = 0;
    private int enemyToSpawn = 0;
    //private int enemySpawnCapacity = 8;
    private UIScript uiFunction;
    public ObjectPool<EnemyCollision> pool;
    private Vector3 spawnPosition;

    //variable region end

    void Start()
    {
        pool = new ObjectPool<EnemyCollision>
                (
                    () => {return Instantiate(enemyPrefab,spawnPosition,Quaternion.identity);},
                    enemy => {
                                enemy.gameObject.SetActive(true);
                                enemy.gameObject.transform.position = spawnPosition;
                            },
                    enemy => {enemy.gameObject.SetActive(false);},
                    enemy => {Destroy(enemy.gameObject);},
                    false,maxEnemy,enemyLimit
                );
        uiFunction = gameObject.GetComponent<UIScript>();
        canSpawn = true;
    }
	void Update()
    {
        if (canSpawn == true && uiFunction.gameStarted == true)
        {
            StartCoroutine("GenerateSpawn");
        }
	}

    private IEnumerator GenerateSpawn()
    {
        canSpawn = false;
        timeBetweenSpawn = Random.Range(0.5f, 2.0f);
        amountOfEnemyToSpawn = Random.Range(minEnemy, maxEnemy);
        for(int i = 0; i < amountOfEnemyToSpawn; i++)
        {
            spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(8.0f, 15.0f), 0.0f);
            pool.Get();
        }
        yield return new WaitForSeconds(timeBetweenSpawn);
        canSpawn = true;
    }
    public void ReturnEnemy(EnemyCollision enemy)
    {
        pool.Release(enemy);
    }
}
