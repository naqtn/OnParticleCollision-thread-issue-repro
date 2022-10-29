using UnityEngine;

/*
 The issue doesn't happen if calling ParticleSystem.Emit directly.
*/
public class EmitOnCollisionTest_CallDirectly : MonoBehaviour
{
    [SerializeField] public ParticleSystem _associateParticleSystem;
    [SerializeField] private int _initialEmitCount;
    [SerializeField] private int _eachEmitCount;

    void Start()
    {
        _associateParticleSystem.Emit(_initialEmitCount);
    }

    private string _entered = null;

    public void OnParticleCollision(GameObject other)
    {
        try
        {
            if (_entered != null)
            {
                Debug.LogError(gameObject.name + " enters OnParticleCollision" +  ", but already entered=" + _entered);
            }
            _entered = "OnParticleCollision";

            _associateParticleSystem.Emit(_eachEmitCount);
        }
        finally
        {
            _entered = null;
        }
    }

}
