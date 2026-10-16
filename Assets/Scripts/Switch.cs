using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField] private GameObject constant;
    [SerializeField] private GameObject straight;
    [SerializeField] private GameObject diverted;

    [SerializeField] public bool isDivergingRight;
    [SerializeField] public bool isDivergingDown;
    
    public bool isStraight;

    private void Start() {
        isStraight = true;
        
        constant.gameObject.SetActive(true);
        straight.gameObject.SetActive(true);
        diverted.gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        if (isStraight) {
            straight.gameObject.SetActive(true);
            diverted.gameObject.SetActive(false);
        } else {
            straight.gameObject.SetActive(false);
            diverted.gameObject.SetActive(true);
        }
    }

    private void OnMouseDown() {
        isStraight = !isStraight;
        Debug.Log("Switch changed: "+ (isStraight ? "Straightened" : "Diverted"));
    }
}
