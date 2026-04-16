using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

//[RequireComponent(typeof(AudioSource))]
public class GrenadeScript : MonoBehaviour 
{
    public GameObject Explosion;
	public float targetThresh = 45.0f;
    public float canThresh = 5.0f;

    GameObject GM;  // GameManager

    void Start()
    {
        GM = GameObject.Find("GameManager");
        StartCoroutine(SelfDestruct());
    }

    // Collision class includes info for physics interaction
    // OnTriggerEnter (Collider collider)                                              
    void OnCollisionEnter(Collision collisionInfo)
    {                                                
		//Debug.Log (Collisioninfo.impactForceSum.magnitude.ToString());
		
		// Code copied from BallShooter.cs Returns UnsignedReferenceException?
		//GameObject explosion = (GameObject)Instantiate(Explosion);
        /*unused*/ // Rigidbody explosion_rb = explosion.GetComponent<Rigidbody>();
        /*unused*/ // explosion.name = "explosion";

        //Instantiate(Explosion,transform.position,transform.rotation);
        //transform.position
        //transform.rotation
        var position = transform.position;
        var rotation = transform.rotation;
        Instantiate(Explosion, position, rotation);

        Destroy(gameObject);
    }
    
    // Detect if it hit the enemy fire ball which has "is Trigger" on
    void OnTriggerEnter(Collider co)
    {
        if (co.gameObject.tag == "Fire")
        {
            Destroy(co.gameObject); 
            print("destroy fire ball");
            Destroy(gameObject);
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }

}
