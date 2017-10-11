using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_In_Square : MonoBehaviour
{
    public Image[] RS20_Rects, RS20S_Rects;
    public Vector3 mouse;
    public GameObject RS20_Img, RS20S_Img;
    public GameObject EscapeText;
    // Use this for initialization
    void Start ()
    {
        RS20_Img.SetActive(false);
        RS20S_Img.SetActive(false);
        EscapeText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        RS20Check();
        RS20SCheck();

        if(State_Manager.CurrentGlobalStates == States.SELECTIING ||
            State_Manager.CurrentGlobalStates == States.SPLITOVERVIEW ||
            State_Manager.CurrentGlobalStates == States.SPLITSELECTION)
        {
            EscapeText.SetActive(true);
        }
        else
        {
            EscapeText.SetActive(false);
        }
    }

    void RS20SCheck()
    {
        mouse =
            (
             new Vector3(Input.mousePosition.x,
             Input.mousePosition.y,
             2)
             );

        //Debug.Log(mouse);

        bool isActive = false;

        for (int i = 0; i < RS20S_Rects.Length; i++)
        {
            if (RS20S_Rects[i].gameObject.active)
            {
                float p1x = RS20S_Rects[i].rectTransform.rect.xMax * GetComponent<Canvas>().scaleFactor;
                float p1y = RS20S_Rects[i].rectTransform.rect.yMax * GetComponent<Canvas>().scaleFactor;
                float p2x = RS20S_Rects[i].rectTransform.rect.xMin * GetComponent<Canvas>().scaleFactor;
                float p2y = RS20S_Rects[i].rectTransform.rect.yMin * GetComponent<Canvas>().scaleFactor;


                Vector2 p1 = new Vector2(RS20S_Rects[i].rectTransform.position.x + p1x, RS20S_Rects[i].rectTransform.position.y + p1y);
                Vector2 p2 = new Vector2(RS20S_Rects[i].rectTransform.position.x + p2x, RS20S_Rects[i].rectTransform.position.y + p2y);

                if (g_utils.pointInRect(mouse.x, mouse.y, p1, p2))
                {
                    isActive = true;
                    break;
                }
                else
                {
                    isActive = false;
                }
            }
        }


        RS20S_Img.SetActive(isActive);
    }


    void RS20Check()
    {
        mouse =
            (
             new Vector3(Input.mousePosition.x,
             Input.mousePosition.y,
             2)
             );

        //Debug.Log(mouse);

        bool isActive = false;

        for (int i = 0; i < RS20_Rects.Length; i++)
        {
            if (RS20_Rects[i].transform.gameObject.active)
            {
                float p1x = RS20_Rects[i].rectTransform.rect.xMax * GetComponent<Canvas>().scaleFactor;
                float p1y = RS20_Rects[i].rectTransform.rect.yMax * GetComponent<Canvas>().scaleFactor;
                float p2x = RS20_Rects[i].rectTransform.rect.xMin * GetComponent<Canvas>().scaleFactor;
                float p2y = RS20_Rects[i].rectTransform.rect.yMin * GetComponent<Canvas>().scaleFactor;


                Vector2 p1 = new Vector2(RS20_Rects[i].rectTransform.position.x + p1x, RS20_Rects[i].rectTransform.position.y + p1y);
                Vector2 p2 = new Vector2(RS20_Rects[i].rectTransform.position.x + p2x, RS20_Rects[i].rectTransform.position.y + p2y);

                if (g_utils.pointInRect(mouse.x, mouse.y, p1, p2))
                {
                    isActive = true;
                    break;
                }
                else
                {
                    isActive = false;
                }
            }
        }


        RS20_Img.SetActive(isActive);
    }

    void OnDrawGizmos()
    {
       //float p1x = RS20_Rects[0].rectTransform.rect.xMax * GetComponent<Canvas>().scaleFactor;
       //float p1y = RS20_Rects[0].rectTransform.rect.yMax * GetComponent<Canvas>().scaleFactor;
       //
       //float p2x = RS20_Rects[0].rectTransform.rect.xMin * GetComponent<Canvas>().scaleFactor;
       //float p2y = RS20_Rects[0].rectTransform.rect.yMin * GetComponent<Canvas>().scaleFactor;
       //
       //Vector3 vec = RS20_Rects[0].rectTransform.position;
       //
       //Gizmos.color = Color.green;
       //Gizmos.DrawSphere(vec, 12f);
       //
       //Vector3 vec2 = new Vector3(RS20_Rects[0].rectTransform.position.x + p1x, RS20_Rects[0].rectTransform.position.y + p1y, 0);
       //
       //Vector3 vec3 = new Vector3(RS20_Rects[0].rectTransform.position.x + p2x, RS20_Rects[0].rectTransform.position.y + p2y, 0);
       //
       //
       //Gizmos.color = Color.red;
       //Gizmos.DrawSphere(vec3, 12f);
       //
       //Gizmos.color = Color.green;
       //Gizmos.DrawSphere(vec2, 12f);
    }
}
