using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TochControl : MonoBehaviour
{
    //ゴールしました
    public bool onGoal = false;
    //カメラ
    Camera mainCamera;
    //レイのヒット
    RaycastHit hit;
    //レイでヒットしたオブジェクト
    GameObject targetObject;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        CastRay();
    }
    // マウスカーソルの位置から「レイ」を飛ばして、何かのコライダーに当たるかどうかをチェック
    void CastRay()
    {

        if (Input.touchCount <= 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Vector3 point = touch.position;
            Ray ray = mainCamera.ScreenPointToRay(point);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (onGoal == false)//ゴールしたら構えない
                {
                    targetObject = hit.collider.gameObject;

                    if (targetObject == null)
                    {
                        GetComponent<MapManager>().AllFloorWaitOff();
                        return;
                    }
                    else if (targetObject.CompareTag("Floor"))
                    {
                        if (targetObject.GetComponent<Floor>().GetFloorState() != "player")
                        {
                            if (targetObject.GetComponent<Floor>().GetChangeWait() == false)
                            {
                                targetObject.GetComponent<Floor>().SetChangeWait(true);
                            }
                            else if (targetObject.GetComponent<Floor>().GetChangeWait() == true)
                            {
                                if (MapManager.floorChange == false)
                                {

                                    MapManager.floorChange = true;
                                    GetComponent<MapManager>().ChangeMap(targetObject);//マップチェンジとチェック

                                }
                            }
                        }
                    }

                }
            }
        }
        

    }

    void CheckTouch()
    {
        if (Input.touchCount <= 0) return;
        
    }
    // 対象のオブジェクトを調べる処理
    void LookUpTargetObject()
    {
        if (targetObject == null)
        {
            return;
        }
        //targetObject.GetComponent<InteractableObject>().LookUp();
        // このコンポーネントを無効にする（そうしないと調べている最中に別のオブジェクトを調べることができてしまう）
        //enabled = false;
    }
   
}
