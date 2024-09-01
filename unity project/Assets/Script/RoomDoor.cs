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
    public int doorNum;
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
        stepToStart = (int)(Mathf.Abs(transform.position.x / 25) + Mathf.Abs(transform.position.y / 12));
        text.text = stepToStart.ToString();
        if (roomUp) doorNum++;
        if (roomDown) doorNum++;
        if (roomLeft) doorNum++;
        if (roomRight) doorNum++;
    }
}
