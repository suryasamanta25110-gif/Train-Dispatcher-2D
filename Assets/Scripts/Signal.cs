using UnityEngine;

public class Signal : MonoBehaviour {
    [SerializeField] public bool isPointingRight;

    public bool isGreen;
    private SpriteRenderer rendererComponent;

    private void Start() {
        isGreen = false;
        rendererComponent = GetComponent<SpriteRenderer>();
        rendererComponent.color = Color.red;
    }

    private void FixedUpdate() {
        rendererComponent.color = isGreen ? Color.green : Color.red;
    }

    private void OnMouseDown() {
        isGreen = !isGreen;
        Debug.Log("Signal state changed: " + (isGreen ? "Green" : "Red"));
    }
}
