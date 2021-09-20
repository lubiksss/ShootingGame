using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Transform playerPos;
    public Animator fadeAnim;
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;
    public ObjectManager objectManager;
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public Text overText;
    public Text deltaTime;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "enemyL", "enemyM", "enemyS", "enemyBoss" };
        StageStart();
    }
    void Update()
    {
        deltaTime.text = string.Format("{0,10:N4}", Time.deltaTime);
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        // UI Score Update
        PlayerAction playerLogic = player.GetComponent<PlayerAction>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }


    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 2;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 0;
                break;
            case "Boss":
                enemyIndex = 3;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;
        if (enemyPoint == 5 || enemyPoint == 6)
        {
            rigid.velocity = new Vector2(-1 * enemyLogic.speed, -1);
            enemy.transform.Rotate(Vector3.back * 90);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
            enemy.transform.Rotate(Vector3.forward * 90);
        }
        else { rigid.velocity = new Vector2(0, -enemyLogic.speed); }

        // 리스폰 인덱스 증가
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        // 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;

    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4;
        player.SetActive(true);
        PlayerAction playerLogic = player.GetComponent<PlayerAction>();
        playerLogic.isHit = false;
    }
    public void updateLifeIcon(int life)
    {
        // UI Life Init Disable
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        // UI Life Active
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void updateBoomIcon(int boom)
    {
        // UI Boom Init Disable
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }
        // UI Boom Active
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void StageStart()
    {
        // Stage UI Load
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "STAGE" + stage + "\nStart";
        clearAnim.GetComponent<Text>().text = "STAGE" + stage + "\nClear!";

        // Enemy Spawn File Read
        ReadSpawnFile();

        // Fade In
        fadeAnim.SetTrigger("In");
    }

    public void StageEnd()
    {
        // Clear UI Load
        clearAnim.SetTrigger("On");

        // Fade Out
        fadeAnim.SetTrigger("Out");

        // Player Repos
        player.transform.position = playerPos.position;

        // Stage Increament
        stage++;
        if (stage > 1)
        {
            overText.text = "All Clear!";
            Invoke("GameOver", 6);
        }
        else
            Invoke("StageStart", 5);

    }

    void ReadSpawnFile()
    {
        // 1. 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 2. 리스폰 파일 일기
        TextAsset textFile = Resources.Load("Stage" + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        // 3. 한줄씩 데이터 저장
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null) { break; }

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // 텍스트 파일 닫기
        stringReader.Close();

        // 첫번째 스폰 딜레이 적용
        nextSpawnDelay = spawnList[0].delay;
    }
}
