using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    
    private GameObject centerObject;  //カメラの中心のオブジェクト
    public float rotateSpeed = 1.0f; //回転スピード

    [SerializeField]
    private float Hight = 0.0f;
    [SerializeField]
    private float sideLength = 0.0f;
    bool center = false;

    void Start()
    {
        
    }
    void Update()
    {
       
        centerObject = GameObject.Find("center(Clone)");//カメラのセンターになるオブジェクトを見つける
        transform.LookAt(centerObject.transform);//カメラターゲットを登録したオブジェクトにしてる
       
        if (centerObject != null)
        {
            if (center == false)
            {
                float x = centerObject.transform.position.x;
                Vector3 pos = centerObject.transform.position;
                pos.y = Hight;
                pos.z = centerObject.transform.position.z - sideLength;//マジークナンバーごめんなさい
                transform.position = pos;
                center = true;
                Debug.Log(centerObject.transform.position.x);
                Debug.Log(centerObject.transform.position.z);
            }
            if (Input.GetMouseButton(1))//0が左クリック１が右クリック
            {
                rotateCamera();

            }
        }
       
    }
   
    private void rotateCamera()
    {
        Vector3 angle = new Vector3(
                Input.GetAxis("Mouse X") * rotateSpeed,
                0,
                0
            );
       transform.RotateAround(centerObject.transform.position, Vector3.up, angle.x);
    }
   
}
