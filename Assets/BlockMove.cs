using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{   
    public bool isMoving;
    private float distance;
    [SerializeField] float inputSpeed;
    private float speed;
    [SerializeField] GameObject square;

    void Start() {
        isMoving = false;
    }

    void Update() {
        if (isMoving) {
            if (distance > 0) {
                Vector3 nextStep = speed * Time.deltaTime * new Vector3(1,0,0);
                if (distance > nextStep.x) {
                    this.transform.localPosition += nextStep;
                    distance -= nextStep.x;
                } else {
                    this.transform.localPosition += new Vector3(distance,0,0);
                    isMoving = false;
                }
            } else
            if (distance < 0) {
                Vector3 nextStep = speed * Time.deltaTime * new Vector3(-1,0,0);
                if (distance < nextStep.x) {
                    this.transform.localPosition += nextStep;
                    distance -= nextStep.x;
                } else {
                    this.transform.localPosition += new Vector3(distance,0,0);
                    isMoving = false;
                }
            }
            else {isMoving = false;}
            

        }
    }
    
    public void travel(float dist, float s) {
        speed = s;
        distance = dist;
        isMoving = true;
    }
    
}
