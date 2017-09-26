using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Country
{
    public bool isSelected, isPointofIntrest, isLinked;
    public GameObject countryObject;
    public Color countryColor;
    public Material mat;
    public GameObject OverViewUI, SelectedViewUI, CameraMoveTarget;
    public GameObject[] childrenList;

    public Country(GameObject _country, Color _color, bool _isPoI)
    {
        countryObject = _country;
        SetCountryColor(_color);
        isPointofIntrest = _isPoI;
        mat = countryObject.GetComponent<MeshRenderer>().material;
    }

    public void SetAllToSelected(bool status)
    {
        isSelected = true;
    }

    public void setChildList(GameObject[] children, Color subSelectionColor)
    {
        childrenList = children;

        for(int i = 0; i < childrenList.Length; i++)
        {
            childrenList[i].gameObject.GetComponent<SubCountry>().isLinked = true;
            childrenList[i].gameObject.GetComponent<SubCountry>().Parent = countryObject;
            childrenList[i].gameObject.GetComponent<SubCountry>().selectionColor = subSelectionColor;
        }
    }

    public void ManageChildren()
    {
        for (int i = 0; i < childrenList.Length; i++)
        {

                if (childrenList[i].gameObject.GetComponent<SubCountry>().isSelected == true)
                {
                    childrenList[i].gameObject.GetComponent<MeshRenderer>().material.color =
                        childrenList[i].gameObject.GetComponent<SubCountry>().selectionColor;

                if (State_Manager.CurrentGlobalStates != States.SPLITSELECTION)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        State_Manager.CurrentGlobalStates = States.GOINGINSPLITSELECTION;
                        Movement_Target.MoveTarget =
                            childrenList[i].gameObject.GetComponent<SubCountry>().CameraMovePoint.transform;
                        Movement_Target.SaveToCameraTargetPosition(CameraMoveTarget.transform.position);
                    }
                }

                    if (State_Manager.CurrentGlobalStates == States.SPLITSELECTION)
                        childrenList[i].gameObject.GetComponent<SubCountry>().UI.SetActive(true);
                    else
                        childrenList[i].gameObject.GetComponent<SubCountry>().UI.SetActive(false);
                
            }
            else
            {
                childrenList[i].gameObject.GetComponent<MeshRenderer>().material.color =
                     childrenList[i].gameObject.GetComponent<SubCountry>().overViewColor;


                childrenList[i].gameObject.GetComponent<SubCountry>().UI.SetActive(false);
            }
            
        }
    }

    public void setChildOverViewColor()
    {
        for (int i = 0; i < childrenList.Length; i++)
        {
            childrenList[i].GetComponent<MeshRenderer>().material.color =
                 childrenList[i].gameObject.GetComponent<SubCountry>().overViewColor;
        }
    }

    public void setChildColor(Color _color)
    {
        for(int i = 0; i < childrenList.Length; i++)
        {
            childrenList[i].GetComponent<MeshRenderer>().material.color = _color;
        }
    }

    public void setUI(GameObject _Oui, GameObject _Sui)
    {
        OverViewUI = _Oui;
        SelectedViewUI = _Sui;
    }

    public void ManageOverViewState(bool status)
    {
        if (status)
        {
            if (State_Manager.CurrentGlobalStates == States.LOOKING &&
                State_Manager.CurrentGlobalStates != States.SELECTIING)
            {
                OverViewUI.SetActive(status);
            }
            else
            {
                OverViewUI.SetActive(false);
            }


            if(State_Manager.CurrentGlobalStates == States.SELECTIING &&
                State_Manager.CurrentGlobalStates != States.LOOKING)
            {
                SelectedViewUI.SetActive(status);
            }
            else
            {
                SelectedViewUI.SetActive(false);
            }
            
        }
        else
        {
            OverViewUI.SetActive(status);
            SelectedViewUI.SetActive(status);
        }
    }

    void SetCountryColor(Color color)
    {
        countryColor = color;
    }
}

