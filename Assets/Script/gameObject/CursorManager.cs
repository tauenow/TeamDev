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

    void Start()
    {
        mainCamera = Camera.main;
        SetCursor(true);
    }
    void Update()
    {
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
                SetCursor(false);

                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    if (targetObject.GetComponent<Floor>().GetFloorState() != "player")
                    {
                        targetObject.GetComponent<Floor>().OnCursor();//ここは変えたい
                                                                      //カーソルが当たっているのをfloorのobjectに伝えたい
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
        if (isDefault)
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
