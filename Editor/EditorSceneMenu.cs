using LevelSaveSystem;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace Base.Init
{
	[InitializeOnLoad]
	public static class EditorSceneMenu
	{
		#region Variables
		static int activeSceneIndex = 0;
		#endregion
		#region Properties 
		#endregion
		#region Editor Methods
		static EditorSceneMenu()
		{
			SceneView.duringSceneGui += OnSceneGUI;
		}
		#endregion
		#region My Methods

		private static void OnSceneGUI(SceneView sceneView)
		{
			GUILayout.BeginArea(new Rect(10, 50, 150, 5000));
			for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
			{
				GUILayout.BeginHorizontal(EditorStyles.toolbar);
				if (EditorBuildSettings.scenes[i].enabled)
				{
					if (GUILayout.Button($"{System.IO.Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path)}", GUILayout.Height(20)))
					{
						OpenScene(EditorBuildSettings.scenes[i].path, false);
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			Handles.EndGUI();
		}
		private static void OpenScene(string path, bool startGame)
		{
			if (!System.IO.File.Exists(path))
			{
				Debug.LogError($"Scene not found at path: {path}");
				return;
			}

			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				EditorSceneManager.OpenScene(path);
				Debug.Log($"Opened scene: {path}");
			}

			if (startGame)
				StartGame();
		}
		private static void StartGame()
		{
			if (!EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = true;
			}
			else
			{
				Debug.LogWarning("Game is already running!");
			}
		}
		#endregion
	}
}