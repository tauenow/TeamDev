using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //�����݂�ȋC��t����
    public static bool floorChange = false;

    [SerializeField]
    Texture2D defaultCursor = null;
    [SerializeField]
    Texture2D interactCursor = null;
    Camera mainCamera;
    RaycastHit hit;
    GameObject targetObject;

    ChangeFloor floor;

    void Start()
    {
        mainCamera = Camera.main;
        SetCursor(true);
        floor = GetComponent<ChangeFloor>();
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
            targetObject = hit.collider.gameObject;
            SetCursor(false);

            if(hit.collider.gameObject.tag =="RedFloor" || hit.collider.gameObject.tag == "BlueFloor")
            {
                targetObject.SendMessage("OnCursor");
                //�J�[�\�����������Ă���̂�floor��object�ɓ`������
                if (floorChange == false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<MapLoad>().ChangeMap(targetObject);
                        targetObject.SendMessage("OnChange");
                        floorChange = true;
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
