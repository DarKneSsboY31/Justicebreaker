using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaimController : MonoBehaviour {

    //SEの設定
    private AudioSource audiosource;
    public AudioClip hitoDeath; //hito用
    public AudioClip iwaDeath; //iwa用
    public AudioClip himeDeath; //hime用
    public AudioClip Exprosion;

    //EnemyCreaterのstopTrigger用コンポーネント
    private int StopTrigger;

   //Rigidbodyコンポーネントを入れる
    private Rigidbody2D myRigidbody;

    //スコアの設定
    public static int score = 0;

    //進む力
    private float Force = 10.0f;

    //敵を倒した数
    public static float Break = 0;

    // Use this for initialization
    void Start() {

        //Rigidbodyコンポーネントの取得
        myRigidbody = GetComponent<Rigidbody2D>();

        //AudioSourceのコンポーネント取得
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {

    }
    private void FixedUpdate()
    {
        //EnemyCreaterのstoTriggerと同期する
        StopTrigger = EnemyCreater.stopTrigger;

        //時が止まっている間は、動かない
        if (StopTrigger == 0)
        {
            return;
        }

        //上下左右の矢印キーを押すと上下左右に移動
        //右又は左を押した時の数値
        float x = Input.GetAxisRaw("Horizontal");

        //上又は下を押した時の数値
        float y = Input.GetAxisRaw("Vertical");

        //移動する向きを求める
        Vector2 direction = new Vector2(x, y).normalized;

        //移動する向きとスピードを代入
        myRigidbody.AddForce(direction * ((Force * (Break + 1.0f))) - myRigidbody.velocity);

    }

    //敵に当たったら、速くなる処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //壁含むすべてに当たると爆発音が鳴る
        audiosource.PlayOneShot(Exprosion, 2.0f);

        if (collision.gameObject.tag == "hito")
        {

            //Scoreを加算
            score += 30;

            //敵を倒した数だけ速くなる処理
            Break += 0.3f;

            //音を出す
            audiosource.PlayOneShot(hitoDeath, 2.0f);
 

        } else if (collision.gameObject.tag == "iwa")
        {
            //Scoreを加算
            score += 50;

            //敵を倒した数だけ速くなる処理
            Break += 0.5f;

            //音を出す
            audiosource.PlayOneShot(iwaDeath, 2.0f);
 
        } else if (collision.gameObject.tag == "hime")
        {

            //Scoreを加算
            score += 100;

            //敵を倒した数だけ速くなる処理
            Break += 0.8f;

            //音を出す
            audiosource.PlayOneShot(himeDeath, 2.0f);
      
        }
    }
   
}
