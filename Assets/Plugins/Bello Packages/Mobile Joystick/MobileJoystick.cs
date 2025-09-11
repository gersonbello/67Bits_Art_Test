using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MobileJoystick : MonoBehaviour
{
    private static Vector2 startPoint;
    private static Vector2 currentPoint;

    [SerializeField] private float maxHandleDistance;
    private static float _maxHandleDistance;
    [SerializeField] private RectTransform joystickBKG;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private RectTransform joystickArea;

    private static float currentDistance;

    private void Awake()
    {
#if !UNITY_EDITOR
gameObject.SetActive(Application.isMobilePlatform);
#endif
        _maxHandleDistance = maxHandleDistance;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                joystickHandle.gameObject.SetActive(true);
                joystickBKG.gameObject.SetActive(true);
                startPoint = Input.GetTouch(0).position;
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, startPoint) && !IsPointerOverUIObject(startPoint))
            {

                currentPoint = Input.GetTouch(0).position;

                currentDistance = Mathf.Min(Vector2.Distance(startPoint, currentPoint), maxHandleDistance);

                joystickBKG.position = startPoint;
                joystickHandle.position = startPoint + (GetJoystickAxis() * currentDistance);
            }
            else currentDistance = 0;

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                currentPoint = Vector2.zero;
                currentDistance = 0;
                joystickBKG.localPosition = Vector2.zero;
                joystickHandle.localPosition = Vector2.zero;
                joystickHandle.gameObject.SetActive(false);
                joystickBKG.gameObject.SetActive(false);
            }
        }
    }
    public static Vector2 GetJoystickAxis()
    {
        if (Input.touchCount > 0) return (currentPoint - startPoint).normalized * currentDistance/_maxHandleDistance;
        else return Vector2.zero;
    }
    private bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        int resultsCount = 0;
        foreach(RaycastResult rR in results)
        {
            if (rR.gameObject != joystickArea.gameObject) resultsCount++;
        }
        return resultsCount > 0;
    }
}
