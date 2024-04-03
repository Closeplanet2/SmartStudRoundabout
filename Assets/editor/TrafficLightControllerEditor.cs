using System.Collections;
using System.Collections.Generic;
using Closeplanet.SmartStudStudy.TrafficLights;
using UnityEditor;
using UnityEngine;

namespace Closeplanet.SmartStudStudy.Editor {
    [CustomEditor(typeof(TrafficLightController))]
    public class TrafficLightControllerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var myScript = (TrafficLightController)target;

            if (GUILayout.Button("Turn Off")) {
                myScript.ToggleLed(5, false);
            }
            
            if (GUILayout.Button("Turn On")) {
                myScript.ToggleLed(5, true);
            }
            
            if (GUILayout.Button("Reset")) {
                myScript.ResetLEDs(5);
            }
        }
    }
}


