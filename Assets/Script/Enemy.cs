using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        }
    }

    void Update()
    {
        Fire();
        Reload();
    }

    public void OnHit(int dmg)
    {
        if (health < 0) { return; }
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("returnSprite", 0.1f);
        if (health <= 0)
        {
            PlayerAction playerLogic = player.GetComponent<PlayerAction>();
            playerLogic.score += enemyScore;
            // Random Ratio Item Drop
            int ran = Random.Range(0, 10);
            if (ran < 2) { Debug.Log("No Item"); }
            else if (ran < 6)
            {
                GameObject itemCoin = objectManager.MakeObj("itemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if (ran < 8)
            {
                GameObject itemPower = objectManager.MakeObj("itemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 10)
            {
                GameObject itemBoom = objectManager.MakeObj("itemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    void returnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
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
}
