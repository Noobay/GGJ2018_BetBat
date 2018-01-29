using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    [SerializeField]
    private SceneryGenerator[] _sceneryGenerators;

    [SerializeField]
    private EnemyGenerator[] _enemyGenerators;

    private BaseSceneGenerator[] _sceneGenerators;

    private void Awake()
    {
        _sceneGenerators = new BaseSceneGenerator[_enemyGenerators.Length + _sceneryGenerators.Length];


        _sceneryGenerators.CopyTo(_sceneGenerators, 0);
        _enemyGenerators.CopyTo(_sceneGenerators, _sceneryGenerators.Length);


        // minions ignore collision with each other
        int friendlyLayerMask = LayerMask.NameToLayer("Friendly");
        int enemyLayerMask = LayerMask.NameToLayer("Enemy");
        int bordersLayerMask = LayerMask.NameToLayer("Borders");
        Physics2D.IgnoreLayerCollision(friendlyLayerMask, friendlyLayerMask, true);
        Physics2D.IgnoreLayerCollision(enemyLayerMask, bordersLayerMask, true);
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
