using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCreater : MonoBehaviour {

    //ゲームスタートのトリガー
    private bool Trigger = false;

    //UIのオブジェクトを取得
    public GameObject Image; //ブルースクリーン
    public GameObject StartText;
    public GameObject ScoreText;
    public GameObject EndText;
    public GameObject TimeText;
    public GameObject ResultText;

    //EndTextを消すためのキー
    public static int key = 0;

    //スコアを設定、「SlaimController」から引用する
    private int Score;

    //キャラ召喚の東西南北の限界値を設定
    public float North = 112.0f;
    public float East = 119.0f;
    public float South = -103.0f;
    public float West = -105.0f;

    //ステージごとの一定秒数に対して増える数
    public int hitokazu = 1; //hito
    public int iwakazu = 1; //iwa
    public int himekazu = 1; //hime

    //hime、hito、iwaを入れる箱を設定
    public GameObject iwa;
    public GameObject hito;
    public GameObject hime;

    //敵たちが増殖するレベル
    public int level = 0;

    //動きを止めさせるためのトリガー
    public static int stopTrigger = 0;

    //ゲーム時間
    private float gameTime = 145;

    //敵が増える時間を示す指標
    private float Sumontime = 0;
    
	// Use this for initialization
	void Start () {

        //最初はstopTriggerをオンにしておく
        stopTrigger = 0;
               
        //UI用オブジェクトの取得
        Image = GameObject.Find("Image");
        StartText = GameObject.Find("StartText");
        ScoreText = GameObject.Find("ScoreText");
        EndText = GameObject.Find("EndText");
        TimeText = GameObject.Find("TimeText");
        ResultText = GameObject.Find("ResultText");

        EndText.SetActive(false); //「EndText」の表示を消す
        ResultText.SetActive(false); //「ResultText」の表示を消す

        //最初にhito50体、iwa30体、hime10体をランダム位置に召喚する
        //まずはhito
        for (int z = 0; z < hitokazu * 50; z++)
        {
            hitoGenerate();
        }

        //次はiwa
        for (int z = 0; z < iwakazu * 30; z++)
        {
            iwaGenerate();
        }

        //最後はhime
        for (int z = 0; z < himekazu * 10; z++)
        {
            himeGenerate();

        }

    }
	
	// Update is called once per frame
	void Update () {
        
        //「SlaimController」からScoreの得点を引用
        Score = SlaimController.score;

        //ScoreTextに獲得した得点を表示
        this.ScoreText.GetComponent<Text>().text = "すこあ→" + this.Score + "。";

        //最初StartTextを出してから、ゲームスタート
        if (!Trigger)
        {

            StartText.transform.position = new Vector2(StartText.transform.position.x + Random.Range(-1.0f, 1.0f), StartText.transform.position.y + Random.Range(-1.0f, 1.0f));　//StartTextを揺らす

            Invoke("StartMesod", 3.0f); //StartMesodを開始

        }
        //トリガーがオン、終了フラグが全て立てばゲーム終了
        else if (Trigger && level == 50 && stopTrigger == 0 && key == 0 && gameTime <= 0.0f)
        {

            key = 1;//キーを設定

            ScoreText.SetActive(false); //「ScoreText」の表示を消す
            EndText.SetActive(true); //「EndText」の表示を出す
            
            Invoke("EndMesod", 3.0f); //EndMesodを開始
            
        }

        

        //文字をそれぞれ押したら、対応する行動をする。
        if (key == 1)
        {
            //「ｚ」を押せばタイトルへ
            if (Input.GetKeyDown(KeyCode.Z))
            {

                //あらゆる要素を元に戻す
                key = 0;
                Trigger = false;
                level = 0;
                Sumontime = 0;
                gameTime = 145;
                Score = 0;
                SlaimController.score = 0;
                SlaimController.Break = 0;

                //戻る
                SceneManager.LoadScene("Title");
                Debug.Log("もどる");
            }
            //「ｃ」を押せば、もう一度遊ぶ
            else if (Input.GetKeyDown(KeyCode.C))
            {
                //あらゆる要素を元に戻す
                key = 0;
                Trigger = false;
                level = 0;
                Sumontime = 0;
                gameTime = 145;
                Score = 0;
                SlaimController.score = 0;
                SlaimController.Break = 0;

                //もう一回
                SceneManager.LoadScene("Battle");
                Debug.Log("もう一回");
            }
            //「ｘ」を押せば、ツイート
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("ついーと");

                //ツイート用の画面を出す
                //本文＋ハッシュタグ＊２ツイート（画像なし）
                naichilab.UnityRoomTweet.Tweet("justicebreaker0000", "あなたは" + Score + "にんふきとばしました。おめでとう！", "unityroom", "じゃすてぃすぶれいか！！！");
                 
            }
        } 

	}

    //hito召喚プログラム
    public void hitoGenerate() {

        //北と南の間でのランダムな位置を設定
        float RandomNS = Random.Range(South, North);
        //東と西
        float RandomEW = Random.Range(West, East);

        //hitoを召喚するための依り代aを作成
        GameObject a = Instantiate(hito) as GameObject;
        //ランダムな位置でhitoを召喚
        a.transform.position = new Vector2(RandomEW, RandomNS);
    }

    //iwa召喚プログラム
    public void iwaGenerate() {

        //北と南の間でのランダムな位置を設定
        float RandomNS = Random.Range(South, North);
        //東と西
        float RandomEW = Random.Range(West, East);

        //iwaを召喚するための依り代bを作成
        GameObject b = Instantiate(iwa) as GameObject;
        //ランダムな位置でiwaを召喚
        b.transform.position = new Vector2(RandomEW, RandomNS);
    }

    //hime召喚プログラム
    public void himeGenerate() {

        //北と南の間でのランダムな位置を設定
        float RandomNS = Random.Range(South, North);
        //東と西
        float RandomEW = Random.Range(West, East);

        //himeを召喚するための依り代cを作成
        GameObject c = Instantiate(hime) as GameObject;
        //ランダムな位置でhimeを召喚
        c.transform.position = new Vector2(RandomEW, RandomNS);

    }

    //時間加算、召喚等のメソッド
    public void StartMesod()
    {
        stopTrigger = 1; //スライムや敵が動ける状態にする。

        //スライムが動ける状態なら、増殖システム作動
        if (stopTrigger == 1)
        {
            StartText.SetActive(false); //「StartText」の表示を消す
            Image.SetActive(false); //「Image」の表示を消す

            //召喚時間を加算させていく
            Sumontime += Time.deltaTime;

            //残り時間の計算
            gameTime -= Time.deltaTime;

            //時間が0を下回らないようにする
            if (gameTime <= 0)
            {
                gameTime = 0.0f;
            }

            //残り時間を表示させる。
            int minutes = Mathf.FloorToInt(gameTime / 60F);//分
            int seconds = Mathf.FloorToInt(gameTime - minutes * 60);//秒
            int mseconds = Mathf.FloorToInt((gameTime - minutes * 60 - seconds) * 1000);//小数点以下
            this.TimeText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, mseconds);


            //時間が10秒ごとにhito、iwa、himeを増加
            //（レベル×定数の増殖数）で敵を召喚
            if (Sumontime >= 10 && level < 10)
            {

                //召喚コルーチンを使う
                StartCoroutine(Hueru());

                Debug.Log("召喚" + (level + 1) + "回目");

                //Sumontimeリセット
                Sumontime = 0;

                //増殖させたら、増殖レベルも増加
                level += 1;

            }
            //levelが10になれば、召喚終了
            else if (level >= 10 && level < 50)
            {
                Debug.Log("召喚終了");
                level = 50; //ゲーム終了用のlevel
                            //Sumontimeリセット
                Sumontime = 0;
            }
            else if (Sumontime >= 45 && level == 50)
            {
                //終了トリガーをオン
                Trigger = true;
                stopTrigger = 0; //スライムや敵を止める。

            }
        }
       

    }

    public void EndMesod()
    {
        if (key == 1) {
            Debug.Log("ゲーム終了");
            EndText.SetActive(false); //「EndText」の表示を消す
            TimeText.SetActive(false); //「TimeText」の表示を消す

            Image.SetActive(true); //「Image」の表示を出す
            ResultText.SetActive(true); //「ResultText」の表示を出す

            ResultText = GameObject.Find("ResultText");//ResultTextのオブジェクト取得

            //スコアで文字を変える。
            if (Score >= 22000)
            {
                //ScoreTextに獲得した得点を表示
                this.ResultText.GetComponent<Text>().text = "さいしゅーすこあ！" + "\n" + this.Score + "！すばらしい！" + "\n" +"たいとるで「すぺーす」おしながら「ｚ」で？" + "\n" + "「ｚ」で戻るよ！" + "\n" + "「ｘ」できろくついーと！" + "\n" + "「ｃ」でもういっかい！";
            }
            else
            {
                //ScoreTextに獲得した得点を表示
                this.ResultText.GetComponent<Text>().text = "さいしゅーすこあ！" + "\n"  + this.Score + "！" + "\n" + "「ｚ」で戻るよ！ 「ｘ」できろくをついーと！！！" + "\n" + "「ｃ」でもういっかい！";
            }

        }
        
    }


    //召喚コルーチン
    IEnumerator Hueru()
    {
        for (int z = 0; z < hitokazu * (4 * level); z++)
        {
            //hitoの召喚
            hitoGenerate();
        }

        for (int z = 0; z < iwakazu * (3 * level); z++)
        {
            //iwaの召喚
            iwaGenerate();
        }

        for (int z = 0; z < himekazu * (2 * level); z++)
        {
            //himeの召喚
            himeGenerate();
        }
       
        // 1秒待つ
        yield return new WaitForSeconds(1.0f);
               
        

    }

}
