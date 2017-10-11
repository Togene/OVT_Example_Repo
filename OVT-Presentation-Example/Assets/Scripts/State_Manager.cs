using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { LOOKING, SELECTIING, GOINGBACK, GOINGIN,

    SPLITOVERVIEW, GOINGINSPLITOVERVIEW, GOINGINSPLITSELECTION, GOINGBACKSPLITSELECTION, SPLITSELECTION};

public class State_Manager : MonoBehaviour {

    public static States CurrentGlobalStates;
    public States currentStatesEditorView;

	// Use this for initialization
	void Start ()
    {
        CurrentGlobalStates = States.LOOKING;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentStatesEditorView = CurrentGlobalStates;
        //Normal Selection
        if(CurrentGlobalStates == States.SELECTIING)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentGlobalStates = States.GOINGBACK;
                Mouse_Controls_Select.CountryListNull();
            }
        }

        //Split Country Selection
        if (CurrentGlobalStates == States.SPLITOVERVIEW) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentGlobalStates = States.GOINGBACK;
                Mouse_Controls_Select.MasterCountry = null;
                Mouse_Controls_Select.CountryListNull();
            }
        }

        //Split Country INNER back to SplitOverView Selection
        if (CurrentGlobalStates == States.SPLITSELECTION)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentGlobalStates = States.GOINGBACKSPLITSELECTION;
            }
        }

        if(CurrentGlobalStates == States.LOOKING)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
