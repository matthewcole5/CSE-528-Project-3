using UnityEngine;
using System.Collections;

public class GrenadeShooter : MonoBehaviour 
{

    [SerializeField] bool use_projectile_origin_location = false;
    [SerializeField] Transform projectile_origin;
    [SerializeField][Tooltip("Enable to set projectile target to a raycasted point, else shoot in forward direction")] bool RayCastTargetting = false;

    public GameObject Grenade;
    public float Force = 50.0f;
    public Vector3 Torque = new Vector3(100, 0, 0);

    //private Gun _gun;

    private bool cursorLocked = false;

	void Start() 
    {
        LockCursor();

        //_gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

	void Update() 
    {

        //check if the gun is ready to fire
        //if (_gun.ReadyToFire())
        {
            if (Input.GetMouseButtonDown(0))
            {
				//call Bang method to perform gun animation and sounds
                //_gun.Bang();

                // Note: transform.position returns object's position in the World space
                

                if(RayCastTargetting && use_projectile_origin_location)
                {
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2.0f,Screen.height/2.0f,0) );
                    RaycastHit hit;
                    projectile_origin.transform.forward = transform.forward;
                    if (Physics.Raycast(ray, out hit))
                    {
                        var _ray = (transform.position - hit.point);
                        print("Hit: " + hit.collider.gameObject.name + " ray length " + _ray.magnitude);
                        projectile_origin.transform.forward = _ray.magnitude >2.5f? (hit.point - projectile_origin.transform.position) : transform.forward;
                    }
                }
                                
                GameObject grenade = (GameObject)Instantiate(Grenade);
                Rigidbody grenade_rb = grenade.GetComponent<Rigidbody>();
                grenade.name = "grenade";
                // Fire the grenade 2 unit forward from the camera

                grenade.transform.position = transform.TransformPoint(2 * Vector3.forward);
                grenade.transform.forward = transform.forward;

                if (use_projectile_origin_location)
                {
                    grenade.transform.position = projectile_origin.position;
                    grenade.transform.forward = projectile_origin.forward;
                }

                //grenade_rb.velocity = transform.TransformDirection(new Vector3(0, 0, Force));
                if (grenade.GetComponent<GrenadeScript>() != null)
                {
                    grenade_rb.AddForce(grenade_rb.transform.TransformDirection(new Vector3(0, 0, Force)));
                    grenade_rb.AddTorque(Torque);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
            LockCursor();
	}

    void LockCursor()
    {
        if (!cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
        }
    }
}