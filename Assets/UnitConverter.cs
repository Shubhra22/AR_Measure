using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
namespace JoystickLab
{   
    public class UnitConverter : SingleToneManager<UnitConverter>
    {
        [HideInInspector]
        public string output;
        
        public TextMeshProUGUI resultText;
        
        // Start is called before the first frame update
        private void OnEnable()
        {
            resultText.text = output;
            transform.DOPunchScale(new Vector3(0.2f,0.2f,1f), 0.3f);
            
            //ApplyConversion();
        }


        void ApplyConversion()
        {                                   
//            if (output.Contains("cm"))
//            {
//                
//            }
        }
    }
}

