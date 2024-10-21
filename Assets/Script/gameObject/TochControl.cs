using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TochControl : MonoBehaviour
{
    //�S�[�����܂���
    public bool onGoal = false;
    [SerializeField]
    Texture2D defaultCursor = null;
    [SerializeField]
    Texture2D interactCursor = null;
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
    void OnDisable()
    {
       
    }
    // �}�E�X�J�[�\���̈ʒu����u���C�v���΂��āA�����̃R���C�_�[�ɓ����邩�ǂ������`�F�b�N
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
                        targetObject.GetComponent<Floor>().OnCursor();//�����͕ς�����
                                                                      //�J�[�\�����������Ă���̂�floor��object�ɓ`������
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
