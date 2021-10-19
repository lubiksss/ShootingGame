using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 모든 생성가능한 프리팹을 변수로 선언하여 에디터에서 담음
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject enemyBossPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletFollowerPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
    public GameObject explosionPrefab;
    // 미리 객체로 만들리스트
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;
    GameObject[] enemyBoss;
    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;
    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletFollower;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] targetPool;
    GameObject[] explosion;

    private void Awake()
    {
        // 시작과 동시에 필요한 객체 크기만큼 선언
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];
        enemyBoss = new GameObject[1];
        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];
        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];
        explosion = new GameObject[20];

        Generate();
    }

    // 이제 리스트에 객체를 담아줌
    void Generate()
    {
        for (int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }
        for (int index = 0; index < enemyBoss.Length; index++)
        {
            enemyBoss[index] = Instantiate(enemyBossPrefab);
            enemyBoss[index].SetActive(false);
        }

        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }
        for (int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "enemyL":
                targetPool = enemyL;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyS":
                targetPool = enemyS;
                break;
            case "enemyBoss":
                targetPool = enemyBoss;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;
            case "explosion":
                targetPool = explosion;
                break;
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    // 플레이어가 폭탄 사용시 전부 없애버릴거기 때문에 풀자체를 리턴함
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "enemyL":
                targetPool = enemyL;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyS":
                targetPool = enemyS;
                break;
            case "enemyBoss":
                targetPool = enemyS;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;
            case "explosion":
                targetPool = explosion;
                break;
        }
        return targetPool;
    }
}
