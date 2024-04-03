using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Closeplanet.SmartStudStudy.TrafficStuds {
    [RequireComponent(typeof(Image))]
    public class TrafficLightStud : MonoBehaviour {
        public void SwitchColor(Color color) {
            GetComponent<Image>().color = color;
        }
    }
}


