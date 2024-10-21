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

        

        if (centerObject != null)
        {

            transform.LookAt(centerObject.transform);//カメラターゲットを登録したオブジェクトにしてる
            if (center == false)
            {
                float x = centerObject.transform.position.x;
                Vector3 pos = centerObject.transform.position;
                pos.y = Hight;
                pos.z = centerObject.transform.position.z - sideLength;//マジークナンバーごめんなさい
                transform.position = pos;
                center = true;
               
            }
            //if (Input.GetMouseButton(1))//0が左クリック１が右クリック
            //{
            //    rotateCamera();

            //}
        }
       
    }
    public void CentrCretae(GameObject center)
    {
        centerObject = center;//カメラのセンターになるオブジェクトを見つける
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
