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

    void Start()
    {
        
    }
    void Update()
    {
       
        centerObject = GameObject.Find("center(Clone)");//�J�����̃Z���^�[�ɂȂ�I�u�W�F�N�g��������
        transform.LookAt(centerObject.transform);//�J�����^�[�Q�b�g��o�^�����I�u�W�F�N�g�ɂ��Ă�
       
        if (centerObject != null)
        {
            if (center == false)
            {
                float x = centerObject.transform.position.x;
                Vector3 pos = centerObject.transform.position;
                pos.y = Hight;
                pos.z = centerObject.transform.position.z - sideLength;//�}�W�[�N�i���o�[���߂�Ȃ���
                transform.position = pos;
                center = true;
                Debug.Log(centerObject.transform.position.x);
                Debug.Log(centerObject.transform.position.z);
            }
            if (Input.GetMouseButton(1))//0�����N���b�N�P���E�N���b�N
            {
                rotateCamera();

            }
        }
       
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
   
}
