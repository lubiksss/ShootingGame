using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public int power;
    public int maxpower;
    public float maxShotDelay;
    public float curShotDelay;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;
    public bool isBoomTime;

    public int life;
    public int score;
    public bool isHit;
    public int boom;
    public int boommax;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject BoomEffect;
    public GameObject[] followers;
    public bool isRespawnTime;
    public SpriteRenderer sprite;
    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA;
    public bool isButtonB;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager.updateBoomIcon(boom);
    }

    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    void OnEnable()
    {
        Unbeatable();
        Invoke("Unbeatable", 2f);
    }

    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime;
        if (isRespawnTime)
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
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
            if (isRespawnTime)
                return;

            if (isHit)
                return;
            isHit = true;
            life--;
            gameManager.updateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");
            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power == maxpower)
                    {
                        score += 500;
                    }
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if (boom == boommax)
                    {
                        score += 1000;
                    }
                    else
                    {
                        boom++;
                        gameManager.updateBoomIcon(boom);
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
    void AddFollower()
    {
        if (power == 4)
        {
            followers[0].SetActive(true);
        }
        else if (power == 5)
        {
            followers[1].SetActive(true);
        }
        else if (power == 6)
        {
            followers[2].SetActive(true);
        }
    }
    void Boom()
    {
        // if (!Input.GetButton("Fire2")) { return; }
        if (!isButtonB)
        {
            return;
        }
        if (isBoomTime) { return; }
        if (boom == 0) { return; }
        boom--;
        gameManager.updateBoomIcon(boom);
        isBoomTime = true;
        // 1.Effect Visible
        BoomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);
        // 2. Remove Enemy
        GameObject[] enemiesL = objectManager.GetPool("enemyL");
        GameObject[] enemiesM = objectManager.GetPool("enemyM");
        GameObject[] enemiesS = objectManager.GetPool("enemyS");

        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf)
            {
                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }

        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {
                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }

        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {
                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }

        }

        // 3. Remove Enemy Bullet
        GameObject[] bulletsA = objectManager.GetPool("bulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("bulletEnemyB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            if (bulletsA[index].activeSelf)
            {
                bulletsA[index].SetActive(false);
            }
        }
        for (int index = 0; index < bulletsB.Length; index++)
        {
            if (bulletsB[index].activeSelf)
            {
                bulletsB[index].SetActive(false);
            }
        }
        isButtonB = false;
    }
    void OffBoomEffect()
    {
        isBoomTime = false;
        BoomEffect.SetActive(false);
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
    public void JoyPanel(int type)
    {
        for (int index = 0; index < 9; index++)
        {
            joyControl[index] = index == type;
        }
    }
    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }
    void Move()
    {
        // Player Move
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // Mobile
        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }

        // Border Control
        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft) || !isControl) { h = 0; }
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom) | !isControl) { v = 0; }

        // Player Move Animation
        animator.SetInteger("isLR", (int)h);

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }
    public void ButtonADown()
    {
        isButtonA = true;
    }
    public void ButtonAUp()
    {
        isButtonA = false;
    }
    public void ButtonBDown()
    {
        isButtonB = true;
    }
    void Fire()
    {
        // // 조건이 아니면 리턴
        // if (!Input.GetButton("Fire1")) { return; }
        if (!isButtonA)
        {
            return;
        }
        // 딜레이 줌.
        if (curShotDelay < maxShotDelay) { return; }
        // 조건 다 만족하면 쏘겠음.
        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("bulletPlayerA");
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletL = objectManager.MakeObj("bulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletR = objectManager.MakeObj("bulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            default:
                GameObject bulletLL = objectManager.MakeObj("bulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.25f;
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletCC = objectManager.MakeObj("bulletPlayerB");
                bulletCC.transform.position = transform.position;
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletRR = objectManager.MakeObj("bulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.25f;
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



