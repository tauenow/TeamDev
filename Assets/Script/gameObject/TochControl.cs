using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TochControl : MonoBehaviour
{
    //ゴールしました
    public bool onGoal = false;
    [SerializeField]
    Texture2D defaultCursor = null;
    [SerializeField]
    Texture2D interactCursor = null;
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
    void OnDisable()
    {
       
    }
    // マウスカーソルの位置から「レイ」を飛ばして、何かのコライダーに当たるかどうかをチェック
    void CastRay()
    {

        CheckTouch();

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {

            Vector3 point = touch.position;
            Ray ray = mainCamera.ScreenPointToRay(point);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (onGoal == false)
                {

                    targetObject = hit.collider.gameObject;
                   
                    if (hit.collider.gameObject.tag == "Floor")
                    {
                        targetObject.GetComponent<Floor>().OnCursor();//ここは変えたい
                                                                      //カーソルが当たっているのをfloorのobjectに伝えたい
                        if (CursorManager.floorChange == false)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {

                                CursorManager.floorChange = true;
                                GetComponent<MapManager>().ChangeMap(targetObject);

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
