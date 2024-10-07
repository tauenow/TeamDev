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

    public void CheckFloor(List<Floor> oldGameObjectList)
    {

        List<Floor> old = oldGameObjectList;
        List<bool> bools = new List<bool>();


        for(int i = 0;i < 4; i++)
        {
            if(i == 0)
            {
                Debug.Log(position.z + " > " + parentMap.MinPositionZ());
                if (position.z > parentMap.MinPositionZ())
                {
                    Debug.Log("赤です");
                    GameObject obj1 =  parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z + 1);
                    if (old.Find(match => match.GetComponent<Floor>() != obj1.GetComponent<Floor>()))
                    {
                        if(obj1.GetComponent<Floor>().GetFloorState() == "red")
                        {
                            bools.Add(true);
                            old.Add(obj1.GetComponent<Floor>());
                            obj1.GetComponent<Floor>().CheckFloor(old);
                            Debug.Log("赤です");
                        }
                        else if(obj1.GetComponent<Floor>().GetFloorState() == "goal")
                        {
                            Debug.Log("Goalです");
                        }
                        else
                        {

                            Debug.Log("Noneです");
                            bools.Add(true);
                        }
                    }
                }
            }
            else if (i == 1)
            {
                if (position.z < parentMap.MaxPositionZ())
                {
                    GameObject obj2 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z - 1);
                    if (old.Find(match => match.GetComponent<Floor>() != obj2.GetComponent<Floor>()))
                    {
                        if (obj2.GetComponent<Floor>().GetFloorState() == "red")
                        {

                            old.Add(obj2.GetComponent<Floor>());
                            obj2.GetComponent<Floor>().CheckFloor(old);
                            Debug.Log("赤です");
                        }
                        else if (obj2.GetComponent<Floor>().GetFloorState() == "goal")
                        {
                            Debug.Log("Goalです");
                        }
                        else
                        {
                            Debug.Log("Noneです");
                            bools.Add(true);
                        }
                    }
                }
            }
            else if (i == 2)
            {
                if (position.x > parentMap.MinPositionX())
                {
                    GameObject obj3 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);
                    if (old.Find(match => match.GetComponent<Floor>() != obj3.GetComponent<Floor>()))
                    {
                        if (obj3.GetComponent<Floor>().GetFloorState() == "red")
                        {

                            old.Add(obj3.GetComponent<Floor>());
                            obj3.GetComponent<Floor>().CheckFloor(old);
                        }
                        else if (obj3.GetComponent<Floor>().GetFloorState() == "goal")
                        {

                        }
                        else
                        {
                            Debug.Log("Noneです");
                            bools.Add(true);
                        }
                    }
                }
            }
            else if (i == 3)
            {
                if (position.z > 0)
                {
                    GameObject obj4 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);
                    if (old.Find(match => match.GetComponent<Floor>() != obj4.GetComponent<Floor>()))
                    {
                        if (obj4.GetComponent<Floor>().GetFloorState() == "red")
                        {

                            old.Add(obj4.GetComponent<Floor>());
                            obj4.GetComponent<Floor>().CheckFloor(old);
                        }
                        else if (obj4.GetComponent<Floor>().GetFloorState() == "goal")
                        {

                        }
                        else
                        {
                            Debug.Log("Noneです");
                            bools.Add(true);
                        }
                    }

                }
            }


        }

        if (bools.Find(match => match == false))
        {

        }


    }

}
