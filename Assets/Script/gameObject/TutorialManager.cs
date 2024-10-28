using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private StageScriptableObject scriptableObject = null;
    private GameObject MapObject = null;
    private bool doOnce = false;
    private bool tutorialClear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(doOnce == false)
        {
            MapObject = GameObject.Find("map(Clone)");
            if(MapObject != null)
            {
                doOnce = true;
            }
        }

        if (MapObject != null)
        {
            WaitControl();
        }
    }

    void WaitControl()
    {
        if(scriptableObject.textIndex == 1)
        {
            MapObject.GetComponent<CursorManager>().enabled = true;
            MapObject.GetComponent<TouchControl>().enabled = true;

            foreach(GameObject obj in MapObject.GetComponent<MapManager>().GetGameObjectList())
            {
                if(obj.GetComponent<Floor>().GetMapPosition() == new Vector3(2.0f,0.0f,2.0f))
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            
        }
        else if(scriptableObject.textIndex >= 7)
        {
            if (tutorialClear == false)
            {

                MapObject.GetComponent<CursorManager>().enabled = true;
                MapObject.GetComponent<TouchControl>().enabled = true;

                foreach (GameObject obj in MapObject.GetComponent<MapManager>().GetGameObjectList())
                {

                    obj.GetComponent<MeshRenderer>().material.color = Color.white;

                }

                tutorialClear = true;
            }

        }
        else
        {
            MapObject.GetComponent<CursorManager>().enabled = false;
            MapObject.GetComponent<TouchControl>().enabled = false;
            foreach (GameObject obj in MapObject.GetComponent<MapManager>().GetGameObjectList())
            {
                obj.GetComponent<MeshRenderer>().material.color = Color.white * 0.6f;
            }
        }
    }
}
