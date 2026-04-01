using UnityEngine;

public class MouseWorldPosition : MonoBehaviour
{
    public static MouseWorldPosition Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public Vector3 GetPosition()
    {
        Ray mouseCameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //Get the mouse position on multiple layers in 3D space using Physics
        /*if(Physics.Raycast(mouseCameraRay, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }*/
        
        //Get the mouse position on basic 2D plane not using physics
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if(plane.Raycast(mouseCameraRay, out float distance))
        {
            return mouseCameraRay.GetPoint(distance);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
