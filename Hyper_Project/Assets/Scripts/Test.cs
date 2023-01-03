using UnityEngine;
using DG.Tweening;
public class Test : MonoBehaviour
{
    public float explodeForce = 50f;
    public Rigidbody mainRb;
    public float distance;
    public float killTime = 2f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TreeDestruct();
        }
    }

    public void TreeDestruct()
    {
        var effectivesCollider = Physics.OverlapSphere(mainRb.position, distance);

        foreach (var VARIABLE in effectivesCollider)
        {
            Debug.Log(VARIABLE.name);


            if (VARIABLE.TryGetComponent<Rigidbody>(out Rigidbody selectedRigidbody))
            {
                selectedRigidbody.constraints = RigidbodyConstraints.None;
                selectedRigidbody.useGravity = true;
                selectedRigidbody.AddExplosionForce(explodeForce, mainRb.position, distance, 0.16F);

                var gobTransform = selectedRigidbody.transform;
                gobTransform.DOScale(Vector3.zero, killTime).SetEase(Ease.InCubic)
                    .OnComplete(() => { Destroy(gobTransform, .25f); });
            }
        }
    }
}