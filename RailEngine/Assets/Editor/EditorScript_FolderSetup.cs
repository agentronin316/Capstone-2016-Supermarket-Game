using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorScript_FolderSetup : MonoBehaviour {
	[MenuItem("Tool Creation/Create Folders")]
	 public static void CreateFolders()
	 {
		AssetDatabase.CreateFolder ("Assets", "Materials");
		AssetDatabase.CreateFolder ("Assets", "Textures");
		AssetDatabase.CreateFolder ("Assets", "Prefabs");
		AssetDatabase.CreateFolder ("Assets", "Scripts");
		AssetDatabase.CreateFolder ("Assets", "Scenes");
		AssetDatabase.CreateFolder ("Assets", "Animations");
		AssetDatabase.CreateFolder ("Assets/Animations", "Animation Controllers");

		System.IO.File.WriteAllText (Application.dataPath + "/Materials/folderStructure.txt", "Materials: This Folder is for storing materials.");
		System.IO.File.WriteAllText (Application.dataPath + "/Textures/folderStructure.txt", "Textures: This Folder is for storing textures.");
		System.IO.File.WriteAllText (Application.dataPath + "/Prefabs/folderStructure.txt", "Prefabs: This Folder is for storing prefabs.");
		System.IO.File.WriteAllText (Application.dataPath + "/Scripts/folderStructure.txt", "Scripts: This Folder is for storing scripts.");
		System.IO.File.WriteAllText (Application.dataPath + "/Scenes/folderStructure.txt", "Scenes: This Folder is for storing scenes.");
		System.IO.File.WriteAllText (Application.dataPath + "/Animations/folderStructure.txt", "Animations: This Folder is for storing animations.");
		System.IO.File.WriteAllText (Application.dataPath + "/Animations/Animation Controllers/folderStructure.txt", "Animation Controllers: This Folder is for storing animation controllers.");

		AssetDatabase.Refresh ();
	 }

	[MenuItem("Tool Creation/Create Extended Folders")]
	public static void CreateExtendedFolders()
	{
		//declare big ugly string that will be reused in documentation
		string folderStructureContents = "The Effects folder is where you will place particle effects. Create a new folder for each particle effect."
			+ "/n/nThe Models folder is for raw models. These are for signle looping-animation models or for non-animated models."
			+ "/n/nThe Prefabs folder is for all prefabs. A new folder should be created for each level/world/scene the prefab is used in. "
			+ "Any prefab used in multiple levels/worlds/scenes should be placed in the common folder. Only one copy of each prefab should exist."
			+ "/n/nThe souldn folder is for all sounds. It should follow the same organization as the prefabs folder."
			+ "/n/nThe Texures folder is for all textrues that are applied seperately to models or changed at runtime.";
		//Create folders
		AssetDatabase.CreateFolder ("Assets", "Dynamic Assets");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets", "Resources");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Animations");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Animations", "Sources");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Animation Controllers");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Effects");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Models");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Models", "Character");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Models", "Environment");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Prefabs");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Prefabs", "Common");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Sounds");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Sounds", "Music");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Sounds/Music", "Common");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Sounds", "SFX");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources/Sounds/SFX", "Common");
		AssetDatabase.CreateFolder ("Assets/Dynamic Assets/Resources", "Textures");
		AssetDatabase.CreateFolder ("Assets", "Extensions");
		AssetDatabase.CreateFolder ("Assets", "Gizmos");
		AssetDatabase.CreateFolder ("Assets", "Plugins");
		AssetDatabase.CreateFolder ("Assets", "Scripts");
		AssetDatabase.CreateFolder ("Assets", "Shaders");
		AssetDatabase.CreateFolder ("Assets", "Static Assets");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Animations");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Animations", "Sources");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Animation Controllers");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Effects");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Models");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Models", "Character");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Models", "Environment");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Prefabs");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Prefabs", "Common");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Sounds");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Sounds", "Music");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Sounds/Music", "Common");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Sounds", "SFX");
		AssetDatabase.CreateFolder ("Assets/Static Assets/Sounds/SFX", "Common");
		AssetDatabase.CreateFolder ("Assets/Static Assets", "Textures");
		AssetDatabase.CreateFolder ("Assets", "Testing");
		//Add documentation files to folders
		System.IO.File.WriteAllText (Application.dataPath + "Dynamic Assets/Resources/folderStructure.txt", "Dynamic assets are assets that are loaded into"
		                             + " game at runtime./n/n" + folderStructureContents);
		System.IO.File.WriteAllText (Application.dataPath + "Editor/folderStructure.txt", "Editor: Editor scripts go in this folder");
		System.IO.File.WriteAllText (Application.dataPath + "Extensions/folderStructure.txt", "Extensions: This is a folder for third party assets");
		System.IO.File.WriteAllText (Application.dataPath + "Gizmos/folderStructure.txt", "Gizmos: Gizmo scripts go in this folder");
		System.IO.File.WriteAllText (Application.dataPath + "Plugins/folderStructure.txt", "Plugins: Plugin scripts go in this folder");
		System.IO.File.WriteAllText (Application.dataPath + "Scripts/folderStructure.txt", "Scripts: All other scripts go in this folder");
		System.IO.File.WriteAllText (Application.dataPath + "Shaders/folderStructure.txt", "Shaders: Shader scripts go in this folder");
		System.IO.File.WriteAllText (Application.dataPath + "Static Assests/folderStructure.txt", "Static assets are assets that are not loaded into"
		                             + " game at runtime./n/n" + folderStructureContents);
		System.IO.File.WriteAllText (Application.dataPath + "Testing/folderStructure.txt", "Testing: This folder is for temporary testing assets");
		
		AssetDatabase.Refresh ();




	}
}
