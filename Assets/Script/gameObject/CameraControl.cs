using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    
    private GameObject centerObject;  //�J�����̒��S�̃I�u�W�F�N�g
    public float rotateSpeed = 1.0f; //��]�X�s�[�h

    [SerializeField]
    private float Hight = 0.0f;
    [SerializeField]
    private float sideLength = 0.0f;
    bool center = false;
    private bool clearCameraMove = false;
    private float currentTime = 0.0f;
    void Start()
    {
        
    }
    void Update()
    {

        if (centerObject != null)
        {
            // �⊮�X�s�[�h�����߂�
            float speed = 0.01f;
            // �^�[�Q�b�g�����̃x�N�g�����擾
            Vector3 relativePos = centerObject.transform.position - this.transform.position;
            // �������A��]���ɕϊ�
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // ���݂̉�]���ƁA�^�[�Q�b�g�����̉�]����⊮����
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

            //transform.LookAt(centerObject.transform);//�J�����^�[�Q�b�g��o�^�����I�u�W�F�N�g�ɂ��Ă�
            if (center == false)
            {
                float x = centerObject.transform.position.x;
                Vector3 pos = centerObject.transform.position;
                pos.y = Hight;
                pos.z = centerObject.transform.position.z - sideLength;//�}�W�[�N�i���o�[���߂�Ȃ���
                transform.position = pos;
                center = true;
               
            }
        }
        if(clearCameraMove == true)
        {
            MoveCamera();
        }
       
    }
    public void CentrCretae(GameObject center)
    {
        centerObject = center;//�J�����̃Z���^�[�ɂȂ�I�u�W�F�N�g��������
    }
   
    private void rotateCamera()
    {
        Vector3 angle = new Vector3(
                Input.GetAxis("Mouse X") * rotateSpeed,
                0,
                0
        );
       transform.RotateAround(centerObject.transform.position, Vector3.up, angle.x);
    }
    private void MoveCamera()
    {
        currentTime += Time.deltaTime;

        if (currentTime < 2.0f && currentTime >= 1.0f)
        {
            centerObject = GameObject.Find("map(Clone)").GetComponent<MapManager>().GetGameObjectList().Find(match => match.GetComponent<Floor>().GetFloorState() == "goal");

            
        }
        else if (currentTime < 1.0f && currentTime >= 0.0f)
        {
            
            transform.RotateAround(centerObject.transform.position, Vector3.right, -rotateSpeed);
            Vector3 pos = transform.position;
            pos.z += -rotateSpeed * 0.1f;//�}�W�b�N�i���o�[���߂�
            transform.position = pos;
        }
        else if(currentTime>= 2.0f)
        {
            clearCameraMove = false;
            currentTime = 0.0f;
        }

    }
    public void OnCameraMove()
    {
        clearCameraMove = true;
    }
}
