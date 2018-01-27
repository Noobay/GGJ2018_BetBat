using System.Collections;
using UnityEngine;

public class PawnController : MonoBehaviour {

    private static readonly string DEAD_ANIMATION_KEY = "dead";
    private static readonly string DIE_BOOL_KEY = "die";
    private static readonly string DEATH_STATE_KEY = "death";

    [SerializeField]
    private Transform _spawnTransform;

    private RaycastHit2D _hit;
    private Vector2 _firstTouchPosition;
    private bool _hitByLastTouchBegin = false;

    [SerializeField]
    private float _swipeStrength = 10f;

    private Animator _animator;

    public void Start()
    {
        _animator = transform.GetChild(0).GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch currentTouch = Input.GetTouch(0);
         if (currentTouch.phase == TouchPhase.Began)
        {
            // Construct a ray from the current touch coordinates
            Vector3 touch_origin = Camera.main.ScreenToWorldPoint(currentTouch.position);
            RaycastHit2D hit = Physics2D.CircleCast(touch_origin, 3, Camera.main.transform.forward);

            if (hit && hit.collider & (hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()))
            {
                _swipeStarted(currentTouch);
                _hitByLastTouchBegin = true;
            } else
            {
                _hitByLastTouchBegin = false;
            }
        }
        else if (_hitByLastTouchBegin && currentTouch.phase == TouchPhase.Ended)
        {
            _swipeEnded(currentTouch);
            _hitByLastTouchBegin = false;
        }

    }

    private IEnumerator _waitForStateAndDie(string animation_name)
    {
        do
        {
            yield return null;
        } while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animation_name));

        _kill();
    }

    public void ActDeath()
    {
        _animator.SetBool(DIE_BOOL_KEY, true);
        StartCoroutine(_waitForStateAndDie(DEAD_ANIMATION_KEY));
    }

    public void Revive()
    {
        _animator.SetBool(DEATH_STATE_KEY, false);
        _animator.SetBool(DIE_BOOL_KEY, false);

        transform.position = (Vector2)_spawnTransform.position;
    }

    private void _kill()
    {
        transform.position = new Vector3(-100, -100, -100);
        LivesManager.enqueueDeadBat(this);
    }

    private void _swipeStarted(Touch touch)
    {
        if (touch.deltaPosition.sqrMagnitude == 0)
        {
            return;
        }
 
        _firstTouchPosition = touch .deltaPosition;
    }

    private void _swipeEnded(Touch touch)
    {
        Vector2 touchVector = ((touch.deltaPosition + _firstTouchPosition) / 2).normalized;
        GetComponent<Rigidbody2D>().AddForce(touchVector * _swipeStrength, ForceMode2D.Impulse);
        Debug.Log(touchVector);
        Debug.Log(_firstTouchPosition + " and: " + touch.position);
    }
}
