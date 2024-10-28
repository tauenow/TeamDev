using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    //���ʃf�[�^
    [SerializeField]
    private StageScriptableObject scriptableObject;
    //�S�[�����܂���
    public bool onGoal = false;
    //�J����
    Camera mainCamera;
    //���C�̃q�b�g
    RaycastHit hit;
    //���C�Ńq�b�g�����I�u�W�F�N�g
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
        //�V�[���̐؂�ւ�����
        if(result == true)
        {
            TouchChangeScene();
        }
        else if(mainCamera.GetComponent<CameraControl>().GetIsResult() == true)
        {
            Invoke(nameof(OnResultTouch), 2.0f);
        }
        //�^�b�`���쏈��
        CastRay();
        //�`���[�g���A���̎��̃^�b�`����
        TutorialCastRay();

    }
    // �}�E�X�J�[�\���̈ʒu����u���C�v���΂��āA�����̃R���C�_�[�ɓ����邩�ǂ������`�F�b�N
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
                                GetComponent<MapManager>().ChangeMap(targetObject);//�}�b�v�`�F���W�ƃ`�F�b�N
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
                        if (targetObject.GetComponent<Floor>().GetMapPosition().x == 2 && targetObject.GetComponent<Floor>().GetMapPosition().z == 2)//�G���Ă����I�u�W�F�N�g
                        { 
                            if (targetObject.GetComponent<Floor>().GetChangeWait() == false)
                            {

                                targetObject.GetComponent<Floor>().SetChangeWait(true);
                            }
                            else if (targetObject.GetComponent<Floor>().GetChangeWait() == true)
                            {
                                if (scriptableObject.textIndex == 1)
                                {
                                    GetComponent<MapManager>().ChangeMap(targetObject);//�}�b�v�`�F���W�ƃ`�F�b�N
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
    // �Ώۂ̃I�u�W�F�N�g�𒲂ׂ鏈��
    void OnResultTouch()
    {
        result = true;
    }
   
}
