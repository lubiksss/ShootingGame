using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;
    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }


    void Update()
    {
        Move();
        Scrolling();
    }
    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }
    void Scrolling()
    {
        // 제일 아래 있는 배경사진이 화면 밖으로 나갈 경우 제일 위로 올림
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;

            int tmp = startIndex;
            startIndex = endIndex;
            endIndex = tmp - 1 == -1 ? sprites.Length - 1 : tmp - 1;
        }
    }
}
