using UnityEngine;

public class FlockCommanderController : MonoBehaviour {

    [SerializeField]
    private float startOffset;

    [SerializeField]
    private float slideToOffset;

    [SerializeField]
    private float introTime;

    private Vector3 _screenEdge;
    private float _startTime;

    private void Awake()
    {
        float screenDepth = Camera.main.WorldToScreenPoint(transform.position).z;
    }

    // Use this for initialization
    void Start () {
        _startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float completeness = (Time.time - _startTime) * introTime;
        float fracComplete = completeness / introTime;

        float smoothedOffset = Mathf.Lerp(startOffset, startOffset + slideToOffset, Mathf.Min(fracComplete, 1));
        transform.position = new Vector3(transform.position.x, (transform.position.y) + smoothedOffset, transform.position.z);
    }
}
