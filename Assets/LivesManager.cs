using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour {

    private static Queue<PawnController> _deadQueue = new Queue<PawnController>();

    [SerializeField]
    private float _deadBatCooldown = 5;

    [SerializeField]
    private Animator _timerAnimator;

    [SerializeField]
    private static LivesManager _instance;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        StartCoroutine(ReviveDeadBats());
    }

    public static void enqueueDeadBat(PawnController deadBat)
    {
        _deadQueue.Enqueue(deadBat);
    }
    
    public static IEnumerator ReviveDeadBats()
    {
        while (_deadQueue.Count == 0)
        {
            _stopTimer();
            yield return null;
        }

        _startTimer();
        float startTime = Time.time;
        while ((Time.time - startTime) <= _instance._deadBatCooldown)
        {
            Debug.Log(Time.time - startTime);

            yield return null;
        }

        Debug.Log("yielding revive");
        _deadQueue.Dequeue().Revive();

        yield return ReviveDeadBats();
    }

    private static void _startTimer()
    {
        _instance._timerAnimator.SetBool("run", true);
    }

    private static void _stopTimer()
    {
        _instance._timerAnimator.SetBool("run", false);
    }
}
