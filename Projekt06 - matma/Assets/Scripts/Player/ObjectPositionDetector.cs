using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionDetector : MonoBehaviour
{
    [SerializeField] GameObject objToCheck;

    void CheckPos(){
        Vector3 fromCubeToPlayer = VectorDifference(gameObject.transform.position, objToCheck.transform.position);
        

        //Method 1, four directions
        Debug.Log( $"M1: player is to the {( VectorDot(objToCheck.transform.right, fromCubeToPlayer) > 0 ? "right" : "left")} side of the cube" );
        Debug.Log( $"M1: player {(VectorDot(objToCheck.transform.forward, fromCubeToPlayer) > 0 ? "in front of" : "behind" )} the cube" );


        //Method 2, four directions
        Vector3 fromCubeToPlayerNormalized = VectorNormalization(fromCubeToPlayer);
        Vector3 fromCubeToPlayerLocalSpace = objToCheck.transform.InverseTransformVector( fromCubeToPlayerNormalized );
        float atan2InDeg = Mathf.Rad2Deg * Mathf.Atan2( fromCubeToPlayerLocalSpace.x, fromCubeToPlayerLocalSpace.z );

        Debug.Log( $"M2: player is to the {( atan2InDeg > 0 ? "right" : "left")} side of the cube" );
        Debug.Log( $"M2: player {(Mathf.Abs(atan2InDeg) < 90 ? "in front of" : "behind" )} the cube" );

        
        //Method 3, eight directions
        Dictionary<int, string> directionDict = new Dictionary<int, string>();

        directionDict.Add(0, "N");
        directionDict.Add(1, "NE");
        directionDict.Add(2, "E");
        directionDict.Add(3, "SE");
        directionDict.Add(4, "S");
        directionDict.Add(5, "SW");
        directionDict.Add(6, "W");
        directionDict.Add(7, "NW");

        float atan2InRad = Mathf.Atan2( fromCubeToPlayerLocalSpace.x, fromCubeToPlayerLocalSpace.z );
        int octant = Mathf.RoundToInt( 8 * atan2InRad / (2*Mathf.PI) + 8 ) % 8;

        Debug.Log( $"M3: player is {directionDict[octant]} of the cube" );
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            CheckPos();
        }
    }

    private Vector3 VectorDifference(Vector3 vec1, Vector3 vec2){
        Vector3 difference = new Vector3(vec1.x - vec2.x,
                                        vec1.y - vec2.y,
                                        vec1.z - vec2.z);
        
        return difference;
    }

    private float VectorMagnitude(Vector3 vec){
        float magnitude = Mathf.Sqrt(vec.x*vec.x + vec.y*vec.y + vec.z*vec.z);

        return magnitude;
    }

    private float VectorDistance(Vector3 vec1, Vector3 vec2){
        Vector3 difference = VectorDifference(vec1, vec2);

        float distance = VectorMagnitude(difference);

        return distance;
    }

    private Vector3 VectorNormalization(Vector3 vec){
        float vecMagnitude = VectorMagnitude(vec);

        Vector3 normalizedVec = new Vector3( vec.x/vecMagnitude, vec.y/vecMagnitude, vec.z/vecMagnitude);

        return normalizedVec;
    }

    private float VectorDot(Vector3 vec1, Vector3 vec2){
        float dot = vec1.x*vec2.x + vec1.y*vec2.y + vec1.z*vec2.z;

        return dot;
    }

    private float VectorAngleRad(Vector3 vec1, Vector3 vec2){
        Vector3 vec1Norm = VectorNormalization(vec1);
        Vector3 vec2Norm = VectorNormalization(vec2);

        float angle = Mathf.Acos(VectorDot(vec1Norm, vec2Norm));

        return angle;
    }
}
