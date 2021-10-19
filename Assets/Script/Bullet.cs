using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public bool isRotate;

    void Update()
    {
        // 총알을 돌아가게 할 수 있음
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알이 총알 경계선에 닿으면 파괴함
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
