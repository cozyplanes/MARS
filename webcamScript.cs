/* 

*   Copyright 2017 cozyplanes
*
*   Licensed under the Apache License, Version 2.0 (the "License");
*   you may not use this file except in compliance with the License.
*   You may obtain a copy of the License at
*
*       http://www.apache.org/licenses/LICENSE-2.0
*
*   Unless required by applicable law or agreed to in writing, software
*   distributed under the License is distributed on an "AS IS" BASIS,
*   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*   See the License for the specific language governing permissions and
*   limitations under the License.

*/

// Create new 3D Object (Plane) and assign that to GameObject in inspector

using UnityEngine; // For almost everything

// Do not change the class name, or the base class that Unity extends it
public class webcamScript : MonoBehaviour
{
	// Declare variables, not war ( :D )
	public GameObject webCameraPlane;

	// Initialization
	void Start ()
	{
		// Do the bottom thing when the application is running in a mobile platform
		if (Application.isMobilePlatform) 
		{
			// Create a Gameobject (cameraParent) at runtime
			// camParent : Name of just created GameObject
			GameObject cameraParent = new GameObject("camParent");

			// Set the GameObject (cameraParent) position to the Main Camera's position
			cameraParent.transform.position = this.transform.position;

			// Set the Main Camera's parent to the Gameobject (cameraParent)
			this.transform.parent = cameraParent.transform;

			// Rotate the transform of the GameObject (cameraParent) according to value in Rotate()
			// Rotate() parameters : void Transform.Rotate(Vector3 axis, float angle) (+ 5 overloads)
			cameraParent.transform.Rotate(Vector3.right, 90); 
		}

		// Enable gyro
		Input.gyro.enabled = true;

		// Add onClick for fireBtn
		fireBtn.onClick.AddListener(OnButtonDown);

		/////////////////////////////////////////////////////////////

		// Create new WebCamTexture (webCameraTexture)
		WebCamTexture webCameraTexture = new WebCamTexture();

		// Set the texture of the WebCamTexture by the material fetched from the WebCamTexture (webCameraTexture) MeshRenderer
		webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;

		// Run WebCamTexture (webCameraTexture)
		webCameraTexture.Play();
	}

	// This method is called once per frame
	void Update ()
	{
		// Set Quaternion every 1 frame
		// Quaternion parameters : Quaternion.Quaternion(float x, float y, float z, float w) (+ 1 overload)
		Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);

		// Set Main Camera's localRotation matched to Quaternion (cameraRotation)
		// localRotation : The rotation of the transform relative to the parent transform's rotation.
		this.transform.localRotation = cameraRotation;
	}
}
