using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float bounce = 3.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")                                 //　もし当たった相手のTagが”Ball”だったら
        {
            Vector3 nom = other.contacts[0].normal;          	            // 一回目に跳ねる法線ベクトルを変数nomに入れます
            Vector3 vel = other.rigidbody.velocity.normalized; 	            //　ぶつかった相手の速度ベクトルを単位長さ1にして変数velに入れます　
            vel += new Vector3(-nom.x * 2, -nom.y * 2, -nom.z * 2);         //　速度ベクトルvelに法線ベクトルを2倍して逆方向（-）にしたものを足します

            other.rigidbody.AddForce(vel * bounce, ForceMode2D.Impulse);
            //　ぶつかった相手に、変数velの方向に瞬間の衝撃力を（＊bounce）したものを加えます　
        }
    }
}
