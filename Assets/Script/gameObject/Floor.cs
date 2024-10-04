using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private Vector3 position;//����Floor��2�����z��̃|�W�V����
    private string state;//�Q�����z��œo�^����Ă��镶���i���o�[

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

        if(num == "1")
        {
            state = "2";
        }
        else if (num == "2")
        {
            state = "1";
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
    

}
