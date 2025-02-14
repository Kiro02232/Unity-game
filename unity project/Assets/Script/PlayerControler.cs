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
    public GameObject currentItem;//現在持有物品
    public GameObject Radar, SpeedUp, Trap;//陷阱為未設置陷阱
    public GameObject RadarUI;
    public GameObject SetUpTrap;//已設置陷阱
    public GameObject touchItem;
    private bool canBePick;



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
        if(currentItem != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(currentItem);
            }
        }

        if (currentItem.gameObject.tag == "Radar")
        {
            RadarUI.SetActive(true);
        }
        else
        {
            RadarUI.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && canBePick)
        {
            if (currentItem != null)
            {
                Instantiate(currentItem);
            }

            // 先複製物品，再刪除原始物品
            currentItem = Instantiate(touchItem);
            currentItem.SetActive(false); // 不讓它直接出現在場景中

            touchItem.gameObject.GetComponent<Collection>().Collect();
            Debug.Log(currentItem);

        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Radar") || other.CompareTag("SpeedUp") || other.CompareTag("Trap"))
        {
            canBePick = true;
            touchItem = other.gameObject;
        }

        if (other.gameObject.tag == "Trap")
        {
           //設置陷阱

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Radar")
        {
            canBePick = false;
        }

        if (other.gameObject.tag == "Trap")
        {
            canBePick = false;
        }

    }


}
