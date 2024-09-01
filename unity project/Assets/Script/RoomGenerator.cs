using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up, down, right, left};
    public Direction direction;

    [Header("房間訊息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor, preEndColor;
    private RoomDoor endRoom, preEndRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public float detectRad;

    public List<RoomDoor> rooms = new List<RoomDoor>();

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
        foreach (var room in rooms)
        {
            Debug.Log(preEndRoom.stepToStart);
            if (room.stepToStart > preEndRoom.stepToStart)
            {
                preEndRoom = room;
            }
        }
        preEndRoom.GetComponent<SpriteRenderer>().color = preEndColor;
        AddEndRoom();
        rooms.Add(endRoom);
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
        foreach (var room in rooms) SetUpRoom(room, room.transform.position);
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
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.UpdateRoom();
    }

    public void AddEndRoom()
    {
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
    }
}
