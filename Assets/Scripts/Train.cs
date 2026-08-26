using UnityEngine;

public class Train : MonoBehaviour {
    [SerializeField] private bool isPointingRight;

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

    private void Update() { }

    private void FixedUpdate() {

        // Managing the train's movement based on signals.
        if (shouldStop) { 
            if (rigidbodyComponent.linearVelocity.magnitude > 0.1f)
                rigidbodyComponent.AddForce((isPointingRight ? Vector2.left : Vector2.right) * 2);
            else
                rigidbodyComponent.linearVelocity = Vector2.zero;
        } else {
            if (rigidbodyComponent.linearVelocity.magnitude < 2.9f)
                rigidbodyComponent.AddForce((isPointingRight ? Vector2.right : Vector2.left) * 2);
            else
                rigidbodyComponent.linearVelocity = new Vector2(isPointingRight ? 3 : -3, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

        // Conditions for colliding with signal.
        if (collision.gameObject.layer == 6) {
            Signal signal = collision.GetComponent<Signal>();
            bool sameDirection = (isPointingRight && signal.isPointingRight) || (!isPointingRight && !signal.isPointingRight);
            shouldStop = sameDirection && !signal.isGreen;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 6)
            shouldStop = false;
    }
}