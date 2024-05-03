using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ForceBoxManager : MonoBehaviour
{
    public enum ForceBoxDirection
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    }

    public ForceBoxDirection forceBoxDirection;
    private AreaEffector2D AreaEffector2D;

    // Start is called before the first frame update
    void Start()
    {
        // récupérer le composant AreaEffector2D
        AreaEffector2D = GetComponent<AreaEffector2D>();
        ChangeForceDirection(forceBoxDirection.ToString());
        ChangeSpriteRotation(forceBoxDirection.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // fonction pour changer la direction de la force de AreaEffector2D en fonction de la direction
    public void ChangeForceDirection(string direction)
    {
        switch (direction)
        {
            case "TOP":
                AreaEffector2D.forceAngle = 90;
                break;
            case "BOTTOM":
                AreaEffector2D.forceAngle = 270;
                break;
            case "LEFT":
                AreaEffector2D.forceAngle = 180;
                break;
            case "RIGHT":
                AreaEffector2D.forceAngle = 0;
                break;
        }
        
    }

    // fonction pour changer la rotation du spriteRenderer enfant en fonction de la direction
    public void ChangeSpriteRotation(string direction)
    {
        switch (direction)
        {
            case "TOP":
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 90);
                break;
            case "BOTTOM":
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 270);
                break;
            case "LEFT":
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 180);
                break;
            case "RIGHT":
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    // onDrawGizmos renommer le gameObject en fonction de la direction dans la scène
    private void OnDrawGizmos()
    {
        gameObject.name = "ForceBox_" + forceBoxDirection.ToString();

        // change aussi la rotation du spriteRenderer enfant
        ChangeSpriteRotation(forceBoxDirection.ToString());
    }

}
