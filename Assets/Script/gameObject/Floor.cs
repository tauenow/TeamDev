using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private Vector3 position;//����Floor��2�����z��̃|�W�V����
    private string state;//�Q�����z��œo�^����Ă��镶���i���o�[

    private MapManager parentMap = null;

    private Floor oldFloor = null;//����Floor�̃`�F�b�N�ɓ���O�̏��
    private int rootCount = 0;//����Floor����̕���̐�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetMapPosition(float posX,float posZ,string num)
    {

        position.x = posX;
        position.z = posZ;
        state = num;
    }
    public void SetFloorState(string num)
    {

        if(num == "red")
        {
            state = "blue";
        }
        else if (num == "blue")
        {
            state = "red";
        }

    }

    public Vector3 GetMapPosition()
    {

        return position;

    }

    public string GetFloorState()
    {
        return state;
    }

    public void SetParentmap (MapManager map)
    {
        parentMap = map;
    }
    public void SetRootCount(int count)
    {
        rootCount = count;
    }
    public int GetRootCount()
    {
        return rootCount;
    }

    public void SetOldFloor(Floor floor)
    {
        oldFloor = floor;
    }
    public Floor GetOldFloor()
    {
        return oldFloor;
    }


    public void CheckFloor()
    {
        rootCount = 0;
        List<Floor> floors = new List<Floor>();
        floors = parentMap.GetOldList();

        if (position.z > parentMap.MinPositionZ())
        {
            //Debug.Log("���𒲂ׂ܂�");
            GameObject obj1 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z - 1);

            if (parentMap.GetOldList().Contains(obj1.GetComponent<Floor>()) == false)
            {

                //Debug.Log("�T�����Ă���list�̒��ɂȂ������̂Ń`�F�b�N���܂�");
             
                if (obj1.GetComponent<Floor>().GetFloorState() == "red")
                {
                    parentMap.GetOldList().Add(obj1.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    //Debug.Log("���͐Ԃł�");
                    //root�̐��Ǝ����̏���o�^
                    rootCount++;
                    obj1.GetComponent<Floor>().SetOldFloor(this);
                    obj1.GetComponent<Floor>().CheckFloor();


                }
                else if (obj1.GetComponent<Floor>().GetFloorState() == "goal")
                {
                    parentMap.GetOldList().Add(obj1.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    Debug.Log("goal�ł�");
                    rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.InGoal();
                }
            }
        }


        if (position.z < parentMap.MaxPositionZ())
        {
            //Debug.Log("���𒲂ׂ܂�");
            GameObject obj2 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == (GetComponent<Floor>().GetMapPosition().z + 1));

            if (parentMap.GetOldList().Contains(obj2.GetComponent<Floor>()) == false)
            {

                //Debug.Log("�T�����Ă���list�̒��ɂȂ������̂Ń`�F�b�N���܂�");

                if (obj2.GetComponent<Floor>().GetFloorState() == "red")
                {
                    parentMap.GetOldList().Add(obj2.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    //Debug.Log("���͐Ԃł�");
                    rootCount++;
                    obj2.GetComponent<Floor>().SetOldFloor(this);
                    obj2.GetComponent<Floor>().CheckFloor();
                }
                else if (obj2.GetComponent<Floor>().GetFloorState() == "goal")
                {
                    parentMap.GetOldList().Add(obj2.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    Debug.Log("goal�ł�");
                    rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.InGoal();
                }

            }

        }


        if (position.x > parentMap.MinPositionX())
        {
            //Debug.Log("���𒲂ׂ܂�");
            GameObject obj3 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);

            if (parentMap.GetOldList().Contains(obj3.GetComponent<Floor>()) == false)
            {

                //Debug.Log("�T�����Ă���list�̒��ɂȂ������̂Ń`�F�b�N���܂�");
                
                if (obj3.GetComponent<Floor>().GetFloorState() == "red")
                {
                    parentMap.GetOldList().Add(obj3.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    //Debug.Log("���͐Ԃł�");
                    //root�̐��Ǝ����̏���o�^
                    rootCount++;
                    obj3.GetComponent<Floor>().SetOldFloor(this);
                    obj3.GetComponent<Floor>().CheckFloor();
                }
                else if (obj3.GetComponent<Floor>().GetFloorState() == "goal")
                {
                    parentMap.GetOldList().Add(obj3.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    Debug.Log("goal�ł�");
                    rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.InGoal();
                }

            }
        }

        if (position.x < parentMap.MaxPositionX())
        {
            GameObject obj4 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);
            //Debug.Log("���𒲂ׂ܂�");

            if (parentMap.GetOldList().Contains(obj4.GetComponent<Floor>()) == false)
            {
                //�T�����Ă���list�̒��ɂȂ���ΐi��
                if (obj4.GetComponent<Floor>().GetFloorState() == "red")
                {
                    parentMap.GetOldList().Add(obj4.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    //Debug.Log("���͐Ԃł�");
                    //root�̐��Ǝ����̏���o�^
                    rootCount++;
                    obj4.GetComponent<Floor>().SetOldFloor(this);
                    obj4.GetComponent<Floor>().CheckFloor();
                   
                }
                else if (obj4.GetComponent<Floor>().GetFloorState() == "goal")
                {
                    parentMap.GetOldList().Add(obj4.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
                    Debug.Log("goal�ł�");
                    rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.InGoal();
                }

            }

        }
    }
    public void CheckOldRoot()
    {

        //root������Ȃ�������O�̏���root���m�F
        if (rootCount == 0)
        {
            Debug.Log("�߂�܂�");
            if (oldFloor != null) oldFloor.CheckOldRoot();

            Debug.Log(position.x);
            Debug.Log(position.z);
        }
        else if (rootCount >= 0)
        {
            rootCount--;
            if (rootCount == 0)
            {
                Debug.Log("�߂�܂�");
                if (oldFloor != null) oldFloor.CheckOldRoot();
                Debug.Log(position.x);
                Debug.Log(position.z);
            }
        }

    }

    
}
