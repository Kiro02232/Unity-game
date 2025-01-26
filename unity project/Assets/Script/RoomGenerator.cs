using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up, down, right, left};
    public Direction direction;
    [Header("房間內部")]
    public GameObject monsterRoom;

    [Header("房間訊息")]
    public GameObject roomPrefab;
    public int roomNumber;//how many room
    public Color startColor, endColor, preEndColor;//for debug
    private RoomDoor endRoom, preEndRoom;//最終房間和最終房間的前一個房間

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;//horizontal distance between two rooms 
    public float yOffset;//vertical distance between two rooms 
    public LayerMask roomLayer;
    public float detectRad;

    public List<RoomDoor> rooms = new List<RoomDoor>();

    public WallType wallType;

    void Start()
    {
        for(int i = 0; i < roomNumber; i++)
        {
            //生成房間
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<RoomDoor>());
            //改變生成點位置
            if(i!=roomNumber-1)ChangePointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        preEndRoom = rooms[0];
        foreach (var room in rooms) SetUpRoom(room, room.transform.position);

        //preEndRoom is furthest room from starting room
        foreach (var room in rooms)
        {
            Debug.Log(preEndRoom.stepToStart);
            if (room.stepToStart > preEndRoom.stepToStart)
            {
                preEndRoom = room;
            }
        }

        preEndRoom.GetComponent<SpriteRenderer>().color = preEndColor;//debug
        AddEndRoom();//end room must be attached to preEndRoom
        rooms.Add(endRoom);//add end room
        endRoom.GetComponent<SpriteRenderer>().color = endColor;//debug
        foreach (var room in rooms) SetUpRoom(room, room.transform.position);//set up room again to renew end room
        foreach (var room in rooms)
        {
            //Debug.Log(room.doorNum);
            AddDoor(room, room.transform.position);
        }

        GetComponent<AstarPath>().Scan();
        GameObject.Find("Monster").GetComponent<MonsterAI>().monsterRoomDistance.x = endRoom.roomPosX;
        GameObject.Find("Monster").GetComponent<MonsterAI>().monsterRoomDistance.y = endRoom.roomPosY;
    }


    void Update()
    {
        
    }
    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, detectRad, roomLayer));
        
    }
    public void SetUpRoom(RoomDoor newRoom, Vector3 roomPosition)
    {
        //detect neighbor room
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xOffset, yOffset);
    }

    public void AddDoor(RoomDoor Room, Vector3 roomPos)//generate wall
    {
        switch (Room.doorNum)
        {
            case 1:
                if (Room.roomDown) Instantiate(wallType.singleDown, roomPos, Quaternion.identity);
                if (Room.roomUp) Instantiate(wallType.singleUp, roomPos, Quaternion.identity);
                if (Room.roomLeft) Instantiate(wallType.singleLeft, roomPos, Quaternion.identity);
                if (Room.roomRight) Instantiate(wallType.singleRight, roomPos, Quaternion.identity);
                break;
            case 2:
                if (Room.roomDown&&Room.roomUp) Instantiate(wallType.doubleDU, roomPos, Quaternion.identity);
                if (Room.roomDown && Room.roomLeft) Instantiate(wallType.doubleDL, roomPos, Quaternion.identity);
                if (Room.roomDown && Room.roomRight) Instantiate(wallType.doubleDR, roomPos, Quaternion.identity);
                if (Room.roomLeft && Room.roomUp) Instantiate(wallType.doubleLU, roomPos, Quaternion.identity);
                if (Room.roomLeft && Room.roomRight) Instantiate(wallType.doubleLR, roomPos, Quaternion.identity);
                if (Room.roomRight && Room.roomUp) Instantiate(wallType.doubleRU, roomPos, Quaternion.identity);
                break;
            case 3:
                if (!Room.roomDown) Instantiate(wallType.tripleLRU, roomPos, Quaternion.identity);
                if (!Room.roomUp) Instantiate(wallType.tripleDLR, roomPos, Quaternion.identity);
                if (!Room.roomLeft) Instantiate(wallType.tripleDRU, roomPos, Quaternion.identity);
                if (!Room.roomRight) Instantiate(wallType.tripleDLU, roomPos, Quaternion.identity);
                break;
            case 4:
                Instantiate(wallType.fourDoors, roomPos, Quaternion.identity);
                break;

        }
                
    }

    public void AddEndRoom()
    {
        generatorPoint.position = preEndRoom.transform.position;

        if (!preEndRoom.roomUp)
        {
            generatorPoint.position += new Vector3(0, yOffset, 0);
            endRoom = Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<RoomDoor>();
            SetUpRoom(endRoom, endRoom.transform.position);
        }
        else if (!preEndRoom.roomDown)
        {
            generatorPoint.position += new Vector3(0, -yOffset, 0);
            endRoom = Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<RoomDoor>();
            SetUpRoom(endRoom, endRoom.transform.position);
        }
        else if (!preEndRoom.roomLeft)
        {
            generatorPoint.position += new Vector3(-xOffset, 0, 0);
            endRoom = Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<RoomDoor>();
            SetUpRoom(endRoom, endRoom.transform.position);
        }
        else if (!preEndRoom.roomRight)
        {
            generatorPoint.position += new Vector3(xOffset, 0, 0);
            endRoom = Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<RoomDoor>();
            SetUpRoom(endRoom, endRoom.transform.position);
        }

        Instantiate(monsterRoom, generatorPoint.position, Quaternion.identity);//add monsterRoom's room set
        
    }
}

[System.Serializable]
public class WallType
{
    public GameObject singleDown, singleLeft, singleRight, singleUp,
        doubleDL, doubleDR, doubleDU, doubleLR, doubleLU, doubleRU,
        tripleDLR, tripleDLU, tripleDRU, tripleLRU, fourDoors;

}
