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
    private bool clearCameraMove = false;
    private float currentTime = 0.0f;
    void Start()
    {
        
    }
    void Update()
    {

        if (centerObject != null)
        {
            // 補完スピードを決める
            float speed = 0.01f;
            // ターゲット方向のベクトルを取得
            Vector3 relativePos = centerObject.transform.position - this.transform.position;
            // 方向を、回転情報に変換
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // 現在の回転情報と、ターゲット方向の回転情報を補完する
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

            //transform.LookAt(centerObject.transform);//カメラターゲットを登録したオブジェクトにしてる
            if (center == false)
            {
                float x = centerObject.transform.position.x;
                Vector3 pos = centerObject.transform.position;
                pos.y = Hight;
                pos.z = centerObject.transform.position.z - sideLength;//マジークナンバーごめんなさい
                transform.position = pos;
                center = true;
               
            }
        }
        if(clearCameraMove == true)
        {
            MoveCamera();
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
    private void MoveCamera()
    {
        currentTime += Time.deltaTime;

        if (currentTime < 2.0f && currentTime >= 1.0f)
        {
            centerObject = GameObject.Find("map(Clone)").GetComponent<MapManager>().GetGameObjectList().Find(match => match.GetComponent<Floor>().GetFloorState() == "goal");

            
        }
        else if (currentTime < 1.0f && currentTime >= 0.0f)
        {
            
            transform.RotateAround(centerObject.transform.position, Vector3.right, -rotateSpeed);
            Vector3 pos = transform.position;
            pos.z += -rotateSpeed * 0.1f;//マジックナンバーごめん
            transform.position = pos;
        }
        else if(currentTime>= 2.0f)
        {
            clearCameraMove = false;
            currentTime = 0.0f;
        }

    }
    public void OnCameraMove()
    {
        clearCameraMove = true;
    }
}
