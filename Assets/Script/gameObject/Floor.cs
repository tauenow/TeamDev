using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private Vector3 position;//このFloorの2次元配列のポジション
    private string state;//２次元配列で登録されている文字ナンバー

    private MapManager parentMap;

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

    public void CheckFloor()
    {
        //parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition() == );


        for(int i = 0;i < 4; i++)
        {
            if(i == 0)
            {

            }
            else if (i == 1)
            {

            }
            else if (i == 2)
            {

            }
            else if (i == 3)
            {

            }


        }


    }

}
