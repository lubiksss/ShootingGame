using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float power;
    public float maxShotDelay;
    public float curShotDelay;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameManager manager;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Border Trigger
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top": isTouchTop = true; break;
                case "Bottom": isTouchBottom = true; break;
                case "Left": isTouchLeft = true; break;
                case "Right": isTouchRight = true; break;
            }

        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            gameObject.SetActive(false);
            manager.RespawnPlayer();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        // Border Trigger
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top": isTouchTop = false; break;
                case "Bottom": isTouchBottom = false; break;
                case "Left": isTouchLeft = false; break;
                case "Right": isTouchRight = false; break;
            }

        }
    }
    void Move()
    {
        // Player Move
        float h = Input.GetAxisRaw("Horizontal");
        // Player Move Animation
        animator.SetInteger("isLR", (int)h);
        // Border Control
        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft)) { h = 0; }
        float v = Input.GetAxisRaw("Vertical");
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom)) { v = 0; }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }
    void Fire()
    {
        // 조건이 아니면 리턴
        if (!Input.GetButton("Jump")) { return; }
        // 딜레이 줌.
        if (curShotDelay < maxShotDelay) { return; }
        // 조건 다 만족하면 쏘겠음.
        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }
        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}



