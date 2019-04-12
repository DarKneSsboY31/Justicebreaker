using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //Slaimのゲームオブジェクトを取得
    public GameObject Slaim;

    //Rigidbodyコンポーネントを入れる
    private Rigidbody2D myRigidbody;

    //EnemyCreaterのstopTrigger用コンポーネント
    private int StopTrigger;

    //keyの同期用
    private int key;

    // Use this for initialization
    void Start () {

        //Rigidbodyコンポーネントの取得
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //Slaimを探す
        Slaim = GameObject.Find("Slime");

    }
	
	// Update is called once per frame
	void Update () {

        //EnemyCreaterのstoTriggerと同期する
        StopTrigger = EnemyCreater.stopTrigger;

        //EnemyCreaterのkeyと同期する
        key = EnemyCreater.key;



        //スタートの文字が消えるまでは動かない
        if (StopTrigger == 0)
        {
            if ( key == 1)
            {
                //消える
                this.gameObject.SetActive(false);
            }

                return;
                       
        } 


        //hitoの動き（Slaimに近づく動き???）
        if (this.gameObject.tag == "hito")
        {
           
            //このオブジェクトの位置を確認
            var pos = this.transform.position / Random.Range(-2.0f, 2.0f);

            //Slaimの位置と照らし合わせて、Slaimに向かうベクトルを計算
             Vector2 vec = Slaim.transform.position - pos;

            //Rigidbody2Dコンポーネントのvelocityにvecを代入、力を加える
            myRigidbody.velocity = vec / 30;


        }

        //himeの動き（Slaimから逃げる動き）
        if (this.gameObject.tag == "hime")
        {

            //このオブジェクトの位置を確認
            var pos = this.transform.position;

            //Slaimの位置と照らし合わせて、Slaimに向かうベクトルを計算
            Vector2 vec = -(Slaim.transform.position - pos);

            //Rigidbody2Dコンポーネントのvelocityにvecを代入、力を加える
            myRigidbody.velocity = vec / 50;

        }

        //iwaは動きません。でも慣性で動く

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //スライムに当たった時、消える
        if (collision.gameObject.tag == "Slaim") {

            //消えるためのコルーチンを作動
            StartCoroutine(Kieru());
        }
    }

    //消えるコルーチンの中身
    IEnumerator Kieru(){

        //0.01秒待つ
        yield return new WaitForSeconds(0.01f);

        //スライムを1回しか加速させないために、タグを変える処理
        this.gameObject.tag = "Untagged";

        //0.05秒待つ
        yield return new WaitForSeconds(0.05f);

        //消える
        this.gameObject.SetActive(false);

    }
}
