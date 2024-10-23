using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    

    //�S�[�����܂���
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
    // �}�E�X�J�[�\���̈ʒu����u���C�v���΂��āA�����̃R���C�_�[�ɓ����邩�ǂ������`�F�b�N
    void CastRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            
            if (onGoal == false)//�S�[���Ȃ�\���Ȃ�
            {
                targetObject = hit.collider.gameObject;
                SetCursor(false);

                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    if (targetObject.GetComponent<Floor>().GetFloorState() != "player")
                    {
                        targetObject.GetComponent<Floor>().OnCursor();//�����͕ς�����
                                                                      //�J�[�\�����������Ă���̂�floor��object�ɓ`������
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
    // �Ώۂ̃I�u�W�F�N�g�𒲂ׂ鏈��
    void LookUpTargetObject()
    {
        if (targetObject == null)
        {
            return;
        }
        SetCursor(true);
        //targetObject.GetComponent<InteractableObject>().LookUp();
        // ���̃R���|�[�l���g�𖳌��ɂ���i�������Ȃ��ƒ��ׂĂ���Œ��ɕʂ̃I�u�W�F�N�g�𒲂ׂ邱�Ƃ��ł��Ă��܂��j
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
