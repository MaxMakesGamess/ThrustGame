using System.Diagnostics;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other){
        switch (other.gameObject.tag){
            case "obstacle":
                UnityEngine.Debug.Log("You Crashed");
                break;
            case "Friendly":
                UnityEngine.Debug.Log("You landed on a launch Pad");
                break;
            case "Finish":
                UnityEngine.Debug.Log("You completed the level");
                break;
            default:
                UnityEngine.Debug.Log("Idk what you doin");
                break;
        }
    }
}
