using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public PlayerControler player;
    private bool isCollected = false;
    private Collider2D collectionColl;
    void Start()
    {
        collectionColl = GetComponent<Collider2D>();
    }
    public void Collect()
    {
        Destroy(gameObject);
    }
}
