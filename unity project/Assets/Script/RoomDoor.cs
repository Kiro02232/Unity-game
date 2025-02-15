using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDoor : MonoBehaviour
{

    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart;//distance to starting room counted by room
    public Text text;
    public int doorNum=0;
    public int roomPosX, roomPosY;//room position counted by room, starting room = (0, 0)
    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }


    public void UpdateRoom(float xOffset, float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));
        roomPosX = (int)(transform.position.x / xOffset);
        roomPosY = (int)(transform.position.y / yOffset);
        text.text = stepToStart.ToString();
        doorNum = 0;
        if (roomUp) doorNum++;
        if (roomDown) doorNum++;
        if (roomLeft) doorNum++;
        if (roomRight) doorNum++;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//update player's room
        {
            CameraController.instance.ChangeTarget(transform);
            other.GetComponent<PlayerControler>().playerRoomDistance = new Vector2(roomPosX, roomPosY);
        
        }
        if (other.CompareTag("Enemy")){//update monster's room
            other.GetComponent<MonsterAI>().monsterRoomDistance = new Vector2(roomPosX, roomPosY);

        }
    }
}
