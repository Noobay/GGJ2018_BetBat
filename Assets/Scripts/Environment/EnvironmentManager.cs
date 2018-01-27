using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    private readonly int GENERATORS_ARRAYS_COUNT = 2;

    [SerializeField]
    private SceneryGenerator[] _sceneryGenerators;

    [SerializeField]
    private EnemyGenerator[] _enemyGenerators;

    private BaseSceneGenerator[] _sceneGenerators;

    private void Awake()
    {
        _sceneGenerators = new BaseSceneGenerator[_enemyGenerators.Length + _sceneryGenerators.Length - 2];


        _sceneryGenerators.CopyTo(_sceneGenerators, 0);
        _enemyGenerators.CopyTo(_sceneGenerators, _sceneryGenerators.Length - 2);


        // minions ignore collision with each other
        int layerMask = LayerMask.NameToLayer("Friendly");
        Physics.IgnoreLayerCollision(layerMask, layerMask, true);

    }

    void Start()
    {
        foreach (BaseSceneGenerator generator in _sceneGenerators)
        {
            generator.Init();
        }
    }

    // Update is called once per frame
    void Update () {
        foreach (BaseSceneGenerator generator in _sceneGenerators)
        {
            generator.Update();
        }
	}
}
