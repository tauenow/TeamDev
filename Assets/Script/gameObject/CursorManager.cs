using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    //共通データ
    [SerializeField]
    private StageScriptableObject scriptableObject;
   
    [SerializeField]
    Texture2D defaultCursor = null;
    Camera mainCamera;
    RaycastHit hit;
    GameObject targetObject;

    private bool result = false;

    //一回しか処理しないための変数
    private bool doOnce = false;

    void Start()
    {
        enabled = true;
        //初期化
        
        result = false;

        mainCamera = Camera.main;
        SetCursor(true);
    }
    void Update()
    {
        //シーン切り替え処理
        if(result == true)
        {
            ClickChangeScene();
        }
        else if (mainCamera.GetComponent<CameraControl>().GetIsResult() == true)
        {
            Invoke(nameof(OnResultClick), 2.0f);
        }
        //カーソル処理
        CastRay();
        //チューリアルの時のカーソル処理
        TutorialCastRay();
    }
    // マウスカーソルの位置から「レイ」を飛ばして、何かのコライダーに当たるかどうかをチェック
    void CastRay()
    {
        if (scriptableObject.tutorialClear == false) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {

            targetObject = hit.collider.gameObject;

            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                if (targetObject.GetComponent<Floor>().GetFloorState() != "player")
                {
                    if (targetObject.GetComponent<Floor>().GetChangeState() == false && targetObject.GetComponent<Floor>().GetLinkChangeState() == false)
                    {
                        //カーソルが当たっているのをオブジェクトに伝える
                        targetObject.GetComponent<Floor>().OnCursor();

                        if (Input.GetMouseButtonDown(0))
                        {

                            Debug.Log("押しました");
                            MapManager.floorChange = true;
                            GetComponent<MapManager>().ChangeMap(targetObject);
                            enabled = false;

                        }
                    }
                }
            }
        }
    }
    void TutorialCastRay()
    {
        if (scriptableObject.tutorialClear == true) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {

            targetObject = hit.collider.gameObject;

            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                if (targetObject.GetComponent<Floor>().GetFloorState() != "player")
                {
                    //カーソルが当たっているのをオブジェクトに伝える
                    targetObject.GetComponent<Floor>().OnCursor();

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (targetObject.GetComponent<Floor>().GetMapPosition().x == 2 && targetObject.GetComponent<Floor>().GetMapPosition().z == 2)//触っていいオブジェクト
                        {
                            if (scriptableObject.textIndex == 1)
                            {
                                Debug.Log("押しました");
                                GetComponent<MapManager>().ChangeMap(targetObject);
                                enabled = false;
                                scriptableObject.textIndex++;
                            }
                        }
                    }
                }
            }
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
    }
    void OnResultClick()
    {
        Debug.Log("シーン変えれるで");
        result = true;
    }
}
