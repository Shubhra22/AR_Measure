using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JoystickLab
{    
    public class DistanceButton : ButtonBehaviour
    {
        private void Start()
        {
            base.Start();
        }

        protected override void OnClickButton()
        {
            string output = GetComponentInChildren<TextMeshProUGUI>().text;

            LineRendererDrawing.Instance.ShowDistanceBox(output);
        }
    }
}

