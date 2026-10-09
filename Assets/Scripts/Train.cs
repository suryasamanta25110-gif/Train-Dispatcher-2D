using UnityEngine;

public class Train : MonoBehaviour {
    [SerializeField] private bool isPointingRight;

    private float maxSpeed = 3f;
    private float acceleration = 1.5f;

    private bool shouldStop;
    private Rigidbody2D rigidbodyComponent;

    private void Start() {
        shouldStop = false;
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        Debug.Log("Game started.");

        rigidbodyComponent.sleepMode = RigidbodySleepMode2D.NeverSleep;

        // Setting the position and initial velocity of the train.
        if (isPointingRight) {
            rigidbodyComponent.position = new Vector2(-60, 1);
            rigidbodyComponent.linearVelocity = new Vector2(3, 0);
        } else {
            rigidbodyComponent.position = new Vector2(60, 1);
            rigidbodyComponent.rotation = 180;
            rigidbodyComponent.linearVelocity = new Vector2(-3, 0);
        }
    }

    private void FixedUpdate() {

        // Managing the train's movement based on signals.
        float targetSpeed = shouldStop ? 0f : (isPointingRight ? maxSpeed : -maxSpeed);
        float currentSpeed = rigidbodyComponent.linearVelocity.x;
        float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        rigidbodyComponent.linearVelocity = new Vector2(newSpeed, 0f);
    }

    private void OnTriggerStay2D(Collider2D collision) {

        // Conditions for colliding with signal.
        if (collision.gameObject.layer == 6) {
            Signal signal = collision.GetComponent<Signal>();
            if (signal == null)
                return;
            bool sameDirection = (isPointingRight && signal.isPointingRight) || (!isPointingRight && !signal.isPointingRight);
            shouldStop = sameDirection && !signal.isGreen;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 6)
            shouldStop = false;
    }
}