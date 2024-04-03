using System;
using System.Collections;
using System.Collections.Generic;
using Closeplanet.SmartStudStudy.TrafficStuds;
using Michsky.UI.ModernUIPack;
using UnityEngine;

namespace Closeplanet.SmartStudStudy.TrafficLights {
    public class TrafficLightController : MonoBehaviour {
        [Header("Traffic Light Groups")]
        public List<TrafficLightGroup> trafficLightGroups = new List<TrafficLightGroup>();

        [Header("Traffic Light Delay Settings")]
        public SliderManager trafficLightDelay;
        public SliderManager trafficLightGreenDelay;
        
        [Header("Traffic light studs settings")]
        public Color trafficStudsOn = Color.white;
        public Color trafficStudsOff = Color.white;

        private TrafficLightState currentTrafficLightState = TrafficLightState.RED;
        private int currentTrafficLightGroup = 0;
        private bool stopSim = false;

        private void Start() {
            foreach(var trafficLightGroup in trafficLightGroups) 
                trafficLightGroup.ChangeTrafficLightState(currentTrafficLightState, trafficStudsOn, trafficStudsOff);
        }

        public void StartSim() {
            stopSim = false;
            StartCoroutine(TrafficLightControlLoop());
        }

        public void CancelSim() {
            stopSim = true;
        }

        public void ToggleLed(int group, bool turnOn) {
            trafficLightGroups[group].SwitchTrafficStuds(turnOn ? trafficStudsOn : trafficStudsOff);
        }

        public void ResetLEDs(int group) {
            trafficLightGroups[group].SwitchTrafficStuds(Color.white);
        }

        private IEnumerator TrafficLightControlLoop() {
            switch (currentTrafficLightState) {
                case TrafficLightState.RED:
                    currentTrafficLightState = TrafficLightState.RED_AMBER;
                    trafficLightGroups[currentTrafficLightGroup].ChangeTrafficLightState(currentTrafficLightState, trafficStudsOn, trafficStudsOff);
                    break;
                case TrafficLightState.RED_AMBER:
                    currentTrafficLightState = TrafficLightState.GREEN;
                    trafficLightGroups[currentTrafficLightGroup].ChangeTrafficLightState(currentTrafficLightState, trafficStudsOn, trafficStudsOff);
                    break;
                case TrafficLightState.GREEN:
                    currentTrafficLightState = TrafficLightState.AMBER;
                    trafficLightGroups[currentTrafficLightGroup].ChangeTrafficLightState(currentTrafficLightState, trafficStudsOn, trafficStudsOff);
                    break;
                case TrafficLightState.AMBER:
                    currentTrafficLightState = TrafficLightState.RED;
                    trafficLightGroups[currentTrafficLightGroup].ChangeTrafficLightState(currentTrafficLightState, trafficStudsOn, trafficStudsOff);
                    NextTrafficLightGroup();
                    break;
            }
            yield return new WaitForSeconds(
                currentTrafficLightState == TrafficLightState.GREEN 
                ? trafficLightGreenDelay.mainSlider.value : trafficLightDelay.mainSlider.value
            );
            
            if (!stopSim) StartCoroutine(TrafficLightControlLoop());
        }

        private void NextTrafficLightGroup() {
            currentTrafficLightGroup += 1;
            if (currentTrafficLightGroup >= trafficLightGroups.Count) currentTrafficLightGroup = 0;
        }
    }
    
    [Serializable]
    public class TrafficLightGroup {
        public GameObject trafficLightGroup;
        public List<TrafficLightStud> trafficLightStuds;

        public void ChangeTrafficLightState(TrafficLightState trafficLightState, Color studsOn, Color studsOff) {
            foreach (var trafficLight in trafficLightGroup.GetComponentsInChildren<TrafficLight>()) {
                trafficLight.ChangeTrafficLightState(trafficLightState);
            }
            SwitchTrafficStuds(trafficLightState == TrafficLightState.RED ? studsOff : studsOn);
        }

        public void SwitchTrafficStuds(Color color) {
            foreach (var trafficStud in trafficLightStuds) {
                if(trafficStud != null) trafficStud.SwitchColor(color);
            }
        }
    }
}


