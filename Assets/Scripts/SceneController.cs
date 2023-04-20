using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SceneController : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] GameObject indicator, motorPrefab;
    public static SceneController instance;
    GameObject motor;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    IEnumerator Start()
    {
        while(motor == null)
        {
            if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes))
            {
                if (!indicator.activeInHierarchy)
                    indicator.SetActive(true);
                indicator.transform.position = hits[0].pose.position;
            }
            if (Input.touchCount > 0)
            {
                motor = Instantiate(motorPrefab, hits[0].pose.position, hits[0].pose.rotation);
                Destroy(indicator);
                hits.Clear();
                GC.Collect();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
