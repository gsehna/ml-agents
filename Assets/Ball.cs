using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public VolleyAgent redAgent;
    public VolleyAgent blueAgent;
    private VolleyAgent lastNotTouched = null;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Blue": lastNotTouched = redAgent;
                break;
            case "Red": lastNotTouched = blueAgent;
                break;
            case "Blue Court":
                blueAgent.AddReward(1f);
                blueAgent.EndEpisode();
                redAgent.EndEpisode();
                break;
            case "Red Court":
                redAgent.AddReward(1f);
                blueAgent.EndEpisode();
                redAgent.EndEpisode();
                break;
            case "Ground":
                if (lastNotTouched)
                {
                    lastNotTouched.AddReward(1f);
                }
                blueAgent.EndEpisode();
                redAgent.EndEpisode();
                break;
        }
    }
}
