using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    [SerializeField]
    private GameObject _mapObject;

    [SerializeField]
    private float scrollSpeed = 0.5f;

/*        float mapScreenY = Camera.main.WorldToScreenPoint(_mapObject.transform.position).y;
Vector3 flatScreenEdgePoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mapScreenY));

Vector3 objectBounds = _mapObject.GetComponent<Renderer>().bounds.size;

float width_percentage  = (Screen.width - objectBounds.x)/ objectBounds.x;
float height_percentage = (Screen.height - objectBounds.z)/  objectBounds.z;

Debug.Log(width_percentage + " X " + height_percentage);
_mapObject.transform.localScale = new Vector3(_mapObject.transform.localScale.x * (width_percentage), 1f, _mapObject.transform.localScale.z * height_percentage);
*/
    private void Start()
    {
        _mapObject.transform.localScale = new Vector3(Camera.main.orthographicSize * 2.0f * (Screen.width / Screen.height), 1f, Camera.main.orthographicSize * 2.0f);
    }


    public void FixedUpdate()
    {
        _mapObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(1, Time.time * scrollSpeed));
    }
}
