using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public Texture2D CenterZoneTexture;
    public Texture2D OuterZoneTexture;

    [Header("Radius Limit x=min et y=max")]
    public Vector2 radiusLimit = new Vector2(1f, 10f);

    private GameObject _objectMove;
    private CircleShape _objectToResize;
    private bool _isClicked = false;

    private Vector3 _worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if object is clicked move it to the mouse position
        if ( _isClicked && _objectMove != null )
        {
            _objectMove.transform.position = _worldPosition;
        }

        if ( _isClicked && _objectToResize != null )
        {
            //Calcul de la distance entre le pointer et le centre de l'objet avec Vector2.Distance

            float radius = Vector2.Distance( _objectToResize.transform.position, _worldPosition );
            _objectToResize.Radius = Mathf.Clamp( radius, radiusLimit.x, radiusLimit.y );
        }

        if ( !_isClicked )
        {
            _objectMove = null;
            _objectToResize = null;
        }
    }

    public void PointerMove( InputAction.CallbackContext context )
    {
        Vector2 pointerPosition = context.ReadValue<Vector2>();
        _ray = Camera.main.ScreenPointToRay( pointerPosition );
        
        RaycastHit2D hit = Physics2D.GetRayIntersection( _ray );

        _worldPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        _worldPosition.z = 0;

        if ( hit.collider != null )
        {
            if ( hit.collider.CompareTag( "CenterZone" ) )
            {

                Cursor.SetCursor( CenterZoneTexture, new Vector2(256, 256), CursorMode.Auto );

                _objectMove = hit.collider.transform.parent.gameObject;

            }
            else if( hit.collider.CompareTag( "OuterZone" ) )
            {

                Cursor.SetCursor( OuterZoneTexture, new Vector2(256, 256), CursorMode.Auto );

                _objectToResize = hit.collider.GetComponent<CircleShape>();

            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

    }

    public void PointerClick( InputAction.CallbackContext context )
    {

        switch ( context.phase )
        {
            case InputActionPhase.Started:
 
                break;
            case InputActionPhase.Performed:

                _isClicked = true;
                break;
            case InputActionPhase.Canceled:


                _isClicked = false;
                break;
            default:
                break;
        }
    }   

    private Ray _ray;
}
