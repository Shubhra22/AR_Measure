using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace JoystickLab
{ 
    public class Point : MonoBehaviour
    {
        public GameObject pointObj;
        public bool isFinal;
        
        Vector3 pos;

        

        private void OnTriggerEnter(Collider other)
        {
           if(other.transform.tag == "Circle")
           {
                Debug.Log("Circle Entered");
                pos = transform.position;
                other.transform.GetChild(0).DOMove(transform.position, 0.3f);
                other.transform.GetChild(0).GetComponent<SpriteRenderer>().DOColor(new Color(255 / 255, 62f / 255f, 134f / 255f, 255 / 255), 0.5f);
                //other.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 62, 134, 255);
                //other.transform.position = transform.position;
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.transform.tag == "Circle")
            {
                other.transform.GetChild(0).DOMove(transform.position, 0.3f);
                //other.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 62, 134, 255);
                other.transform.GetChild(0).GetComponent<SpriteRenderer>().DOColor(new Color(255 / 255, 62f / 255f, 134f / 255f, 255 / 255), 0.5f);
            }
        }


        private void OnTriggerExit(Collider other)
        {
           if(other.transform.tag == "Circle")
           {
                other.transform.GetChild(0).DOLocalMove(Vector3.zero, 0.3f);
                other.transform.GetChild(0).localPosition = Vector3.zero;
                other.transform.GetChild(0).GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.5f);

                //other.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 62, 134, 255);
           }
         }
    }
}

