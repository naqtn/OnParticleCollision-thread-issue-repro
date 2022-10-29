using UnityEngine;

public class EmitOnCollisionTest_SeparatedSub : MonoBehaviour
{
    [SerializeField] public ParticleSystem _myParticleSystem;
    [SerializeField] private int _initialEmitCount;
    [SerializeField] private int _eachEmitCount;

    private string _entered = null;

    void Start()
    {
        _myParticleSystem.Emit(_initialEmitCount);
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
        }
        finally
        {
            _entered = null;
        }
    }

    private int _count = 0;
    void OnParticleCollision(GameObject other)
    {
        if (_count++ % 1000 == 0)
        {
            Debug.Log("_count=" + _count);
        }
    }
}
