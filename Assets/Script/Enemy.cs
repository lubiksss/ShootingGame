using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public string enemyName;
    public int enemyScore;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public float maxShotDelay;
    public float curShotDelay;
    public GameObject player;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;
    public ObjectManager objectManager;
    Animator anim;
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    public GameManager gameManager;



    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enemyName == "Boss")
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                health = 40;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 3;
                break;
            case "Boss":
                health = 1000;
                Invoke("Stop", 1.7f);
                break;

        }
    }

    void Update()
    {
        if (enemyName == "Boss")
        {
            return;
        }
        Fire();
        Reload();
    }

    public void OnHit(int dmg)
    {
        if (health < 0) { return; }
        health -= dmg;
        if (enemyName == "Boss")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("returnSprite", 0.1f);
        }
        if (health <= 0)
        {
            PlayerAction playerLogic = player.GetComponent<PlayerAction>();
            playerLogic.score += enemyScore;
            // 아이템 드랍
            int ran = enemyName == "Boss" ? 0 : Random.Range(0, 10);
            // 0,1,2 30% 없음
            if (ran < 2) { Debug.Log("No Item"); }
            // 3,4,5 30% 코인
            else if (ran < 6)
            {
                GameObject itemCoin = objectManager.MakeObj("itemCoin");
                itemCoin.transform.position = transform.position;
            }
            // 6,7 20% 파워업
            else if (ran < 8)
            {
                GameObject itemPower = objectManager.MakeObj("itemPower");
                itemPower.transform.position = transform.position;
            }
            // 8,9 폭탄
            else if (ran < 10)
            {
                GameObject itemBoom = objectManager.MakeObj("itemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);

            // Boss Kill
            if (enemyName == "Boss")
                gameManager.StageEnd();
        }

    }

    void returnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "Boss")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }

    }
    void Fire()
    {
        if (curShotDelay < maxShotDelay) { return; }
        if (enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
            curShotDelay = 0;
        }
        else if (enemyName == "L")
        {
            GameObject bulletL = objectManager.MakeObj("bulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.15f;
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecL = player.transform.position - transform.position;
            rigidL.AddForce(dirVecL.normalized * 5, ForceMode2D.Impulse);

            GameObject bulletR = objectManager.MakeObj("bulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.15f;
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - transform.position;
            rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
            curShotDelay = 0;
        }

    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Debug.Log("stop");
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }
    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }
    void FireForward()
    {
        if (health <= 0) { return; }
        // 속도가 조금 빠른 탄환 전방으로 4발발사
        GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        Vector3 dirVecLL = player.transform.position - transform.position;
        rigidLL.AddForce(dirVecLL.normalized * 8, ForceMode2D.Impulse);

        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.right * 0.3f;
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Vector3 dirVecL = player.transform.position - transform.position;
        rigidL.AddForce(dirVecL.normalized * 8, ForceMode2D.Impulse);

        GameObject butlletR = objectManager.MakeObj("bulletBossA");
        butlletR.transform.position = transform.position + Vector3.left * 0.3f;
        Rigidbody2D rigidR = butlletR.GetComponent<Rigidbody2D>();
        Vector3 dirVecR = player.transform.position - transform.position;
        rigidR.AddForce(dirVecR.normalized * 8, ForceMode2D.Impulse);

        GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Vector3 dirVecRR = player.transform.position - transform.position;
        rigidRR.AddForce(dirVecRR.normalized * 8, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 2);
        }
        else
        {
            Invoke("Think", 2);
        }
    }
    void FireShot()
    {
        if (health <= 0) { return; }
        // 플레이어 방향으로 탄환 5발 샷건 발사
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 2);
        }
        else
        {
            Invoke("Think", 2f);
        }
    }
    void FireArc()
    {
        if (health <= 0) { return; }
        // 플레이어 방향으로 탄환 아크모양으로 발사
        GameObject bullet = objectManager.MakeObj("bulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        // 삼각함수 사용하여 아크 모양으로 발사하게 함
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.1f);
        }
        else
        {
            Invoke("Think", 2f);
        }
    }
    void FireAround()
    {
        if (health <= 0) { return; }
        // 보스 전방위로 발사
        int roundNumA = 50;
        int roundNumB = 40;
        // 두개로 한 이유는 한개로만 하면 플레이어가 가많이 있어도 총알 안 맞음
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            // 삼각함수 사용하여 원 모양으로 발사하게 함
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90; ;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 2f);
        }
    }

}
