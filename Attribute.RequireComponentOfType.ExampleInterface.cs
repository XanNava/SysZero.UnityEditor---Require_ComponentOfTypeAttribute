using UnityEngine;

public class Require_ComponentOfTypeAttribute_ExampleInterface : MonoBehaviour, ImInterfaceExample {
	public void DoSomething() {
		Debug.Log("Did it.");
	}
}

public interface ImInterfaceExample {
	void DoSomething();
}
