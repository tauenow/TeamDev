using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    //���ʃf�[�^
    [SerializeField]
    private StageScriptableObject scriptableObject;
    //�J����
    Camera mainCamera;
    //���C�̃q�b�g
    RaycastHit hit;
    //���C�Ńq�b�g�����I�u�W�F�N�g
    GameObject targetObject = null;

    void Start()
    {
        enabled = true;
        mainCamera = Camera.main;
    }
    void Update()
    {
       
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
                    if (targetObject.GetComponent<Floor>().GetFloorState() != "player"&&targetObject.GetComponent<Floor>().GetFloorState() != "goal")
                    {
                        if (targetObject.GetComponent<Floor>().GetChangeState() == false && targetObject.GetComponent<Floor>().GetLinkChangeState() == false)
                        {
                            if (MapManager.floorChange == false)
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
                        if (MapManager.floorChange == false)
                        {
                            if (targetObject.GetComponent<Floor>().GetMapPosition().x == 1 && targetObject.GetComponent<Floor>().GetMapPosition().z == 1)//�G���Ă����I�u�W�F�N�g
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
    }
    
}
