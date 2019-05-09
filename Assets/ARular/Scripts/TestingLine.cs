using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// Just a test class for testing the line calculations in the unity editor    1
namespace JoystickLab
{   
    public class TestingLine : MonoBehaviour
    {

        public GameObject pointObject;
        public GameObject lineRenderDrawing;
        public GameObject meshDrawing;
        private RaycastHit hit;
        public float pointsize = 0.5f;

        public GameObject markerPoint;

        private bool exit = false;
    
        public enum RenderType
        {
            LineRender,
            MeshRender,
            GL,
        }

        public RenderType renderType;
    
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) exit = !exit;

            if(IsPointerOverUIObject()) return;
            if (!exit)
            {
                CalculateMarker();
            
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject point = Instantiate(pointObject, hit.point, Quaternion.identity);
                    point.transform.localScale = new Vector3(pointsize, pointsize, pointsize);
                    lineRenderDrawing.GetComponent<LineRendererDrawing>().DrawLine(point, true);
                }
            }

            if (Input.GetKeyDown("z"))

            {
                LineRendererDrawing.Instance.Undo();
            }
            
        }
        
        void CalculateMarker()
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                
                if (hit.transform.name == "Plane")
                {
                    Vector3 targetPos = hit.point;
                    //targetPos.y += 1;
                    markerPoint.transform.position = targetPos;
                    lineRenderDrawing.GetComponent<LineRendererDrawing>().DrawLine(markerPoint, false);  
                }
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
        
        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }

}
