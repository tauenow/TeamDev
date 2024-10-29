using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSceneControl : MonoBehaviour
{
    //���ʃf�[�^
    [SerializeField]
    private StageScriptableObject scriptableObject;
    //�J����
    Camera mainCamera;
    private bool result = false;
    void Start()
    {
        enabled = false;
        result = false;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //�V�[���̐؂�ւ�����
        if (result == true)
        {
            TouchChangeScene();
        }
        else if (mainCamera.GetComponent<CameraControl>().GetIsResult() == true)
        {
            Invoke(nameof(OnResultTouch), 2.0f);
        }
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
    // �Ώۂ̃I�u�W�F�N�g�𒲂ׂ鏈��
    void OnResultTouch()
    {
        result = true;
    }
}
