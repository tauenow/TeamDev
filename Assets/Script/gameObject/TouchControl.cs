using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    //共通データ
    [SerializeField]
    private StageScriptableObject scriptableObject;
    //ゴールしました
    public bool onGoal = false;
    //カメラ
    Camera mainCamera;
    //レイのヒット
    RaycastHit hit;
    //レイでヒットしたオブジェクト
    GameObject targetObject = null;

    private bool result = false;

    void Start()
    {
        enabled = true;
        onGoal = false;
        result = false;
        mainCamera = Camera.main;
    }
    void Update()
    {
        //シーンの切り替え処理
        if(result == true)
        {
            TouchChangeScene();
        }
        else if(mainCamera.GetComponent<CameraControl>().GetIsResult() == true)
        {
            Invoke(nameof(OnResultTouch), 2.0f);
        }
        //タッチ操作処理
        CastRay();
        //チュートリアルの時のタッチ操作
        TutorialCastRay();

    }
    // マウスカーソルの位置から「レイ」を飛ばして、何かのコライダーに当たるかどうかをチェック
    void CastRay()
    {
        if (scriptableObject.tutorialClear == false) return;

        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Vector3 point = touch.position;
            Ray ray = mainCamera.ScreenPointToRay(point);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
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
                        if (targetObject.GetComponent<Floor>().GetChangeState() == false && targetObject.GetComponent<Floor>().GetLinkChangeState() == false)
                        {
                            if (targetObject.GetComponent<Floor>().GetChangeWait() == false)
                            {
                                targetObject.GetComponent<Floor>().SetChangeWait(true);
                            }
                            else if (targetObject.GetComponent<Floor>().GetChangeWait() == true)
                            {

                                MapManager.floorChange = true;
                                GetComponent<MapManager>().ChangeMap(targetObject);//マップチェンジとチェック
                                enabled = false;
                            }
                        }
                        else
                        {
                            GetComponent<MapManager>().AllFloorWaitOff();
                        }
                    }
                }
            }
        }
    }
    void TutorialCastRay()
    {
        if (scriptableObject.tutorialClear == true) return;
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Vector3 point = touch.position;
            Ray ray = mainCamera.ScreenPointToRay(point);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
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
                        if (targetObject.GetComponent<Floor>().GetMapPosition().x == 2 && targetObject.GetComponent<Floor>().GetMapPosition().z == 2)//触っていいオブジェクト
                        { 
                            if (targetObject.GetComponent<Floor>().GetChangeWait() == false)
                            {

                                targetObject.GetComponent<Floor>().SetChangeWait(true);
                            }
                            else if (targetObject.GetComponent<Floor>().GetChangeWait() == true)
                            {
                                if (scriptableObject.textIndex == 1)
                                {
                                    GetComponent<MapManager>().ChangeMap(targetObject);//マップチェンジとチェック
                                    enabled = false;
                                    scriptableObject.textIndex++;
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
    void TouchChangeScene()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            GameObject.Find("StageManager").GetComponent<StageSelectManager>().ChangeScene();
        }

    }
    // 対象のオブジェクトを調べる処理
    void OnResultTouch()
    {
        result = true;
    }
   
}
