using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Controls_Select : MonoBehaviour
{
    public Color selectionColor;
    public Color isPointofIntrestColorSelect;
    public static Country MasterCountry;
    public Country MasterCountryEditorView;

    void Start ()
    {
        Planet_Manager.selectionColor = selectionColor;
        Planet_Manager.isPointofIntrestColorSelect = isPointofIntrestColorSelect;
    }
	
    void RayCastSelect()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            MatchToCountryList(hit.transform.gameObject);
        }
        else
        {
            CountryListNull();
        }
    }

    public static void CountryListNull()
    {
        for (int i = 0; i < Planet_Manager.Countries.Count; i++)
        {
            Planet_Manager.Countries[i].isSelected = false;
        }
    }

    void MatchToCountryList(GameObject hit)
    {
        if (hit.GetComponent<SubCountry>() != null)
        {
            for (int i = 0; i < Planet_Manager.Countries.Count; i++)
            {
                if (hit.GetComponent<SubCountry>().Parent == Planet_Manager.Countries[i].countryObject)
                {
                    Planet_Manager.Countries[i].isSelected = true;
                }
                else
                {
                    Planet_Manager.Countries[i].isSelected = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < Planet_Manager.Countries.Count; i++)
            {
                if (hit == Planet_Manager.Countries[i].countryObject)
                {
                    Planet_Manager.Countries[i].isSelected = true;
                }
                else
                {
                    Planet_Manager.Countries[i].isSelected = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        MasterCountryEditorView = MasterCountry;
        ManagerVeriableChanges();
        if (State_Manager.CurrentGlobalStates == States.LOOKING)
        {
            if (!Input.GetMouseButton(0))
            {
                RayCastSelect();
            }
            else
                CountryListNull();
        }
        else if (State_Manager.CurrentGlobalStates == States.SPLITOVERVIEW)
        {
            if (!Input.GetMouseButton(0))
            {
                RayCastSplitSelect();
            }
               
        }

    }

    void RayCastSplitSelect()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            MatchToCountryChildrenList(hit.transform.gameObject);
        }
        else
        {
           // CountryListNull();
        }
    }

    void MatchToCountryChildrenList(GameObject hit)
    {
        for (int i = 0; i < MasterCountry.childrenList.Length; i++)
        {
            if (hit == MasterCountry.childrenList[i])
            {
                MasterCountry.childrenList[i].GetComponent<SubCountry>().isSelected = true;
            }
            else
            {
                MasterCountry.childrenList[i].GetComponent<SubCountry>().isSelected = false;
            }
        }
    }
    void ManagerVeriableChanges()
    {
        if (Planet_Manager.selectionColor != selectionColor)
        {
            Planet_Manager.selectionColor = selectionColor;
        }

        if (Planet_Manager.isPointofIntrestColorSelect != isPointofIntrestColorSelect)
        {
            Planet_Manager.isPointofIntrestColorSelect = isPointofIntrestColorSelect;
        }
    }
}
