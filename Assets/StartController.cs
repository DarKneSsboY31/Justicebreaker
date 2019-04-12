using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

    //ルール画面に行く為のキー
    private int a = 0;

    //RuleTextとImageの取得用オブジェクト
    public GameObject RuleText;
    public GameObject RuleImage;

	// Use this for initialization
	void Start () {

        //UI用のオブジェクトの取得
        RuleImage = GameObject.Find("Image");
        RuleText = GameObject.Find("RuleText");

        RuleImage.SetActive(false);//黒い画像消去
        RuleText.SetActive(false);//ルール消去

    }
	
	// Update is called once per frame
	void Update () {

        
        //「ｚ」を押すとゲームスタート、ルールを見ている時はスタートしない
        if (Input.GetKeyDown(KeyCode.Z)　&& a == 0)
        {

            Debug.Log("げーむかいし");
            //戻る
            SceneManager.LoadScene("Battle");//Battleシーンへ行く

        }
        //「ｘ」を押せばルール説明、もう一度押せばもとに戻る
        else if (Input.GetKeyDown(KeyCode.X) && a == 0)
        {
            Debug.Log("黒い画面と白い文字出た");
            RuleImage.SetActive(true);//黒い画像表示
            RuleText.SetActive(true);//ルール表示
            RuleText = GameObject.Find("RuleText");//RuleTextのオブジェクト取得

            //RuleTextにルールを表示
            this.RuleText.GetComponent<Text>().text = "るーる！" + "\n" + "うえ、した、みぎ、ひだりキーで" + "\n" + "あおいスライムをうごかして" + "\n" + "ひとをふきとばそう！" + "\n" + "たおせばたおすほどはやくなるよ！" + "\n" + "ひとは3しゅるい！" + "\n" + "とくてんもちがうよ！" + "\n" + "せいげんじかんいないに" + "\n" + "いっぱいたおそう！" + "\n" + "「ｘ」でもどる";

            a = 1;//ルール説明用のキーを作動

        }
        else if (a == 1 && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("黒い画面と白い文字消えた");
            a = 0;//キーを戻す
            RuleImage.SetActive(false);//黒い画像消去
            RuleText.SetActive(false);//ルール消去

        }

    }
}
