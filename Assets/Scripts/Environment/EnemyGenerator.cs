using UnityEngine;

[System.Serializable]
public class EnemyGenerator : BaseSceneGenerator {
    [SerializeField]
    private GameObject enemyObject;

    [SerializeField]
    private float _generationRate;
    [SerializeField]
    private float _generationTimeSeconds;

    [SerializeField]
    private float _spawnYRange;

    private float _startTime;

    private static float _screenTopRightEdgeSpawnX = -1;
    private static float _screenTopLeftEdgeSpawnX = -1;
    private static float _topSpawnY = -1;

    private Vector3 _enemyBoundsSize;

    public override void Init()
    {
        if (_screenTopRightEdgeSpawnX == -1 || _screenTopLeftEdgeSpawnX == -1 || _topSpawnY == -1)
        {
            _enemyBoundsSize = enemyObject.GetComponent<Renderer>().bounds.size;
            Vector3 screenTopRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 1.0f));
            Vector3 screenTopLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 1.0f));

            _screenTopLeftEdgeSpawnX = screenTopLeftEdge.x + _enemyBoundsSize.x / 2;
            _screenTopRightEdgeSpawnX = screenTopRightEdge.x - _enemyBoundsSize.x / 2;
            _topSpawnY = screenTopRightEdge.y - _enemyBoundsSize.y / 2;
        }

        _startTime = Time.time;

        _generateEnemyAtRandomSpawn();

    }
	
	// Update is called once per frame
	public override void Update () {
        if ((Time.time - _startTime) >= (_generationTimeSeconds) * Random.Range(0.6f, 1f))
        {
            for (short i = 0; i < _generationRate; ++i)
            {
                _generateEnemyAtRandomSpawn();
            }
            _startTime = Time.time;
        }
    }

    private void _generateEnemyAtRandomSpawn()
    {
        float spawnX = Random.Range(_screenTopLeftEdgeSpawnX, _screenTopRightEdgeSpawnX);
        float spawnY = Random.Range(_topSpawnY, _topSpawnY - _spawnYRange);
        Object.Instantiate(enemyObject, new Vector3(spawnX, spawnY, 1f), enemyObject.transform.rotation);
    }
}
