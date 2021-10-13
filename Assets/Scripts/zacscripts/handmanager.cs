using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Interaction;

public class handmanager : MonoBehaviour
{
    LeapServiceProvider provider;
    [SerializeField]
    LineRenderer lineRendererL;
    [SerializeField]
    LineRenderer lineRendererR;

    [SerializeField]
    Sprite dirty;
    [SerializeField]
    Sprite clean;
    float lineWidth = .25f;
    // Start is called before the first frame update
    void Start()
    {
        provider = GetComponent<LeapServiceProvider>();
        lineRendererL.startWidth = lineWidth;
        lineRendererL.endWidth = lineWidth;

        provider = GetComponent<LeapServiceProvider>();
        lineRendererR.startWidth = lineWidth;
        lineRendererR.endWidth = lineWidth;

    }

    // Update is called once per frame
    void Update()
    {
        lineRendererL.enabled = false;
        lineRendererR.enabled = false;
        Frame frame = provider.CurrentFrame;
        foreach (Hand h in frame.Hands)
        {
            LineRenderer lineRenderer;
            if (h.IsLeft) lineRenderer = lineRendererL;
            else lineRenderer = lineRendererR;
            foreach (Finger f in h.Fingers)
            {
                if (f.Type == Finger.FingerType.TYPE_INDEX)
                {
                    if (f.IsExtended)
                    {
                        Vector3 pos = new Vector3(f.TipPosition.x, f.TipPosition.y, f.TipPosition.z);
                        Vector3 dir = new Vector3(f.Direction.x, f.Direction.y, f.Direction.z);
                        Ray ray = new Ray(pos, dir);
                        RaycastHit hit = new RaycastHit();
                        if (Physics.Raycast(ray, out hit, 1000))
                        {
                            GameObject g = hit.collider.gameObject;
                            lineRenderer.enabled = true;
                            lineRenderer.SetPositions(new Vector3[] { pos, hit.point });
                            foreach(Transform t in g.transform)
                            { 
                                SpriteRenderer s = t.gameObject.GetComponent<SpriteRenderer>();
                                if (s != null)
                                {
                                   if(s.sprite.name == dirty.name)
                                    {
                                        s.sprite = clean;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}