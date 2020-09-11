using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGlobalController : MonoBehaviour
{
    public Transform rayOrigin;
    public Ray ray;
    public RaycastHit raycast;
    public Transform obj;

    // Update is called once per frame
    void Update()
    {
        ray.origin = rayOrigin.position;
        ray.direction = rayOrigin.forward;
        if (Physics.Raycast(ray, out raycast))
        {
            if (obj == null)
                obj = raycast.transform;
            if (obj == raycast.transform)
            {
                if (raycast.transform.GetComponent<IHotfix>() != null)
                {
                    raycast.transform.GetComponent<IHotfix>().OnHeadEnterTrigger(null);
                }
            }
            else
            {
                if (obj.GetComponent<IHotfix>() != null)
                {
                    obj.transform.GetComponent<IHotfix>().OnHeadExitTrigger(null);
                }
                if (raycast.transform.GetComponent<IHotfix>() != null)
                {
                    raycast.transform.GetComponent<IHotfix>().OnHeadEnterTrigger(null);
                }
                obj = raycast.transform;
            }
        }
        else
        {
            if (obj)
            {
                if (obj.GetComponent<IHotfix>() != null)
                {
                    obj.transform.GetComponent<IHotfix>().OnHeadExitTrigger(null);
                }
                obj = null;
            }
        }
    }
}
