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
    public Vector2 monsterRoomDistance;//distance to starting room counted by room
    public Pathfinding.AIDestinationSetter target;
    public float trapTime;

    private RoomDoor targetRoom;

    public int movingStrategy = 1;// 0=sleep, 1=scout, 2=chase

    public float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        roomGenerator = GameObject.Find("RoomGenerator");
        rooms = roomGenerator.GetComponent<RoomGenerator>().rooms;
        player = GameObject.Find("Player");
        target = GetComponent<Pathfinding.AIDestinationSetter>();
        targetRoom = rooms[Random.Range(0, roomGenerator.GetComponent<RoomGenerator>().roomNumber)];
        movingStrategy = 1;
        timer = Time.time*Time.deltaTime;
    }


    void FixedUpdate()
    {
        Move();
        ChangeMode();
        if (Time.time * Time.deltaTime - timer >= trapTime)
        {
            GetComponent<Pathfinding.AIPath>().canMove = true;
            Debug.Log("move");
        }
    }

    void ChangeMode()
    {
        float xx = Mathf.Abs(monsterRoomDistance.x - player.GetComponent<PlayerControler>().playerRoomDistance.x);//horizontal dis
        float yy = Mathf.Abs(monsterRoomDistance.y - player.GetComponent<PlayerControler>().playerRoomDistance.y);//vertical dis
        Debug.Log(xx);
        Debug.Log(yy);
        if (movingStrategy == 0)
        {

        }
        else if (movingStrategy == 1)
        {
            if(xx + yy <= 1)
            {
                movingStrategy = 2;
            }
        }
        else if (movingStrategy == 2)
        {
            if(xx + yy >= 2)
            {
                movingStrategy = 1;
            }
        }
    }

    void Move()
    {
        if(movingStrategy == 1)
        {
            target.target = targetRoom.transform;//target = a random room's position
            if((transform.position - targetRoom.transform.position).sqrMagnitude < 1)//the position usually is not accurate enough, so set the inaccuracy to 1
            {
                targetRoom = rooms[Random.Range(0, roomGenerator.GetComponent<RoomGenerator>().roomNumber)];
                
            }
        }
        else if(movingStrategy == 2)
        {
            target.target = player.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SetUpTrap"))
        {
            
            GetComponent<Pathfinding.AIPath>().canMove = false;
            timer = Time.time*Time.deltaTime;
            Destroy(other.gameObject);

        }

    }


}
