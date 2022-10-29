using UnityEngine;
using System.Threading;

/*
Issue reproducing of
Emitting particles while in OnParticleCollision sometimes cause a termination of the method without any exceptions nor errors.
*/
public class EmitOnCollisionTest_IssueRepro : MonoBehaviour
{
    [SerializeField] public ParticleSystem _myParticleSystem;
    [SerializeField] private EmitOnCollisionTest_IssueRepro _associateScript;
    [SerializeField] private int _initialEmitCount;
    [SerializeField] private int _eachEmitCount;

    private string _entered = null;
    private int _threadId = Thread.CurrentThread.ManagedThreadId;

    void Start()
    {
        _myParticleSystem.Emit(_initialEmitCount);
    }

    void OnParticleCollision(GameObject other)
    {
        try
        {
            if (_entered != null)
            {
                Debug.LogError(gameObject.name + " enters OnParticleCollision" + ", but already entered=" + _entered
                +", _threadId=" + _threadId + ", cur thread=" + Thread.CurrentThread.ManagedThreadId);
            }
            _entered = "OnParticleCollision";
            _threadId = Thread.CurrentThread.ManagedThreadId;

            _associateScript.CallEmit();
        }
        finally
        {
            _entered = null;
        }
    }

    public void CallEmit()
    {
        try
        {
            if (_entered != null)
            {
                Debug.LogError(gameObject.name + " enters CallEmit" + ", but already entered=" + _entered
                    + ", _threadId=" + _threadId + ", cur thread=" + Thread.CurrentThread.ManagedThreadId);
            }
            _entered = "CallEmit";
            _threadId = Thread.CurrentThread.ManagedThreadId;

            _myParticleSystem.Emit(_eachEmitCount);
            // _myParticleSystem.Play(); // Play cause same issue (Enable Emission module before test)
        }
        finally
        {
            _entered = null;
        }
    }

}
