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
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public float detectRad;

    public List<GameObject> rooms = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < roomNumber; i++)
        {
            //生成房間
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));
            //改變生成點位置
            ChangePointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        endRoom = rooms[0];
        foreach(var room in rooms)
        {
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room;
            }
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
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
}
