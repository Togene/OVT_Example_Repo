using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Target : MonoBehaviour
{
    public static Transform MoveTarget;
    public Transform moveTargetEditorView;

    public static Transform LookTarget;
    public Transform LookTargetEditorView;


    public static Vector3 CurrentCameraPosition, CurrentCameraParentMovePosition;
    public float transitionDuration;
    public float transitionThreshold;
    bool savedPosition;

    // Use this for initialization
    void Start ()
    {
        LookTarget = GameObject.Find("LookAtTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveTargetEditorView = MoveTarget;
        LookTargetEditorView = LookTarget;

        transform.LookAt(LookTarget);

        if (State_Manager.CurrentGlobalStates == States.GOINGIN || 
            State_Manager.CurrentGlobalStates == States.GOINGINSPLITOVERVIEW ||
            State_Manager.CurrentGlobalStates == States.GOINGINSPLITSELECTION)
        {
            LerpThisBitch();
        }
        else if (State_Manager.CurrentGlobalStates == States.GOINGBACK ||
            State_Manager.CurrentGlobalStates == States.GOINGBACKSPLITSELECTION)
        {
            LerpThisBitchBack();
        }
    }
    
    public void LerpThisBitch()
    {
        float t = 0.0f;

        Vector3 startingPos = transform.position;

        if (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            transform.position = Vector3.Lerp(startingPos, MoveTarget.position, t);

            if (Mathf.Abs((transform.position - MoveTarget.position).magnitude) < transitionThreshold)
            {
                transform.position = MoveTarget.position;

                if (State_Manager.CurrentGlobalStates == States.GOINGINSPLITOVERVIEW)
                    State_Manager.CurrentGlobalStates = States.SPLITOVERVIEW;
                else if (State_Manager.CurrentGlobalStates == States.GOINGINSPLITSELECTION)
                    State_Manager.CurrentGlobalStates = States.SPLITSELECTION;
                else
                    State_Manager.CurrentGlobalStates = States.SELECTIING;
            }
        }
    }

    public void LerpThisBitchBack()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
 
        if (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            if(State_Manager.CurrentGlobalStates != States.GOINGBACKSPLITSELECTION)
            {
                transform.position = Vector3.Lerp(startingPos, CurrentCameraPosition, t);

                if (Mathf.Abs((transform.position - CurrentCameraPosition).magnitude) < transitionThreshold)
                {
                    transform.position = CurrentCameraPosition;
                    State_Manager.CurrentGlobalStates = States.LOOKING;
                }

            }
            else
            {

                transform.position = Vector3.Lerp(startingPos, CurrentCameraParentMovePosition, t);

                if (Mathf.Abs((transform.position - CurrentCameraParentMovePosition).magnitude) < transitionThreshold)
                {
                    transform.position = CurrentCameraParentMovePosition;
                    State_Manager.CurrentGlobalStates = States.SPLITOVERVIEW;
                }
            }

            //if(Input.GetMouseButton(0))
            //{
            //    MouseOrbitImproved.distance = transform.position.magnitude;
            //    State_Manager.CurrentGlobalStates = States.LOOKING;
            //}
        }
    }

    public static void SavePosition()
    {
        Debug.Log("Saved Position");
        CurrentCameraPosition = Camera.main.transform.position;
    }

    public static void SaveToCameraTargetPosition(Vector3 pos)
    {
        Debug.Log("Saved Position");
        CurrentCameraParentMovePosition = pos;
    }
}
