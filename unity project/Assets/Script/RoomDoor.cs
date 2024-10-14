using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDoor : MonoBehaviour
{

    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart;
    public Text text;
    public int doorNum=0;
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
        text.text = stepToStart.ToString();
        doorNum = 0;
        if (roomUp) doorNum++;
        if (roomDown) doorNum++;
        if (roomLeft) doorNum++;
        if (roomRight) doorNum++;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
            other.GetComponent<PlayerControler>().playerRoomDistance = stepToStart;
            other.GetComponent<PlayerControler>().playerRoomX = transform.position.x;
            other.GetComponent<PlayerControler>().playerRoomY = transform.position.y;
        }
        if (other.CompareTag("Enemy")){
            other.GetComponent<MonsterAI>().monsterRoomDistance = stepToStart;

            other.GetComponent<MonsterAI>().monsterRoomX = transform.position.x;
            other.GetComponent<MonsterAI>().monsterRoomY = transform.position.y;
        }
    }
}
