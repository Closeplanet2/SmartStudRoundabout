using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.ModernUIPack
{
    [ExecuteInEditMode]
    public class UIManagerInputField : MonoBehaviour
    {
        [Header("Settings")]
        public UIManager UIManagerAsset;
        public bool webglMode = false;

        [Header("Resources")]
        public List<GameObject> images = new List<GameObject>();
        public List<GameObject> texts = new List<GameObject>();

        [Header("Custom Settings")] 
        public bool overrideTextSize = true;
        public bool overrideTextFont = true;
        public bool overrideTextColor = true;
        public bool overrideInputFieldColor = true;

        [Header("Custom Override Variables")] 
        public TMP_FontAsset customTextFont;
        public float customTextSize;
        public Color customTextColor= new Color(255, 255, 255, 255);
        public Color customInputFieldColor = new Color(255, 255, 255, 255);

        void Awake()
        {
            if (Application.isPlaying && webglMode == true)
                return;

            try
            {
                if (UIManagerAsset == null)
                    UIManagerAsset = Resources.Load<UIManager>("MUIP Manager");

                this.enabled = true;

                if (UIManagerAsset.enableDynamicUpdate == false)
                {
                    UpdateInputField();
                    this.enabled = false;
                }
            }

            catch { Debug.Log("<b>[Modern UI Pack]</b> No UI Manager found, assign it manually.", this); }
        }

        void LateUpdate()
        {
            if (UIManagerAsset == null)
                return;

            if (UIManagerAsset.enableDynamicUpdate == true)
                UpdateInputField();
        }

        void UpdateInputField()
        {
            for (int i = 0; i < images.Count; ++i)
            {
                var currentImage = images[i].GetComponent<Image>();
                currentImage.color = overrideInputFieldColor ? UIManagerAsset.inputFieldColor : customInputFieldColor;
            }

            for (int i = 0; i < texts.Count; ++i)
            {
                var currentText = texts[i].GetComponent<TextMeshProUGUI>();
                currentText.color = overrideTextColor ? UIManagerAsset.inputFieldColor : customTextColor;
                currentText.font = overrideTextFont ? UIManagerAsset.inputFieldFont : customTextFont;
                currentText.fontSize = overrideTextSize ? UIManagerAsset.inputFieldFontSize : customTextSize;
            }
        }
    }
}