using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;
    public float curShotDelay;

    public ObjectManager objectManager;
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }
    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    void Watch()
    {
        // 현재 위치가 큐에 안담겼다면 넣음
        if (!parentPos.Contains(parent.position))
        {
            parentPos.Enqueue(parent.position);
        }
        // followDelay를 넘어서는 순간부터 parent position 추적할수 있게
        if (parentPos.Count > followDelay)
        {
            followPos = parentPos.Dequeue();
        }
        // 시작할때 parent에 붙어서 생성되게 함
        else if (parentPos.Count < followDelay)
        {
            followPos = parent.position;
        }
    }

    void Follow()
    {
        transform.position = followPos;
    }
    void Fire()
    {
        // 조건이 아니면 리턴 => Follower는 무조건쏨

        // if (!Input.GetButton("Fire1")) { return; }
        // 딜레이 줌.
        if (curShotDelay < maxShotDelay) { return; }
        // 조건 다 만족하면 쏘겠음.

        GameObject bullet = objectManager.MakeObj("bulletFollower");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

}
