using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{

    public Material MaterialInGaze;
    private Material _oldMaterial;
    private GameObject _objectInFocus;


    void Start ()
    {
		
	}
	
	void Update ()
	{
	    var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
	    RaycastHit raycastInfo;

	    if (Physics.Raycast(ray, out raycastInfo))
	    {
	        var hitObject = raycastInfo.transform.gameObject;
	        if (hitObject == _objectInFocus) { return; }
	        var renderer = hitObject.GetComponent<Renderer>();

	        if (renderer == null) { return; }

	        _oldMaterial = renderer.material;
	        renderer.material = MaterialInGaze;
	        _objectInFocus = hitObject;
	    }
	    else
	    {
	        if (_objectInFocus == null) return;

	        var renderer = _objectInFocus.GetComponent<Renderer>();
	        renderer.material = _oldMaterial;
	        _objectInFocus = null;
	    }
	}
}
