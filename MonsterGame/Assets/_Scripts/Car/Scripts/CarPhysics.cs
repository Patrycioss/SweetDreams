using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Car.Scripts
{
    public class CarPhysics : MonoBehaviour
    {
        public class GravField
        {
            public GameObject origin;
            public int fieldType;

            public GravField(GameObject org, int fld)
            {
                origin = org;
                fieldType = fld;
            }
        }

        public Rigidbody carRB;
        public List<GameObject> wheels;
        public GameObject GravOrigin;
        public float gravity = 9.81f;
        [Range (0,10)]
        public int gravSit = 1;
        public List<GravField> zones = new List<GravField>();

        [Header("Car Stats")]
        public float springStrength = 15;
        public float springDamper = .5f;
        public float tireRadius = .25f;
        public float tireGrip = 1;
        public float tireMass = 1;
        public float topSpeed;
        public AnimationCurve curve;
        public float rollFriction = 1f;
        public float breakSpeed = 1f;
        public float steeringAngle = 30f;
    

        float x;
        float y;
    
        void Start()
        {
            carRB = GetComponent<Rigidbody>();
        }
    
        void FixedUpdate()
        {
            InputCollection();
            ChooseGrav();
            if (zones.Count != 0)
            {
                int last = zones.Count - 1;
                DoGrav(zones[last].fieldType,zones[last].origin);
            }
            else
            {
                DoGrav(0,null);
            }
            WheelLogic();
        }

        void InputCollection()
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        }

        void ChooseGrav()
        {

        }

        void DoGrav(int situation, GameObject origin)
        {  
            switch(situation)
            {
                case 1:
                    carRB.AddForce(Vector3.Normalize(gameObject.transform.position - origin.transform.position) * gravity * -1);
                    break;
                case 2:
                    Vector3 gravdir = gameObject.transform.position - origin.transform.position;
                    carRB.AddForce(Vector3.Normalize(Vector3.ProjectOnPlane(gravdir, origin.transform.forward)) * gravity * -1);
                    break;
                case 3:
                    carRB.AddForce(origin.transform.up * gravity);
                    break;
                default: 
                    carRB.AddForce(new Vector3(0,1,0) * gravity);
                    break;
            }
        
        }

        void WheelLogic()
        {

            float carSpeed = Vector3.Dot(transform.forward,carRB.velocity);

            for (int i = 0; i<2; i++)
            {
                wheels[i].transform.localRotation = Quaternion.Euler(new Vector3(0,(x*steeringAngle),0));
            }

            foreach (GameObject wheel in wheels)
            {
                Transform wtf = wheel.transform; 
                RaycastHit hit;
                Vector3 wUp = wtf.TransformDirection(Vector3.up);
                bool didHit = Physics.Raycast(wtf.position, wtf.TransformDirection(Vector3.down), out hit, tireRadius);
            

                if (didHit)
                {
                    Vector3 worldVel = carRB.GetPointVelocity(wtf.position);

                    float offset = .5f - hit.distance;

                    float vel = Vector3.Dot(wtf.up, worldVel);

                    float force = (offset * springStrength) - (vel * springDamper);

                    Vector3 suspension = wUp * force;

                


                    float steeringVel = Vector3.Dot(wtf.right,worldVel);

                    float desiredVelChange = -steeringVel * tireGrip;

                    float desiredAccel = desiredVelChange / Time.deltaTime;

                    Vector3 steering = wtf.right * desiredAccel * tireMass;

                    Vector3 driving = Vector3.Normalize(-carSpeed * transform.forward) * rollFriction;

                    if (y != 0)
                    {

                        float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / topSpeed);

                        float availableTorque = curve.Evaluate(normalizedSpeed) * y;

                        if (y * carSpeed > 0)
                        {
                            driving = wtf.forward * availableTorque;
                        }
                        else
                        {
                            driving = breakSpeed * y * wtf.forward * tireGrip;
                        }
                    }

                    carRB.AddForceAtPosition(suspension + steering + driving, wtf.position);
                }
            }
        }
    }
}
