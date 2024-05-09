using UnityEngine;

public partial class Require_ComponentOfTypeAttribute_ExampleMain : MonoBehaviour {
	[Require_ComponentOfType(typeof(ImInterfaceExample))]
	public Component target;
	private ImInterfaceExample _target;

	// Used for internal assignment from external references.
	public void Start() {
		// Note the order, to prioritize the serialized reference on initialization.
		if (target != null)
			_target = (ImInterfaceExample)target;
		if (target == null)
			_target = GetComponentInChildren<ImInterfaceExample>();
	}

	[AddComponentMenu("Call()")]
	public void Call() {
		_target.DoSomething();
	}
}
