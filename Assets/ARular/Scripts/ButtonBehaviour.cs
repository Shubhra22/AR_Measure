using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickLab
{
    public class ButtonBehaviour : MonoBehaviour
    {
        private Button button;
        private Image buttonImage;

        protected bool doShow = true;
    
        public Sprite show;
        public Sprite hide;
        // Start is called before the first frame update
        protected void Start()
        {
            button = GetComponent<Button>();
            buttonImage = GetComponent<Image>();
            button.onClick.AddListener(OnClickButton);
        }

        protected virtual void OnClickButton()
        {
            doShow = !doShow;
            if (doShow)
            {
                buttonImage.sprite = hide;
            }
            else
            {
                buttonImage.sprite = show;
            }
        }
    }

}
