using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    //Slimeのオブジェクト
    public GameObject Slime;

	// Use this for initialization
	void Start () {

        //Slimeのオブジェクトを取得
        Slime = GameObject.Find("Slime");

		
	}
	
	// Update is called once per frame
	void Update () {

        //Slimeを中心にカメラ位置を付ける
        this.transform.position = new Vector3(Slime.transform.position.x, Slime.transform.position.y -3, -10);
		
	}
}
