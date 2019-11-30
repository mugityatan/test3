using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShooter1_Y : MonoBehaviour {

    [Header("Object creation")]
    public GameObject prefabToSpawn; 　 //出現させる弾

    [Header("Other options")]
    public float spawnInterval = 3.0f;   // 出現させる間隔

    Vector2 bulletDirection;            //弾の飛ぶ方向
    public float shootSpeed = 3.0f;     //弾のスピード
    public GameObject player;           //プレイヤー

    void Start()
    {
        StartCoroutine(SpawnObject());  //コルーチンの開始
    }


    IEnumerator SpawnObject()           //コルーチンの内容
    {
        while (true)
        {
            if (player != null)
            {
                Vector2 shooterPosition = this.GetComponent<Transform>().position;　//発射する位置
                Vector2 playerPos = player.GetComponent<Transform>().position;   //プレイヤーの位置

                if (playerPos.x < shooterPosition.x)　//プレイヤーより後ろ側なら撃たない条件分岐
                {
                    GameObject newObject = Instantiate<GameObject>(prefabToSpawn);
                    newObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                    bulletDirection = new Vector2(-1.0f, 0.0f);     //x軸はマイナス方向、y軸方向の変化はない
                    Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
                    rigidbody2D.AddForce(bulletDirection * shootSpeed, ForceMode2D.Impulse);
                }
            }
            yield return new WaitForSeconds(spawnInterval);　//spainintervalの時間だけ待ってwhileに戻ります
        }
    }
}