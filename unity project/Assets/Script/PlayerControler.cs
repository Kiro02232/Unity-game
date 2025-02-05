using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Basic")]
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    public float moveSpeed;
    public Vector2 playerRoomDistance = new Vector2 (0, 0);//distance to starting room counted by room
    private Collider2D coll;

    [Header("BackpackSystem")]
    string currentItem = "none";//現在持有物品
    public GameObject Radar, SpeedUp, Trap;//陷阱為未設置陷阱
    public GameObject RadarUI;
    public GameObject SetUpTrap;//已設置陷阱



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        RadarUI = GameObject.Find("RadarDetecting");
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(currentItem != "none")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GenerateItem(currentItem);
                currentItem.Remove(0);
                currentItem.Insert(0, "none");
            }
        }

        if (currentItem == "Radar")
        {
            RadarUI.SetActive(true);
        }
        else
        {
            RadarUI.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
     
        if(other.gameObject.tag == "Radar")//可能會跟37行出bug
        {
            GenerateItem(currentItem);
            if (Input.GetButtonDown("Fire1"))
            {
                currentItem.Replace(currentItem, "Radar");
                other.gameObject.GetComponent<Collection>().Collect();
            }
        }

        if (other.gameObject.tag == "Trap")//可能會跟37行出bug
        {
            GenerateItem(currentItem);
            if (Input.GetButtonDown("Fire1"))
            {
                currentItem.Replace(currentItem, "Trap");
                other.gameObject.GetComponent<Collection>().Collect();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                //播放設置動畫

                Destroy(other.gameObject);
                GenerateItem("SetUpTrap");
            }
        }

    }

    private void GenerateItem(string ItemName)//生成物品
    {
        if(ItemName == "none")
        {
            return;
        }
        if(ItemName == "Radar")
        {
            Instantiate(Radar);
        }
        if (ItemName == "SpeedUp")
        {
            Instantiate(SpeedUp);
        }
        if(ItemName == "Trap")
        {
            Instantiate(Trap);
        }
        if(ItemName == "SetUpTrap")
        {
            Instantiate(SetUpTrap);
        }
    }
}
