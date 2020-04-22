using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    private bool isDrop;

    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody;

    // Sheep Spawner
    private SheepSpawner sheepSpawner;

    // Heart
    public float heartOffset;   // Where the heart will spawn (y axis)
    public GameObject heartPrefab;

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

    // When sheep drops from the ground
    private void Drop()
    {
        // Sound sheep falling
        SoundManager.Instance.PlaySheepDroppedClip();
        // A sheep was dropped
        GameStateManager.Instance.DroppedSheep();
        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        Destroy(gameObject, dropDestroyDelay); 
    }

    // Sheep has been hitted
    private void HitByHay()
    {
        // A sheep was saved
        GameStateManager.Instance.SavedSheep();

        //Play song  of sheep hitted
        SoundManager.Instance.PlaySheepHitClip();

        //Destroy the sheep
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;
        Destroy(gameObject, gotHayDestroyDelay);

        // Create a heart where the sheep died
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        // Animation of the heart
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; 
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep") && !isDrop)
        {
            isDrop = true;
            Drop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        isDrop = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
}
