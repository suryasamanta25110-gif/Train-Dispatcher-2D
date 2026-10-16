using System;
using UnityEngine;

public class Coach : MonoBehaviour {
    [SerializeField] private Train train;
    [SerializeField] private int coachIndex;

    private Rigidbody2D rigidbodyComponent;
    private float speed;
    private float angle;

    private void Start() {
        angle = train.isPointingRight ? 0f : (float) Math.PI;
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.sleepMode = RigidbodySleepMode2D.NeverSleep;

        // Setting the position and initial velocity of the coaches.
        if (train.isPointingRight)
            rigidbodyComponent.position = new Vector2(-(50 + 2 * (coachIndex - 1)), 1);
        else
            rigidbodyComponent.position = new Vector2(50 + 2 * (coachIndex - 1), 1);
    }

    private void FixedUpdate() {
        speed = train.speed;
        rigidbodyComponent.linearVelocity = new Vector2(speed * (float) Math.Cos(angle), speed * (float) Math.Sin(angle));
        rigidbodyComponent.rotation = angle * 180 / (float) Math.PI;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        // Condition for colliding with track switches.
        if (collision.gameObject.layer == 11) {
            SwitchSensors trackSwitch = collision.GetComponent<SwitchSensors>();
            if (trackSwitch == null || train.isPointingRight != trackSwitch.isDirectingRight)
                return;
            
            if (trackSwitch.isDivergingRight)
                angle = train.isPointingRight ? trackSwitch.divergingAngle : (float) (Math.PI - trackSwitch.constantAngle);
            else
                angle = train.isPointingRight ? trackSwitch.constantAngle : (float) (Math.PI - trackSwitch.divergingAngle);
        }
    }
}
