/*
 * Unity is missing a touchable concept in its OO chain, meaning buttons without an image or text to click on cannot be interacted with.
 * This script fills in the missing concept and was found on https://stackoverflow.com/questions/36888780/how-to-make-an-invisible-transparent-button-work 
 *  from user Fattie on April 27, 2016
 */

using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(Touchable))]
public class Touchable_Editor : Editor
{
    public override void OnInspectorGUI() { }
}
#endif

public class Touchable:Text
{
    protected override void Awake() {
        base.Awake();
    }
}