using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MathHelp;
using combat;

public class AIMove : MonoBehaviour
{
    public Vector3 startPos = new Vector3(5.0f, 0.0f, 0.0f);
    public Vector3 endPos = new Vector3(10.0f, 0.0f, 0.0f);

    LineEquation lineOfMotion = new LineEquation();
    private float pathLength;

    [HideInInspector]
    public bool[] stateFlags = new bool[3]; // 0 - whileReturning, 1 - whileMoving, 3 - whileFollowingPlayer
    bool lastMovingDir = false; // false - start, true - end

    private float distPerFrame = 0.1f;
    private float marginOfError = 0.1f;

    private IEnumerator coroutine;

    private void Start() {
        startPos.y = gameObject.transform.position.y;
        endPos.y = gameObject.transform.position.y;

        lineOfMotion.CountCoefficients(startPos.x, startPos.z, endPos.x, endPos.z);
        pathLength = (endPos - startPos).magnitude;
    }

    private void FixedUpdate() {
        if(stateFlags[2] == true){
            if(gameObject.GetComponent<CombatSystemAI>().player != null){
                StopCoroutine(coroutine);
                coroutine = MoveTo(gameObject.GetComponent<CombatSystemAI>().player.transform.position - gameObject.transform.position, 
                        gameObject.GetComponent<CombatSystemAI>().player.transform.position, 2);
                StartCoroutine(coroutine);
            }
            else{
                stateFlags[2] = false;
                gameObject.GetComponent<CombatSystemAI>().followingPlayer = false;
            }
        }
        else{
            if(!IsAtPath() && stateFlags[0] == false){
            ReturnToPath();
            }
            else{
                if(stateFlags[0] == false && stateFlags[1] == false){
                    stateFlags[1] = true;

                    if(lastMovingDir){
                        Vector3 distance = startPos - gameObject.transform.position;
                        coroutine = MoveTo(distance, startPos, 1);
                        lastMovingDir = false;
                    }
                    else{
                        Vector3 distance = endPos - gameObject.transform.position;
                        coroutine = MoveTo(distance, endPos, 1);
                        lastMovingDir = true;
                    }

                    StartCoroutine(coroutine);
                }
            }
        }
        
    }
    
    public void ReturnToPath(){
        stateFlags[0] = true;

        Vector3 distance1 = startPos - gameObject.transform.position;
        Vector3 distance2 = endPos - gameObject.transform.position;

        if(distance1.magnitude >= distance2.magnitude){
            coroutine = MoveTo(distance2, endPos, 0);
        }
        else{
            coroutine = MoveTo(distance1, startPos, 0);
        }

        StartCoroutine(coroutine);
    }

    private IEnumerator MoveTo(Vector3 returnVector, Vector3 returnPos, int flagIndex)
    {
        float oneDegreeInRad = 2 * Mathf.PI/360.0f;
        bool turnedRightWay = false;
        float angleShift = Mathf.PI * 1/2.0f;

        float aiDirRad;
        Vector2 aiDirection;
        Vector2 aiDirectionInc;
        Vector2 aiDirectionDec;

        Vector2 returnVector2 = new Vector2(returnVector.x, returnVector.z);

        float deltaX;
        float deltaZ;

        if(returnVector.x != 0){ 
            deltaX = Mathf.Sign(returnVector.x) * Mathf.Sqrt(Mathf.Pow(distPerFrame, 2)/(1 + Mathf.Pow(returnVector.z, 2)/Mathf.Pow(returnVector.x, 2)));
            deltaZ = deltaX * returnVector.z/returnVector.x;
        }
        else{
            deltaX = 0;
            deltaZ = distPerFrame * Mathf.Sign(returnVector.z);
        }
        
        while (stateFlags[flagIndex])
        {
            if(stateFlags[2] == true && flagIndex != 2)
            {
                stateFlags[flagIndex] = false;
                yield break;
            }
            if(!turnedRightWay){
                aiDirRad = -gameObject.transform.localEulerAngles.y * oneDegreeInRad + angleShift;
                aiDirection = new Vector2(Mathf.Cos(aiDirRad), Mathf.Sin(aiDirRad));
                aiDirectionInc = new Vector2(Mathf.Cos(aiDirRad + 2 * oneDegreeInRad), Mathf.Sin(aiDirRad +  2 * oneDegreeInRad));
                aiDirectionDec = new Vector2(Mathf.Cos(aiDirRad - 2 * oneDegreeInRad), Mathf.Sin(aiDirRad -  2 * oneDegreeInRad));

                if( Vector2.Angle(aiDirection, new Vector2(returnVector.x, returnVector.z)) <= 1.0f){
                    turnedRightWay = true;
                }else{
                    
                    if( Vector2.Angle(aiDirection, returnVector2) >= Vector2.Angle(aiDirectionInc, returnVector2) ){
                        gameObject.transform.Rotate(new Vector3(0.0f, -2.0f, 0.0f), Space.World);
                    }
                    else{
                        if(Vector2.Angle(aiDirection, returnVector2) > Vector2.Angle(aiDirectionDec, returnVector2)){
                            gameObject.transform.Rotate(new Vector3(0.0f, 2.0f, 0.0f), Space.World); 
                        }
                    }
                }
                
                yield return new WaitForFixedUpdate();
            }
            else{
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + deltaX, gameObject.transform.position.y, gameObject.transform.position.z + deltaZ);

                if((gameObject.transform.position - returnPos).magnitude < marginOfError){
                    stateFlags[flagIndex] = false;
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }

    private bool IsAtPath(){
        if( TestRange(gameObject.transform.position.z, lineOfMotion.CountY(gameObject.transform.position.x) - marginOfError, lineOfMotion.CountY(gameObject.transform.position.x) + marginOfError) ){
            if(pathLength + marginOfError >= (startPos - gameObject.transform.position).magnitude && pathLength + marginOfError >= (endPos - gameObject.transform.position).magnitude){
                return true;
            }
        }

        return false;
    }

    bool TestRange(float numberToCheck, float bottom, float top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    }
}
