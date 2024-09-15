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


    public void UpdateRoom()
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 10));
        text.text = stepToStart.ToString();
        doorNum = 0;
        if (roomUp) doorNum++;
        if (roomDown) doorNum++;
        if (roomLeft) doorNum++;
        if (roomRight) doorNum++;
    }
}
