using System.Collections;
using UnityEngine;

public class PawnController : MonoBehaviour {

    private static readonly string DEAD_ANIMATION_KEY = "dead";
    private static readonly string DIE_BOOL_KEY = "die";

    [SerializeField]
    private Transform _spawnTransform;

    private RaycastHit2D _hit;
    private Vector3 _firstTouchPosition;
    private bool _hitByLastTouchBegin = false;

    [SerializeField]
    private float _swipeMaxStrength = 10f;
    [SerializeField]
    private float _swipeStrengthMutliplier = 1f;
    [SerializeField]
    private float _swipeTouchStrengthMultiplier = 0.2f;

    private Animator _animator;

    public void Start()
    {
        _animator = transform.GetChild(0).GetComponentInChildren<Animator>();
    }

    public void Update()
    {

#if UNITY_ANDROID || UNITY_IOS

        // this should be managed from an input manager of somesort
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

            if (hit && hit.collider && (hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()))
            {
                _swipeStarted(currentTouch.position);
                _hitByLastTouchBegin = true;
            } else
            {
                _hitByLastTouchBegin = false;
            }
        }
        else if (_hitByLastTouchBegin && currentTouch.phase == TouchPhase.Ended)
        {
            _swipeEnded(currentTouch.position);
            _hitByLastTouchBegin = false;
        }
#endif
    }

#if UNITY_EDITOR_WIN || UNITY_WSA || UNITY_WEBGL
    private void OnMouseDown()
    {
        Vector3 currentPos = Input.mousePosition;
        // Construct a ray from the current mouse coordinates
        Vector3 touch_origin = Camera.main.ScreenToWorldPoint(currentPos);
        RaycastHit2D hit = Physics2D.CircleCast(touch_origin, 3, Camera.main.transform.forward);


        if (hit && hit.collider && (hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()))
        {
            _swipeStarted(currentPos);
            _hitByLastTouchBegin = true;
        }
    }

    private void OnMouseUp()
    {
        _swipeEnded(Input.mousePosition);
    }
#endif 
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
        _animator.SetBool(DIE_BOOL_KEY, false);

        transform.position = (Vector2)_spawnTransform.position;
    }

    private void _kill()
    {
        transform.position = new Vector3(-100, -100, -100);
        LivesManager.enqueueDeadBat(this);
    }

    private void _swipeStarted(Vector3 position)
    {
        _firstTouchPosition = position;
    }

    private void _swipeEnded(Vector3 position)
    {
        Vector2 swipeVector = -1 * (_firstTouchPosition - position);

        Vector2 normalizedSwipe = swipeVector.normalized;
        float swipeMagnitude = Mathf.Clamp(swipeVector.magnitude, 0f, _swipeMaxStrength);
#if UNITY_ANDROID || UNITY_IOS
        swipeMagnitude *= _swipeTouchStrengthMultiplier;
#endif
        GetComponent<Rigidbody2D>().AddForce(swipeVector * swipeMagnitude * _swipeStrengthMutliplier, ForceMode2D.Impulse);
    }
}
