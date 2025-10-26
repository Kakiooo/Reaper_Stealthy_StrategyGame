using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FOW_Enemy))]
public class FOW_Editor : Editor
{
    private void OnSceneGUI()
    {
        ShowVisionCircle();
    }
    void ShowVisionCircle()
    {
        FOW_Enemy fow = (FOW_Enemy)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.ViewRadius);

        Vector3 firstDir=fow.AngleDir(fow.ViewAngle/2,false);
        Vector3 secondDir = fow.AngleDir(-fow.ViewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + firstDir * fow.ViewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position+secondDir*fow.ViewRadius);

        Handles.color=Color.red;
        //for (int i = 0; i < fow.VisibleObjects.Count-1; i++)
        //{
        //    Handles.DrawLine(fow.transform.position, fow.transform.position + fow.VisibleObjects[i].transform.position);
        //} 
        foreach(Transform transform in fow.VisibleObjects)
        {
            Handles.DrawLine(fow.transform.position, transform.position);
        }
        
    }
}
