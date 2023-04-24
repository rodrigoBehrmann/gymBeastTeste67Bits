using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActive : MonoBehaviour
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
        RagDollOff();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Punch" && GameManager.instance.enemyInBack != GameManager.instance.coletableEnemies)
        {
            RagDollOn();
            Destroy(gameObject, 1f);
        }
    }

    //collects all GameObject colliders and rigidbodies
    void GetRagdollAndRigidbodies()
    {
        ragDollColliders = enemyGO.GetComponentsInChildren<Collider>();
        limbsRigidbodies = enemyGO.GetComponentsInChildren<Rigidbody>();
    }

    //disable the RagDoll
    void RagDollOff()
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rig in limbsRigidbodies)
        {
            rig.isKinematic = true;
        }

        enemyAnim.enabled = true;
        boxCollider.enabled = true;
        enemyRigibody.isKinematic = false;
    }

    //enable the RagDoll
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
        enemyRigibody.isKinematic = true;
    }

    //wait for the object to be destroyed to collect
    private void OnDestroy()
    {
        GameManager.instance.EnemyCollect();
    }
}