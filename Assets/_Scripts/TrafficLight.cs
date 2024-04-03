using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Closeplanet.SmartStudStudy.TrafficLights {
    public class TrafficLight : MonoBehaviour {

        [Header("LEDS")] 
        public Image redLight;
        public Image amberLight;
        public Image greenLight;

        public void ChangeTrafficLightState(TrafficLightState trafficLightState) {
            redLight.color = trafficLightState == TrafficLightState.RED || trafficLightState == TrafficLightState.RED_AMBER
                ? Color.red
                : Color.white;
            amberLight.color = trafficLightState == TrafficLightState.AMBER || trafficLightState == TrafficLightState.RED_AMBER
                ? Color.yellow
                : Color.white;
            greenLight.color = trafficLightState == TrafficLightState.GREEN 
                ? Color.green :
                Color.white;
        }
    }

    public enum TrafficLightState {
        RED,
        RED_AMBER,
        GREEN,
        AMBER
    }
}


