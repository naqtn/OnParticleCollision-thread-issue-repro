using UnityEngine;

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
                Debug.LogError(gameObject.name + " enters OnParticleCollision" + ", but already entered=" + _entered);
            }
            _entered = "OnParticleCollision";

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
                Debug.LogError(gameObject.name + " enters CallEmit" + ", but already entered=" + _entered);
            }
            _entered = "CallEmit";

            _myParticleSystem.Emit(_eachEmitCount);
            // _myParticleSystem.Play(); // Play cause same issue (Enable Emission module before test)
        }
        finally
        {
            _entered = null;
        }
    }

}
