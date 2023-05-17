using System;
using UnityEngine;

namespace _Scripts
{
	public class FootMover : MonoBehaviour
    {
	    [SerializeField] private float _stepDistance;
	    [SerializeField] private Transform _pelvis;
	    private GameObject _anchor;
		private ConfigurableJoint _joint;

		private Rigidbody _anchorBody;
	    
	    private void Awake()
	    {
		    _joint = gameObject.AddComponent<ConfigurableJoint>();
	    }

	    private void Start()
	    {
		    _joint.xMotion = ConfigurableJointMotion.Locked;
		    _joint.yMotion = ConfigurableJointMotion.Locked;
		    _joint.zMotion = ConfigurableJointMotion.Locked;
		    _joint.angularXMotion = ConfigurableJointMotion.Locked;
		    _joint.angularYMotion = ConfigurableJointMotion.Locked;
		    _joint.angularZMotion = ConfigurableJointMotion.Locked;


		    _anchor = new GameObject("Anchor");
		    _anchor.transform.position = transform.position;
		    _anchor.transform.rotation = transform.rotation;

		    _anchorBody = _anchor.AddComponent<Rigidbody>();
		    _anchorBody.isKinematic = true;
		    _anchorBody.useGravity = false;
		    _joint.connectedBody = _anchorBody;
	    }

	    private void FixedUpdate()
	    {
		    if (Mathf.Abs(_anchor.transform.position.z - _pelvis.position.z) > _stepDistance)
		    {
			    _anchor.transform.position = transform.position + _pelvis.transform.forward * _stepDistance;
		    }
	    }

	    private void Update()
	    {
		    if (Input.GetKeyDown(KeyCode.T))
		    {
			    _anchor.transform.position = transform.position + _pelvis.transform.forward * _stepDistance;

		    }
	    }
    }
}
