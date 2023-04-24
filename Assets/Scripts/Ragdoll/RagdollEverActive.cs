using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEverActive : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject enemyGO;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Rigidbody enemyRigibody;
    private Collider[] ragDollColliders;
    private Rigidbody[] limbsRigidbodies;

    void Start()
    {
        GetRagdollAndRigidbodies();
        RagDollOn();
    }

    void GetRagdollAndRigidbodies()
    {
        ragDollColliders = enemyGO.GetComponentsInChildren<Collider>();
        limbsRigidbodies = enemyGO.GetComponentsInChildren<Rigidbody>();
    }

    public void RagDollOn()
    {
        enemyAnim.enabled = false;

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rig in limbsRigidbodies)
        {
            rig.isKinematic = false;
        }

        boxCollider.enabled = false;
        //enemyRigibody.isKinematic = true;
    }
}
