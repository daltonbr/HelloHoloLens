﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.XR.WSA.Input;

public class InteractionHelper : MonoBehaviour
{
    public Material MaterialInGaze;
    private Material _oldMaterial;
    private GameObject _objectInFocus;
    public GameObject ObjectToReset;


    void Start ()
    {
		GestureRecognizer gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        // Obsolete Event
        //gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;        
        Action<TappedEventArgs> tapAction = OnTappedEvent;        
        gestureRecognizer.Tapped += tapAction;
        gestureRecognizer.StartCapturingGestures();

        KeywordRecognizer keywordRecognizer = new KeywordRecognizer(new [] {"Stop"});
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (ObjectToReset == null) { return; }

        // Reset our Object to initial position
        var rb = ObjectToReset.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        ObjectToReset.transform.position = new Vector3(0f, 0f, 2f);
    }

    private void OnTappedEvent(TappedEventArgs tappedEventArgs)
    {    
        if (_objectInFocus == null) { return; }
        _objectInFocus.SendMessage("DoDrop");
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
