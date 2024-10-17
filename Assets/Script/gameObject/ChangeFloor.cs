using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Media;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    [SerializeField]
    private float endTime = 3.0f;
    [SerializeField]
    private float linkEndTime = 1.0f;
    private float currentTime = 0.0f;
    private bool change = false; //床が変わる
    private bool linkChange = false;

    [SerializeField]
    private float Hight;
    [SerializeField]
    private string tagName1;
    [SerializeField]
    private string tagName2;
    [SerializeField]
    private Material mat_red;
    [SerializeField]
    private Material mat_blue;

    private bool cursor = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(change);
        //Debug.Log(currentTime);

        if (change == true)//床の色を変えるモーション
        {
            currentTime += Time.deltaTime;

            if (currentTime < 1.0f)
            {
                Vector3 transformPos = transform.position;
                transformPos.y += 0.01f;
                transform.position = transformPos;
            }
            else if (currentTime >= 1.0f && currentTime < 2.0f)
            {
                transform.Rotate(180.0f * Time.deltaTime, 0.0f, 0.0f);

                GetComponent<MeshRenderer>().material = mat_red;

            }
            else if (currentTime >= 2.0f && currentTime < endTime)
            {
                Vector3 transformPos = transform.position;
                transformPos.y -= 0.01f;
                transform.position = transformPos;
                
            }
            else if (currentTime >= endTime)
            {
                if (tag == tagName1)
                {
                    tag = tagName2;
                    GetComponent<MeshRenderer>().material = mat_blue;
              
                }
                else if (tag == tagName2)
                {
                    tag = tagName1;
                    GetComponent<MeshRenderer>().material = mat_red;
                    
                }
                
                change = false;
                //CursorManager.floorChange = false;

            }
        }
        else if (change == false)//カーソルが当たっている時のモーション
        {
            if (cursor == true)
            {
                Vector3 pos = transform.position;
                pos.y = 1.0f;//マジックナンバーごめん
                transform.position = pos;
                cursor = false;
                return;
            }
            else if (cursor == false)
            {
                Vector3 pos = transform.position;
                pos.y = 0.0f;//マジックナンバーごめん
                transform.position = pos;
            }
        }

        if(linkChange == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 0.0f && currentTime < linkEndTime)
            {
                transform.Rotate(180.0f * Time.deltaTime, 0.0f, 0.0f);

                GetComponent<MeshRenderer>().material = mat_red;

            }
            else if (currentTime >= linkEndTime)
            {
                if (tag == tagName1)
                {
                    tag = tagName2;
                    GetComponent<MeshRenderer>().material = mat_blue;

                }
                else if (tag == tagName2)
                {
                    tag = tagName1;
                    GetComponent<MeshRenderer>().material = mat_red;

                }
                linkChange = false;
              
            }


        }


    }

    public void LinkChange()
    {
        linkChange = true;
    }

    public void OnChange()
    {
        change = true;
        currentTime = 0.0f;

    }
   public  bool GetChangeState()
    {
        return change;
    }
    public void OnCursor()
    {
        cursor = true;
    }
}
