using UnityEngine;

/*
The issue doesn't happen if OnParticleCollision and Emit caller are separated into two scripts,
even if the Emit caller implements OnParticleCollision.
*/
public class EmitOnCollisionTest_SeparatedMain : MonoBehaviour
{
    [SerializeField] private EmitOnCollisionTest_SeparatedSub _associateScript;
    private string _entered = null;

    public void OnParticleCollision(GameObject other)
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

}
