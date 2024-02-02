using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private string buildTag;

    public Action<Collider> OnBuildDetected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(buildTag))
        {
            OnBuildDetected?.Invoke(other);
          
        }
    }
}
