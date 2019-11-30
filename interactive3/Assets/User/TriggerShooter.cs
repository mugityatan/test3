using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShooter : MonoBehaviour {

    [Header("Object creation")]
    public GameObject prefabToSpawn; 　 //出現させる弾

    [Header("Other options")]
    public float spawnInterval = 3.0f;　　 // 出現させる間隔
    public float bulletInterval = 0.1f;    // 弾の連射間隔
    public int bulletAmount = 3;           // 弾の連射個数

    Vector2 bulletDirection;            //弾の飛ぶ方向
    public float shootSpeed = 3.0f;     //弾のスピード
    public GameObject player;           //プレイヤー
    public bool spawnFlag;              //弾が出ているか？のフラッグ
    private bool enterTrigger;         //撃つのを止めるフラグ


    void Start()
    {
        spawnFlag = false;
        enterTrigger = false;
        StartCoroutine(SpawnObject());  //コルーチンの開始
    }

    IEnumerator SpawnObject()           //コルーチンの内容
    {
        while (true)
        {
            spawnFlag = false;
            GameObject parent = this.transform.parent.gameObject;
            //enterTrigger = parent.GetComponent<TriggerFlag>().isTrigger;
            if (enterTrigger == true)            //もしも入れられた値がfalseの場合、以下の処理（攻撃）に進みます
            {
                if (player != null)
                {
                    Vector2 shooterPosition = this.GetComponent<Transform>().position;  //発射する位置
                    Vector2 playerPos = player.GetComponent<Transform>().position;     //プレイヤーの位置

                    if (playerPos.x < shooterPosition.x)  //プレイヤーより後ろ側なら撃たない条件分岐
                    {
                        spawnFlag = true;
                        for (int index = 0; index < bulletAmount; index++)  //　bulletAmountの回数だけ繰り返します
                        {
                            GameObject newObject = Instantiate<GameObject>(prefabToSpawn);
                            newObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                            bulletDirection = new Vector2(-1.0f, 0.0f);      //x軸はマイナス方向、y軸方向の変化はない

                            Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
                            rigidbody2D.AddForce(bulletDirection * shootSpeed, ForceMode2D.Impulse);

                            yield return new WaitForSeconds(bulletInterval);  //　bulletintervalの時間だけ待ってforに戻ります
                        }
                        spawnFlag = false;
                    }
                }
                yield return new WaitForSeconds(spawnInterval);
            }

            else
            {
                yield return new WaitForSeconds(0.1f);
            }

        }
    }
}

