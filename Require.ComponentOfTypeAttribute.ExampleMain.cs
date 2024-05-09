using UnityEngine;

public partial class Require_ComponentOfTypeAttribute_ExampleMain : MonoBehaviour {
	[Require_ComponentOfType(typeof(ImInterfaceExample))]
	public Component target;
}