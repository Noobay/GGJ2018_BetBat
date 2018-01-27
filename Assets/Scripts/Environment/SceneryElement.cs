using UnityEngine;

public class SceneryElement : MonoBehaviour {
    
    [SerializeField]
    private Vector3 _fallVector = Vector3.one;
    [SerializeField]
    private float _timeToLive = 10f;

    private float _startTime = 0;

    private void Start()
    {
        _startTime = Time.time;
    }
    // Update is called once per frame
    void FixedUpdate() {
        transform.Translate(_fallVector);

        if ((Time.time - _startTime) > _timeToLive) {
            Destroy(gameObject);
        } 
    }
}
