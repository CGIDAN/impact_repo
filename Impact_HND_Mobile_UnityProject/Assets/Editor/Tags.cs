using UnityEditor;
using UnityEngine;
using System.Collections;

public class Tags : AssetPostprocessor
{

	static Tags()
	{
		Create();
	}

	/*
	* The asset need the Layer Ground on User Layer 8 and the Layer Enemy on User Layer 9
	* This script starts automatically and set the Layers to the project settings
	*/
	static void Create()
	{

		SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

		SerializedProperty it = tagManager.GetIterator();
		bool showChildren = true;

		bool playerIsSet = false;
		bool playerChildIsSet = false;
		bool groundIsSet = false;

		// Unity 4.x
		while (it.NextVisible(showChildren))
		{

			bool isSet = false;

			if (it.name.Contains("User Layer"))
			{

				if (it.stringValue.Equals("Player"))
				{
					playerIsSet = true;
				}

				if (it.stringValue.Equals("PlayerChild"))
				{
					playerIsSet = true;
				}

				if (it.stringValue.Equals("Ground"))
				{
					groundIsSet = true;
				}

				if (it.stringValue.Equals(""))
				{

					if (!isSet)
					{
						if (!playerIsSet)
						{
							playerIsSet = true;
							it.stringValue = "Player";
							isSet = true;
						}
					}

					if (!isSet)
					{
						if (!playerChildIsSet)
						{
							playerChildIsSet = true;
							it.stringValue = "PlayerChild";
							isSet = true;
						}
					}

					if (!isSet)
					{
						if (!groundIsSet)
						{
							groundIsSet = true;
							it.stringValue = "Ground";
							isSet = true;
						}
					}
				}
			}
		}

		// Unity 5.x
		SerializedProperty layerProp = tagManager.FindProperty("layers");

		for (int i = 8; i <= 31; i++)
		{

			SerializedProperty sp = layerProp.GetArrayElementAtIndex(i);

			if (sp != null)
			{
				if (sp.stringValue.Equals("Player"))
				{
					playerIsSet = true;
				}
				if (sp.stringValue.Equals("PlayerChild"))
				{
					playerChildIsSet = true;
				}
				if (sp.stringValue.Equals("Ground"))
				{
					groundIsSet = true;
				}
			}
		}



		if (!playerIsSet)
		{

			for (int i = 8; i <= 31; i++)
			{

				SerializedProperty sp = layerProp.GetArrayElementAtIndex(i);

				if (sp != null)
				{
					if (sp.stringValue.Equals(""))
					{
						sp.stringValue = "Player";
						playerIsSet = true;
						break;
					}
				}
			}
		}

		if (!playerChildIsSet)
		{

			for (int i = 8; i <= 31; i++)
			{

				SerializedProperty sp = layerProp.GetArrayElementAtIndex(i);

				if (sp != null)
				{
					if (sp.stringValue.Equals(""))
					{
						sp.stringValue = "PlayerChild";
						playerIsSet = true;
						break;
					}
				}
			}
		}

		if (!groundIsSet)
		{

			for (int i = 8; i <= 31; i++)
			{

				SerializedProperty sp = layerProp.GetArrayElementAtIndex(i);

				if (sp != null)
				{
					if (sp.stringValue.Equals(""))
					{
						sp.stringValue = "Ground";
						groundIsSet = true;
						break;
					}
				}
			}
		}


		SerializedProperty tagsProp = tagManager.FindProperty("tags");

		bool foundPlanet = false;
		for (int i = 0; i < tagsProp.arraySize; i++)
		{
			SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
			if (t.stringValue.Equals("Planet")) { foundPlanet = true; break; }
		}

		// if not found, add it
		if (!foundPlanet)
		{
			tagsProp.InsertArrayElementAtIndex(0);
			SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
			n.stringValue = "Planet";
		}

		bool foundEnemy = false;
		for (int i = 0; i < tagsProp.arraySize; i++)
		{
			SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
			if (t.stringValue.Equals("Enemy")) { foundEnemy = true; break; }
		}

		// if not found, add it
		if (!foundEnemy)
		{
			tagsProp.InsertArrayElementAtIndex(0);
			SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
			n.stringValue = "Enemy";
		}


		tagManager.ApplyModifiedProperties();
		tagManager.Update();
	}
}