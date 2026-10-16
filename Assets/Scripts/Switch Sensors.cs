using System;
using UnityEngine;

public class SwitchSensors : MonoBehaviour {
    [SerializeField] public Switch trackSwitch;
    [SerializeField] public bool isDirectingRight;

    public bool isStraight;
    public bool isDivergingRight;
    private bool isDivergingDown;

    public float constantAngle;
    public float divergingAngle;

    private void Start() {
        isStraight = true;
        isDivergingRight = trackSwitch.isDivergingRight;
        isDivergingDown = trackSwitch.isDivergingDown;

        constantAngle = 0f;
        divergingAngle = 0f;
    }

    private void FixedUpdate() {
        isStraight = trackSwitch.isStraight;
        divergingAngle = isStraight ? 0f : (isDivergingDown ? (float) (-45 * Math.PI / 180) : (float) (45  * Math.PI / 180));
    }
}
