using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using GoogleARCore;
using UnityEngine.EventSystems;

namespace JoystickLab
{
     public class ARController : MonoBehaviour
    {

        public GameObject dotPoint; // object representing the point
        public GameObject markerPoint; // The placement marker Object
    

        private TrackableHit hit; // gives the hit info when ray hits trackables

        private Camera fpsCamera;// AR Camera

        private Touch touch; 

        private Pose placementPose; //Placement markers pose

        // Start is called before the first frame update
        void Start()
        {
            fpsCamera = Camera.main; // Initilize the camera in start to minimize "Camera.main" call multiple time inside code
        }

        // Update is called once per frame
        void Update()
        {   
            CalculateMarker(); // Continously calculates the placement marker Pos
        
            touch = Input.GetTouch(0); 
            
            
            if(Input.touchCount<0  || touch.phase!=TouchPhase.Began)
                return;
            
            if(IsPointerOverUIObject(touch)) return; // If we have touched on top of a UI element, then we just return

            //Otherwise create a point and pass that point to drawline. 
            GameObject point = Instantiate(dotPoint, placementPose.position, Quaternion.identity);
            LineRendererDrawing.Instance.DrawLine(point,true); // The passed argument is true, because we touch here. That means it completes a line/point
        }

        void CalculateMarker()
        {
            TrackableHitFlags flags = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

            // The idea is to throw a ray from the center of the screen.
            // So take the center of screen in screen point co ordinate.
            Vector3 origin = fpsCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

            //if the ray thrown from the center of the screen hit any trackables
            if(Frame.Raycast(origin.x,origin.y,flags,out hit))
            {
                // We get the plane that was hit and change the rotation of our marker point according to the plane type.
                IntPtr plane = hit.Trackable.m_TrackableNativeHandle;
                if (hit.Trackable.m_NativeSession.PlaneApi.GetPlaneType(plane) == DetectedPlaneType.Vertical)
                {
                    markerPoint.transform.eulerAngles = new Vector3(0,0,0);
                }
                else
                {
                    markerPoint.transform.eulerAngles = new Vector3(90,0,0);
                }
                
                //change markerpoint position to the hit
                markerPoint.transform.position = hit.Pose.position;
                placementPose = hit.Pose;

                //Taking the center point object of the marker point. This is not important in this moment.
                //But might come handy when we do some animation like "Apple Measure" app
                GameObject markerPointObj = markerPoint.transform.GetChild(0).gameObject;               
                // The passed argument is false, because we touch here. That means it just temporary dotted line.
                LineRendererDrawing.Instance.DrawLine(markerPointObj,false);
            }
        
        
        }
        
        private bool IsPointerOverUIObject(Touch touch)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

    }

}
