using UnityEngine;

[System.Serializable]
public class SceneryGenerator : BaseSceneGenerator {

    [SerializeField]
    private GameObject scenereyObject;

    [SerializeField]
    private bool snapToScreenEdges = false;
    [SerializeField]
    private bool alignedToRight = false;
    [SerializeField]
    private bool flip = false;
    [SerializeField]
    private bool spread = false;

    [SerializeField]
    private float generationTimeSeconds = 1f;
    [SerializeField]
    private int generationRate = 1;

    private float _startTime;

    private Vector3 _screenTopRightEdge;
    private Vector3 _screenTopLeftEdge;
    private float _screenBottomY;


    public override void Init()
    {
        _startTime = Time.time;
        _screenTopRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 1.0f));
        _screenTopLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 1.0f));
        _screenBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y;
    }

    public override void Update () {

        if ((Time.time - _startTime) >= (generationTimeSeconds) * Random.Range(0.6f,1f))
        { 
            for (short i = 0; i < generationRate; ++i)
            {
                _generateScenery();
            }
            _startTime = Time.time;
        }
	}

    private void _generateScenery()
    {
        Vector3 screenLocation = Vector3.zero;
        Quaternion rotation = Quaternion.identity;

        if (flip)
        {
            rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (snapToScreenEdges)
        {
            if (spread)
            {
                screenLocation += new Vector3(0f, Random.Range(_screenBottomY + ((_screenTopLeftEdge.y - _screenBottomY) / 2), _screenTopLeftEdge.y), 0f);
            }
            else
            {
                screenLocation += new Vector3(0f, _screenTopLeftEdge.y, 0f);
            }

            Vector3 objectBoundsSize = scenereyObject.GetComponent<Renderer>().bounds.size;

            if (alignedToRight)
            {
                screenLocation += new Vector3(_screenTopRightEdge.x, 0f, 0f) + new Vector3(scenereyObject.transform.position.x - (objectBoundsSize.x / 2.4f), 0f, 0f);
            }
            else
            {
                screenLocation += new Vector3(_screenTopLeftEdge.x, 0f, 0f) + new Vector3(scenereyObject.transform.position.x + (objectBoundsSize.x / 2.4f), 0f, 0f); 
            }
        } else
        {
            screenLocation = _screenTopRightEdge / 2f;
        }

        GameObject tempObject = Object.Instantiate(scenereyObject, screenLocation, rotation);

    }
}
