using UnityEngine;

namespace _Scripts.Car.Scripts
{
    public class GravZone : MonoBehaviour
    {
    
        public CarPhysics car;
        [Range (1,10)]
        public int fieldType;
        CarPhysics.GravField thething;

        void OnTriggerEnter(Collider other)
        {
            thething = new CarPhysics.GravField(gameObject, fieldType);
            car.zones.Add(thething);
        }

        void OnTriggerExit(Collider other)
        {
            CarPhysics carrie = car.GetComponent<CarPhysics>();
            car.zones.Remove(thething);
        }
    }
}
