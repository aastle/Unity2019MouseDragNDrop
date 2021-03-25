# Unity 2019 Drag and Drop
March 23, 2021
Unity 2D 2019.4 code implementing drag and drop of sprites to their targets.
Some components are added at runtime in code.  These components are Audio Source, Box Collider 2D and Rigid Body 2D.
This is done so the developer doesn't have to remember to add them at design time.

March 24, 2021
The hit detection implementation used a comparision of dropped game object center points offset by a small value. That has been replaced with detecting overlapping Collider components on the target and the dropped game object.


March 25, 2021
A custom, overriden UnityEvent class was added to the ObjectMouseDrag class.  This custom UnityEvent class adds a single input parameter of the type string. This UnityEvent is used so that when the current game object is dropped on its target, it will raise the custom Unity event and invoke an event handler in the GameManager class.  The event handler adds the string tag from the Unity event to a list of game object tags.  The GameManager event handler then checks to see if all of the ObjectMouseDrag script components from each draggable game object have a true value for the static "locked" property of the ObjectMouseDrag script component.