public class Planet_Manager : MonoBehaviour
{
    public GameObject[] CountriesObjects;
    public static List<Country> Countries = new List<Country>();
    public int index;
    public static Color selectionColor, isPointofIntrestColorSelect;
    public Color subSelectionColor;

    public Transform[] ActiveAreasCameraPositions;
    public GameObject[] UIOverViewList;
    public GameObject[] UISelectionist;
    public List<Country> PointsOfIntrest = new List<Country>();
    public Color POIColor;

    void Start ()
    {
		for(int i = 0; i < CountriesObjects.Length; i++)
        {
            if (CountriesObjects[i].transform.childCount == 0)
                CreateCountryObject(CountriesObjects[i], Color.white, false);
            else
                CreateCountryObject(CountriesObjects[i], POIColor, true);
        }

        AddInfo();

    }
	
    void CreateCountryObject(GameObject country, Color color, bool poi)
    {
        Country newCountry = new Country(country, color, poi);

        Countries.Add(newCountry);

        if (poi)
        {
            newCountry.CameraMoveTarget = country.transform.GetChild(0).gameObject;

            if (country.transform.childCount > 1)
            {
                List<GameObject> children = new List<GameObject>();

                for(int i = 0; i < country.transform.childCount; i++)
                {
                    if (country.transform.GetChild(i).name == "Camera_Point")
                        continue;
                    else
                        children.Add(country.transform.GetChild(i).gameObject);
                }
                newCountry.isLinked = true;
                newCountry.setChildList(children.ToArray(), subSelectionColor);
                newCountry.setChildColor(isPointofIntrestColorSelect);
                PointsOfIntrest.Add(newCountry);
            }
            else
                PointsOfIntrest.Add(newCountry);

        }
    }

    void AddInfo()
    {
        for (int i = 0; i < PointsOfIntrest.Count; i++)
        {
            PointsOfIntrest[i].setUI(UIOverViewList[i], UISelectionist[i]);
        }
    }

	void Update ()
    {
        ManageCountries();
    }

    void ManageCountries()
    {
        for (int i = 0; i < CountriesObjects.Length; i++)
        {
            CheckIfSelected(i);
            CheckIfMaster(i);
        }
    }


    void CheckIfMaster(int index)
    {
        if(Countries[index] == Mouse_Controls_Select.MasterCountry)
        {
            Countries[index].ManageChildren();
        }
    }

    void CheckIfSelected(int index)
    {
        if (Countries[index].isSelected)
        {
            if (Countries[index].isPointofIntrest)
            {
                Countries[index].ManageOverViewState(true);

                if (State_Manager.CurrentGlobalStates == States.LOOKING)
                {
                
                    if (Input.GetMouseButton(0))
                    {
                        //Going to Normal Select
                        if (!Countries[index].isLinked)
                        {
                            State_Manager.CurrentGlobalStates = States.GOINGIN;
                            Movement_Target.MoveTarget =
                                Countries[index].CameraMoveTarget.transform;
                            Movement_Target.SavePosition();
                        }
                        //Going to Split Select
                        else
                        {
                            Mouse_Controls_Select.MasterCountry = Countries[index];
                            //Countries[index].isSelected = false;
                            State_Manager.CurrentGlobalStates = States.GOINGINSPLITOVERVIEW;
                            Movement_Target.MoveTarget =
                                Countries[index].CameraMoveTarget.transform;
                            Movement_Target.SavePosition();
                        }
                    }
                }

                Countries[index].mat.color = isPointofIntrestColorSelect;

                if (Countries[index].isLinked)
                {
                    Countries[index].setChildOverViewColor();
                }
            }
            else
            {
                Countries[index].mat.color = selectionColor;
            }
        }
        else
        {
            if (Countries[index].isPointofIntrest)
            {
                Countries[index].ManageOverViewState(false);

                if (Countries[index].isLinked)
                {
                    Countries[index].setChildColor(Countries[index].countryColor);
                }
            }
            Countries[index].mat.color = Countries[index].countryColor;
        }
    }
}
