using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetecting : MonoBehaviour
{
    // Start is called before the first frame update
    Animation anim;
    public float animSpeed;
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //animSpeed = ;//玩家怪物距離
        if (animSpeed > 8) animSpeed = 8;
        SetAnimationSpeed(anim, "RadarBlinking", animSpeed);
    }
    public void SetAnimationSpeed(Animation ani, string name, float speed)//ani = 動畫參數 name = 動畫名  speed = 速度
    {
        if (null == ani) return;
        AnimationState state = ani[name];
        state.speed = speed;

    }
}
