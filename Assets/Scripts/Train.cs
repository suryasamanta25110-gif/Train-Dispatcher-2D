using UnityEngine;

public class Train : MonoBehaviour {
    [SerializeField] private bool isPointingRight;

    private Rigidbody2D rigidbodyComponent;

    private float maxSpeed = 3f;
    private float acceleration = 1.5f;
    private float arrivalTime = 0f;
    private float departureTime = 0f;
    private float currentTime = 0f;

    private bool shouldStopSignal;
    private bool shouldStopStation;

    private void Start() {
        shouldStopSignal = false;
        shouldStopStation = false;
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.sleepMode = RigidbodySleepMode2D.NeverSleep;

        Debug.Log("Game started.");

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
        currentTime = Time.time;

        // Managing the train's movement.
        float targetSpeed = shouldStopSignal || shouldStopStation ? 0f : (isPointingRight ? maxSpeed : -maxSpeed);
        float currentSpeed = rigidbodyComponent.linearVelocity.x;
        float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        rigidbodyComponent.linearVelocity = new Vector2(newSpeed, 0f);

        if (currentTime >= departureTime)
            shouldStopStation = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        // Conditions for colliding with station.
        if (collision.gameObject.layer == 10) {
            Platform platform = collision.GetComponent<Platform>();
            if (platform == null || isPointingRight != platform.forRight)
                return;
            
            shouldStopStation = true;
            departureTime = Mathf.Max(departureTime, currentTime + 5f);
            Debug.Log("Station detected. Train is stopping at station.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

        // Conditions for colliding with signal.
        if (collision.gameObject.layer == 6) {
            Signal signal = collision.GetComponent<Signal>();
            if (signal == null)
                return;
            
            bool sameDirection = (isPointingRight && signal.isPointingRight) || (!isPointingRight && !signal.isPointingRight);
            shouldStopSignal = sameDirection && !signal.isGreen;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 6)
            shouldStopSignal = false;
        
        if (collision.gameObject.layer == 10)
            shouldStopStation = false;
    }
}