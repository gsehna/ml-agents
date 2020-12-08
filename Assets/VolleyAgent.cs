using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class VolleyAgent : Agent
{
    public Transform target;
    public float forceMultiplier;
    private Rigidbody targetBody;
    private Rigidbody body;

    [Header("Starting Area")]
    public Vector2 minRandom;
    public Vector2 maxRandom;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        targetBody = target.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(minRandom.x, minRandom.y),
                                              1.5f,
                                              Random.Range(maxRandom.x, maxRandom.y));

        body.angularVelocity = Vector3.zero;
        body.velocity        = Vector3.zero;

        // Reset Target
        targetBody.angularVelocity = Vector3.zero;
        targetBody.velocity        = Vector3.zero;
        do
        {
            target.position = new Vector3(Random.Range(-2, 2),
                                          Random.Range(5, 8),
                                          Random.Range(-7, 7));
        } while (target.position.z == 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(targetBody.velocity.x);
        sensor.AddObservation(targetBody.velocity.y);
        sensor.AddObservation(targetBody.velocity.z);

        // Agent
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(body.velocity.x);
        sensor.AddObservation(body.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        body.AddForce(controlSignal * forceMultiplier);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
