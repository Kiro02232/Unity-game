using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetecting : MonoBehaviour
{
    // Start is called before the first frame update
    Animation anim;
    public float animSpeed;
    private Vector3 playerPos, monsterPos;
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        playerPos = GameObject.Find("Player").transform.position;
        monsterPos = GameObject.Find("Monster").transform.position;
        animSpeed = (2000 - (playerPos - monsterPos).sqrMagnitude)/400;//玩家怪物距離
        Debug.Log((playerPos - monsterPos).sqrMagnitude);
        if (animSpeed < 1) animSpeed = 0;
        SetAnimationSpeed(anim, "RadarBlinking", animSpeed);
    }
    public void SetAnimationSpeed(Animation ani, string name, float speed)//ani = 動畫參數 name = 動畫名  speed = 速度
    {
        if (null == ani) return;
        AnimationState state = ani[name];
        state.speed = speed;

    }
}
