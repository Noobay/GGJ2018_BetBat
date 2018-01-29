using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBatController : MonoBehaviour {
    // Both this class and PawnController should have an interface for a basic BatController

    private static string DIE_BOOL_KEY = "die";

    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = transform.GetChild(0).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D collision) {

    }

    public void ActDeath()
    {
        _animator.SetBool(DIE_BOOL_KEY, true);

        // TODO: LOSE GAME, this shouldn't be here
        SceneManager.LoadScene(0);
    }
}
