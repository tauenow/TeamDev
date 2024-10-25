using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    

    //ゴールしました
    public bool onGoal = false;
    [SerializeField]
    Texture2D defaultCursor = null;
    [SerializeField]
    Texture2D interactCursor = null;
    Camera mainCamera;
    RaycastHit hit;
    GameObject targetObject;

    private bool result = false;

    void Start()
    {
        //初期化
        onGoal = false;
        result = false;

        mainCamera = Camera.main;
        SetCursor(true);
    }
    void Update()
    {
        if(result == true)
        {
            ClickChangeScene();
        }
        else if (mainCamera.GetComponent<CameraControl>().GetIsResult() == true)
        {
            Invoke(nameof(OnResultClick), 2.0f);
        }
        CastRay();
    }
    void OnDisable()
    {
        SetCursor(true);
    }
    // マウスカーソルの位置から「レイ」を飛ばして、何かのコライダーに当たるかどうかをチェック
    void CastRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            
            if (onGoal == false)//ゴールなら構えない
            {
                targetObject = hit.collider.gameObject;
                //SetCursor(false);
                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    if (targetObject.GetComponent<Floor>().GetFloorState() != "player")
                    {
                        //カーソルが当たっているのをオブジェクトに伝える
                        targetObject.GetComponent<Floor>().OnCursor();
                                                                      
                        if (MapManager.floorChange == false)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {

                                MapManager.floorChange = true;
                                GetComponent<MapManager>().ChangeMap(targetObject);

                            }
                        }
                    }
                }

            }
            }
            else
            {
            targetObject = null;
            SetCursor(true);
        }

    }
    void ClickChangeScene()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject.Find("StageManager").GetComponent<StageSelectManager>().ChangeScene();
        }
    }
    // 対象のオブジェクトを調べる処理
    void LookUpTargetObject()
    {
        if (targetObject == null)
        {
            return;
        }
        SetCursor(true);
        //targetObject.GetComponent<InteractableObject>().LookUp();
        // このコンポーネントを無効にする（そうしないと調べている最中に別のオブジェクトを調べることができてしまう）
        //enabled = false;
    }
    public void SetCursor(bool isDefault)
    {
        if (isDefault == true)
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
        }
    }
    void OnResultClick()
    {
        Debug.Log("シーン変えれるで");
        result = true;
    }
}
