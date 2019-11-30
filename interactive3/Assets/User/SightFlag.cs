using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightFlag : MonoBehaviour {

    private bool sight;	　　　　　　//bool型の変数　sightを用意します
    private Animator anime;       //Animatorを格納するAnimator型の変数animeを用意します

    void Start()
    {
        anime = GetComponent<Animator>();　　 //変数animeにAnimatorコンポネントを格納します
        anime.SetBool("inSight", false);      //anime内にあるSetBoolメソッドの「insight」をfalseにします
    }

    void FixedUpdate()
    {
        transform.localPosition = new Vector2(0f, 0f);
        //　ローカルポジションを（親の位置からみて）(0f,0f)　にします（他のオブジェクトとの接触でズレてしまうため）
        GameObject parent = this.transform.parent.gameObject;
        //GameObject型の変数parentにこの「親」のオブジェクトを格納します
        sight = parent.GetComponent<TriggerFlag>().inArea;
        //変数sightに「TriggerFlag」クラスのinAreaフラグをいれます

        if (sight == true)
        {
            anime.SetBool("inSight", true);  //変数isightがtrueの時、アニメのフラグinSightをtrueにします
        }

        if (sight == false)
        {
            anime.SetBool("inSight", false);  //変数isightがfalseの時、アニメのフラグinSightをtrueにします
        }
    }
}