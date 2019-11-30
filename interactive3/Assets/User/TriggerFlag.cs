using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlag : MonoBehaviour {

    public bool inArea = false;
    public GameObject obj;		//GameObject型の変数objを用意します


    private void Start()
    {
        //obj = GameObject.Find("Sight");		　　//「Sight」という名前のオブジェクトを探して入れます
        obj.GetComponent<SpriteRenderer>().enabled = false;　　 //格納したオブジェクトの「SpriteRenderer」を止めます

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = true;
            obj.GetComponent<SpriteRenderer>().enabled = true;　//プレイヤーがいる時「SpriteRenderer」を復活させます

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = false;
            obj.GetComponent<SpriteRenderer>().enabled = false;　//プレイヤーが離れた時「SpriteRenderer」を止めます
        }
    }
}