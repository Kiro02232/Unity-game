using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public GameObject player;
    public GameObject roomGenerator;
    public List<RoomDoor> rooms;
    public int monsterRoomDistance;
    public float monsterRoomX;
    public float monsterRoomY;

    public int movingStrategy = 0;// 0=sleep, 1=scout, 2=chase

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        roomGenerator = GameObject.Find("RoomGenerator");
        rooms = roomGenerator.GetComponent<RoomGenerator>().rooms;
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        Move();
    }

    void ChangeMode()
    {
        if(movingStrategy == 0)
        {

        }
        else if (movingStrategy == 1)
        {
            if(Mathf.Abs(monsterRoomDistance - player.GetComponent<PlayerControler>().playerRoomDistance) <= 1)
            {
                movingStrategy = 2;
            }
        }
        else if (movingStrategy == 2)
        {
            if(Mathf.Abs(monsterRoomDistance - player.GetComponent<PlayerControler>().playerRoomDistance) >=2)
            {
                movingStrategy = 1;
            }
        }
    }

    void Move()
    {
        if(movingStrategy == 1)
        {

        }
        else if(movingStrategy == 2)
        {

        }
    }
}
