using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TochControl : MonoBehaviour
{
    //�S�[�����܂���
    public bool onGoal = false;
    //�J����
    Camera mainCamera;
    //���C�̃q�b�g
    RaycastHit hit;
    //���C�Ńq�b�g�����I�u�W�F�N�g
    GameObject targetObject;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        CastRay();
    }
    // �}�E�X�J�[�\���̈ʒu����u���C�v���΂��āA�����̃R���C�_�[�ɓ����邩�ǂ������`�F�b�N
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
                if (onGoal == false)//�S�[��������\���Ȃ�
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
                                    GetComponent<MapManager>().ChangeMap(targetObject);//�}�b�v�`�F���W�ƃ`�F�b�N

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
    // �Ώۂ̃I�u�W�F�N�g�𒲂ׂ鏈��
    void LookUpTargetObject()
    {
        if (targetObject == null)
        {
            return;
        }
        //targetObject.GetComponent<InteractableObject>().LookUp();
        // ���̃R���|�[�l���g�𖳌��ɂ���i�������Ȃ��ƒ��ׂĂ���Œ��ɕʂ̃I�u�W�F�N�g�𒲂ׂ邱�Ƃ��ł��Ă��܂��j
        //enabled = false;
    }
   
}
