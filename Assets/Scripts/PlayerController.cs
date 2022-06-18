using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Remember to add gravity to the player for movement.
    //Also set animator to true if no animator starts the player.
    public bool testCoroutine;
    [SerializeField] public bool animatorNull = false;

    public float stamina = 100;
    public GameObject gameRun;


    public float baseSpeed = 1;
    [SerializeField] public float speed = 2f;
    [SerializeField] public float upwardSpeed = 2f;
    [SerializeField] public float maxVelocity = 20f;
    [SerializeField] public float maxSpeed = 20f;
    [SerializeField] public float dropForce = 20f;
    [SerializeField] public float staminaDecreaseRate = 10f;
    public int health = 1;
    [SerializeField] private float turnSpeed = 900f;
    public float playerTurnVariable = .75f;

    private float horizontalInput;
    private float forwardInput;
    public Rigidbody thisRigidbody;
    private float screenCenterX;

    private bool playerHitObstacle = false;
    public bool playerGetsHurt = false;

    public bool slowDownBool = false;
    private float massSlowDownVariable = 5.625f;
    public bool breakingEnabled = false;

    //power up variables
    public bool playerDefense = false;
    public bool newAnimationBool = false;


    //for wind drag controls
    private bool mouseDownBool = false;
    private Vector3 firstMousePosition;
    private Vector3 secondMousePosition;
    // This variable should probably be looked over and changed later
    public int windForce = 150;



    public float animationDelay = 2;

    public float maxHeightTrigger = 100;

    private bool maxHeightForceCorrection = false;


    // rotation
    private Quaternion originalRotation;
    private float rotateSpeed = 1f;
    public bool restoreRotation = false;
    public bool restoreRotationTriggered = false;
    private bool closeRotation = false;
    private bool forceBack = false;


    private float secondsStopped = 3;


    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;

        animationDelay = 2;
        gameObject.SetActive(true);
        //Variables we may need to keep track of when we create save scripts
        /*
        health = GetComponent<PlayerUpgradeVariables>().playerHealth;
        maxSpeed = GetComponent<PlayerUpgradeVariables>().playerMaxSpeed;
        maxVelocity = GetComponent<PlayerUpgradeVariables>().playerMaxSpeed;
        */
        screenCenterX = Screen.width * 0.5f;
        thisRigidbody = GetComponent<Rigidbody>();
        //thisRigidbody.velocity = new Vector3(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Animator>() == null && !animatorNull)
        {
            //thisRigidbody.AddForce(Vector3.up * 5, ForceMode.Acceleration);
            animatorNull = true;
            thisRigidbody.AddForce(Vector3.forward * 5);

            Invoke("AfterAnimationForce", animationDelay );
            //thisRigidbody.AddForce(Vector3.up * 15, ForceMode.Acceleration);
        }
        if (animatorNull || newAnimationBool)
        {
            PlayerControlInputs();
        }

        if (transform.position.y > maxHeightTrigger)
        {
            maxHeightForceCorrection = true;
            float delta = 10 * Time.deltaTime;
            Vector3 currentPosition = transform.position;
            Vector3 nextPosition = Vector3.MoveTowards(currentPosition, new Vector3(currentPosition.x, maxHeightTrigger, currentPosition.z), delta);
            transform.position = nextPosition;
        }



        // checks if player is stopped
        /*
        if (gameObject.GetComponent<Rigidbody>().velocity.z < 1)
        {
            Invoke("PlayerStopped", 3);
        }*/


        

    }

    private void LateUpdate()
    {
        
        /* if (maxHeightForceCorrection && transform.position.y < maxHeightTrigger)
        {
            maxHeightForceCorrection = false;
            thisRigidbody.AddForce(Vector3.up * 1);
        }*/
    }

    private void AfterAnimationForce()
    {
        Debug.Log("triggered animation force");
        thisRigidbody.useGravity = true;
        speed = 1.1f;
    }

    private void PlayerControlInputs()
    {
        ClickControls();
        ConstantMovement();
        WasdControls();
        //thisRigidbody.AddForce(Vector3.down * speed * (thisRigidbody.mass / 1.5f), ForceMode.Force);
        /*if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            thisRigidbody.position += Vector3.back * Time.deltaTime * speed;
        }*/
        /*
        if (Input.touchCount > 0)
        {
            // get the first one
            Touch firstTouch = Input.GetTouch(0);
            if (breakingEnabled)
            {
                if (firstTouch.position.x > screenCenterX && (firstTouch.position.y > Screen.height / 5))
                {
                    // if the touch position is to the right of center
                    thisRigidbody.AddForce(Vector3.right * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
                    // move right
                }
                if (firstTouch.position.x < screenCenterX && (firstTouch.position.y > Screen.height / 5))
                {
                    // if the touch position is to the left of center
                    // move left
                    thisRigidbody.AddForce(Vector3.left * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
                }
                if ((firstTouch.position.y < Screen.height / 5))
                {
                    slowDownBool = true;
                    thisRigidbody.AddForce(-Vector3.forward * speed * massSlowDownVariable * thisRigidbody.mass, ForceMode.Force);
                }
            }
            else
            {
                if (firstTouch.position.x > screenCenterX)
                {
                    // if the touch position is to the right of center
                    thisRigidbody.AddForce(Vector3.right * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
                    // move right
                }
                if (firstTouch.position.x < screenCenterX)
                {
                    // if the touch position is to the left of center
                    // move left
                    thisRigidbody.AddForce(Vector3.left * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
                }
            }
        }*/
    }

    private void ClickControls()
    {
        if (Input.GetMouseButton(0) && !mouseDownBool)
        {
            mouseDownBool = true;
            firstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDownBool = false;
            secondMousePosition = Input.mousePosition;


            // make wind force function here. 

            // Move Player Down
            if (firstMousePosition.y - secondMousePosition.y > 0)
            {
                float powerCalculation = (windForce * firstMousePosition.y / Screen.height) - (windForce * secondMousePosition.y / Screen.height);
                //thisRigidbody.AddForce(Vector3.down * powerCalculation, ForceMode.Acceleration);
                if (speed > 0)
                {
                    thisRigidbody.AddForce(Vector3.back * powerCalculation * Time.deltaTime * 10);
                    //speed = speed * -1;
                    //baseSpeed = baseSpeed * -1;
                }
            }

            if (firstMousePosition.y - secondMousePosition.y < 0)
            {
                float powerCalculation = Mathf.Abs((windForce * firstMousePosition.y / Screen.height) - (windForce * secondMousePosition.y / Screen.height));
                //thisRigidbody.AddForce(Vector3.up * powerCalculation, ForceMode.Acceleration);
                speed = Mathf.Abs(speed);
                baseSpeed = Mathf.Abs(baseSpeed);
            }

            if (firstMousePosition.x - secondMousePosition.x < 0)
            {
                float powerCalculation = Mathf.Abs((windForce * firstMousePosition.x / Screen.width) - (windForce * secondMousePosition.x / Screen.width));
                thisRigidbody.AddForce(Vector3.right * powerCalculation, ForceMode.Acceleration);
            }

            if (firstMousePosition.x - secondMousePosition.x > 0)
            {
                float powerCalculation = Mathf.Abs((windForce * firstMousePosition.x / Screen.width) - (windForce * secondMousePosition.x / Screen.width));
                thisRigidbody.AddForce(Vector3.left * powerCalculation, ForceMode.Acceleration);
            }

        }
    }

    public void PlayerStopped()
    {
        // Possible bones of method if we want to drop the player
        if ((gameObject.GetComponent<Rigidbody>().velocity.z < 1) && gameObject.activeSelf)
        {
            health = 1;
        }
    }

    public void RestoreRotation()
    {
        restoreRotation = true;
        restoreRotationTriggered = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitObstacle(collision);

        if (!restoreRotationTriggered)
        {
            restoreRotationTriggered = true;
            CancelInvoke("RestoreRotation");
            Invoke("RestoreRotation", 3);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            playerHitObstacle = false;
        }

    }


    private void OnTriggerEnter(Collider trigger)
    {
        // possible start to logic if we want obstacles to slow the player down. 

    }

    private void OnTriggerExit(Collider other)
    {

    }
    private void HitObstacle(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !playerHitObstacle)
        {
            playerHitObstacle = true;
            HurtPlayer();
        }
    }

    public void HurtPlayer()
    {
        //this is in the case of powerups for playerdefense
        if (!playerDefense)
        {
            health -= 1;
        }
        if (health <= 0 && !playerDefense)
        {
            // Create a method to drop the player

            //Create a shatterable script that will blow the 
            //gameObject.GetComponent<Shatterable>().Die();
            //DisplayDeathReward();
            //DisplayDeathReward();
            //DisplayDeathReward();
            //Invoke("DisplayDeathReward", 3);

            //Create a scipt to store player lives 
            //gameRun.GetComponent<Lives>().LoseLife();
        }
    }
   
    // Create a save method on application pause / quit

    private void WasdControls()
    {
        if (thisRigidbody.useGravity)
        {

            if (Input.GetKey(KeyCode.W) && transform.position.y < maxHeightTrigger)
            {
                //thisRigidbody.AddForce(Vector3.left * Time.deltaTime * turnSpeed);
                thisRigidbody.AddForce(Vector3.up * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
                stamina -= Time.deltaTime * staminaDecreaseRate;
            }
            else
            {
                stamina += Time.deltaTime * 2;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            //thisRigidbody.AddForce(Vector3.left * Time.deltaTime * turnSpeed);
            thisRigidbody.AddForce(Vector3.left * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //thisRigidbody.AddForce(Vector3.right * Time.deltaTime * turnSpeed);
            thisRigidbody.AddForce(Vector3.right * Time.deltaTime * turnSpeed * thisRigidbody.mass * playerTurnVariable);
        }

        if (Input.GetKey(KeyCode.S) && breakingEnabled)
        {
            slowDownBool = true;
            thisRigidbody.AddForce(Vector3.down * speed * massSlowDownVariable * thisRigidbody.mass, ForceMode.Force);
            //GetComponent<Rigidbody>().velocity *= .3f;
        }

        if (Input.GetKeyDown("space"))
        {
            speed = 0;
            thisRigidbody.velocity *= 0.1f;
            thisRigidbody.AddForce(Vector3.down * dropForce);
        }
        if (Input.GetKeyUp("space"))
        {
            speed = baseSpeed;
        }

        if (animatorNull && speed < baseSpeed)
        {
            speed += 2 * Time.deltaTime;
        }
    }

    private void ConstantMovement()
    {
        thisRigidbody.AddForce(Vector3.up * upwardSpeed * Time.deltaTime, ForceMode.Force);

        if ((thisRigidbody.velocity.z < maxVelocity) && !Input.GetKeyDown("space"))
        {
            if (thisRigidbody.velocity.z < maxVelocity / 2)
            {
                thisRigidbody.AddForce(Vector3.forward * speed * 7, ForceMode.Acceleration);
            }
            thisRigidbody.AddForce(Vector3.forward * speed, ForceMode.Acceleration);
        }


        // this allows consistent rotation and resets the dandelion to a starting position after a set time. 
        if (restoreRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * rotateSpeed);

            //Debug.Log("Z difference" + (FindDifference(transform.rotation.z, originalRotation.z)));
            //Debug.Log("y difference" + (FindDifference(transform.rotation.y, originalRotation.y)));
            //Debug.Log("x difference" + (FindDifference(transform.rotation.x, originalRotation.x)));
            if ((FindDifference(transform.rotation.z, originalRotation.z) < .1f) && (FindDifference(transform.rotation.x, originalRotation.x) < .1f) && (FindDifference(transform.rotation.y, originalRotation.y) < .1f))
            {
                transform.rotation = originalRotation; 
            }
            thisRigidbody.freezeRotation = true;
            if (transform.rotation == originalRotation)
            {
                restoreRotation = false;
                thisRigidbody.freezeRotation = false;

            }
        }

        if ((thisRigidbody.velocity.z < .1f) && animatorNull)
        {
            Debug.Log("BACKUP!");
            secondsStopped -= Time.deltaTime;
            if (secondsStopped <= 0)
            {
                stamina -= 5;
                thisRigidbody.AddForce(Vector3.back * 55);
                thisRigidbody.AddForce(Vector3.up * 45);
                secondsStopped = 3;
            }

        }

        if (thisRigidbody.velocity.z > 1f)
        {
            secondsStopped = 3;

        }

        if (!restoreRotation && !restoreRotationTriggered)
        {
            transform.Rotate(new Vector3(10, 20, 10) * Time.deltaTime * 2);
        }

    }
    public float FindDifference(float nr1, float nr2)
        {
            return Mathf.Abs(nr1 - nr2);
        }
 
}
