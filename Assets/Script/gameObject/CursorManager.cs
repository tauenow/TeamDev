using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    //���ʃf�[�^
    [SerializeField]
    private StageScriptableObject scriptableObject;
   
    [SerializeField]
    Texture2D defaultCursor = null;
    Camera mainCamera;
    RaycastHit hit;
    GameObject targetObject;

    void Start()
    {
        enabled = true;
        //������
        
        mainCamera = Camera.main;
        SetCursor(true);
    }
    void Update()
    {
       
        //�J�[�\������
        CastRay();
        //�`���[���A���̎��̃J�[�\������
        TutorialCastRay();
    }
    // �}�E�X�J�[�\���̈ʒu����u���C�v���΂��āA�����̃R���C�_�[�ɓ����邩�ǂ������`�F�b�N
    void CastRay()
    {
        if (scriptableObject.tutorialClear == false) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {

            targetObject = hit.collider.gameObject;

            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                if (targetObject.GetComponent<Floor>().GetFloorState() != "player" && targetObject.GetComponent<Floor>().GetFloorState() != "goal")
                {
                    if (targetObject.GetComponent<Floor>().GetChangeState() == false && targetObject.GetComponent<Floor>().GetLinkChangeState() == false)
                    {
                        if (MapManager.floorChange == false)
                        {
                            //�J�[�\�����������Ă���̂��I�u�W�F�N�g�ɓ`����
                            targetObject.GetComponent<Floor>().OnCursor();

                            if (Input.GetMouseButtonDown(0))
                            {

                                Debug.Log("�����܂���");
                                MapManager.floorChange = true;
                                GetComponent<MapManager>().ChangeMap(targetObject);
                                enabled = false;

                            }
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
                    if (MapManager.floorChange == false)
                    {
                        //�J�[�\�����������Ă���̂��I�u�W�F�N�g�ɓ`����
                        targetObject.GetComponent<Floor>().OnCursor();

                        if (Input.GetMouseButtonDown(0))
                        {
                            if (targetObject.GetComponent<Floor>().GetMapPosition().x == 2 && targetObject.GetComponent<Floor>().GetMapPosition().z == 2)//�G���Ă����I�u�W�F�N�g
                            {
                                if (scriptableObject.textIndex == 1)
                                {
                                    Debug.Log("�����܂���");
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
    }
    
    // �Ώۂ̃I�u�W�F�N�g�𒲂ׂ鏈
    public void SetCursor(bool isDefault)
    {
        if (isDefault == true)
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
    
}
