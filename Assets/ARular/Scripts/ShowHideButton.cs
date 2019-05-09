using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickLab
{
    public class ShowHideButton : ButtonBehaviour
    {
        public GameObject floorGrid;
    
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
        
        }

        protected override void OnClickButton()
        {
            base.OnClickButton();
            floorGrid.SetActive(doShow);        
        }
    }

}
