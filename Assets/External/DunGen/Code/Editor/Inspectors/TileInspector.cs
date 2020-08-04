﻿using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace DunGen.Editor
{
    [CustomEditor(typeof(Tile))]
    public class TileInspector : UnityEditor.Editor
    {
		#region Labels

		private static class Label
		{
			public static readonly GUIContent AllowRotation = new GUIContent("Allow Rotation", "If checked, this tile is allowed to be rotated by the dungeon gennerator. This setting can be overriden globally in the dungeon generator settings");
			public static readonly GUIContent RepeatMode = new GUIContent("Repeat Mode", "Determines how a tile is able to repeat throughout the dungeon. This setting can be overriden globally in the dungeon generator settings");
			public static readonly GUIContent OverrideAutomaticTileBounds = new GUIContent("Override Automatic Tile Bounds", "DunGen automatically calculates a bounding volume for tiles. Check this option if you're having problems with the automatically generated bounds.");
			public static readonly GUIContent TileBoundsOverride = new GUIContent("Overridden Bounds", "If specified, DunGen will use these boundary values for this tile instead of automatically calculating its own");
			public static readonly GUIContent FitToTile = new GUIContent("Fit to Tile", "Uses DunGen's automatic bounds generating to try to fit the bounds to the tile.");
			public static readonly GUIContent Entrance = new GUIContent("Entrance", "If set, DunGen will always use this doorway as the entrance to this tile.");
			public static readonly GUIContent Exit = new GUIContent("Exit", "If set, DunGen will always use this doorway as the first exit from this tile");
		}

		#endregion

		private SerializedProperty allowRotation;
		private SerializedProperty repeatMode;
		private SerializedProperty overrideAutomaticTileBounds;
		private SerializedProperty tileBoundsOverride;
		private SerializedProperty entrance;
		private SerializedProperty exit;

		private BoxBoundsHandle overrideBoundsHandle;


		private void OnEnable()
		{
			allowRotation = serializedObject.FindProperty("AllowRotation");
			repeatMode = serializedObject.FindProperty("RepeatMode");
			overrideAutomaticTileBounds = serializedObject.FindProperty("OverrideAutomaticTileBounds");
			tileBoundsOverride = serializedObject.FindProperty("TileBoundsOverride");
			entrance = serializedObject.FindProperty("Entrance");
			exit = serializedObject.FindProperty("Exit");


#if UNITY_2017_1_OR_NEWER
			overrideBoundsHandle = new BoxBoundsHandle();
#else
			overrideBoundsHandle = new BoxBoundsHandle(0);
#endif

			overrideBoundsHandle.SetColor(Color.red);
		}

		public override void OnInspectorGUI()
        {
			var tile = (Tile)target;

			serializedObject.Update();

            EditorGUILayout.PropertyField(allowRotation, Label.AllowRotation);
			EditorGUILayout.PropertyField(repeatMode, Label.RepeatMode);
			EditorGUILayout.PropertyField(overrideAutomaticTileBounds, Label.OverrideAutomaticTileBounds);

			if (overrideAutomaticTileBounds.boolValue)
			{
				EditorGUILayout.PropertyField(tileBoundsOverride, Label.TileBoundsOverride);

				if(GUILayout.Button(Label.FitToTile))
					tileBoundsOverride.boundsValue = tile.transform.InverseTransformBounds(UnityUtil.CalculateObjectBounds(tile.gameObject, false, false));
			}

			EditorGUILayout.BeginVertical("box");
			EditorGUILayout.HelpBox("You can optionally designate doorways as the entrance / exit for this tile", MessageType.Info);

			EditorUtil.ObjectFieldLayout(entrance, Label.Entrance, typeof(Doorway), true, false);
			EditorUtil.ObjectFieldLayout(exit, Label.Exit, typeof(Doorway), true, false);

			EditorGUILayout.EndVertical();

			serializedObject.ApplyModifiedProperties();
        }

		private void OnSceneGUI()
		{
			if (!overrideAutomaticTileBounds.boolValue)
				return;

			var tile = (Tile)target;
			overrideBoundsHandle.center = tileBoundsOverride.boundsValue.center;
			overrideBoundsHandle.size = tileBoundsOverride.boundsValue.size;

			EditorGUI.BeginChangeCheck();

			using (new Handles.DrawingScope(tile.transform.localToWorldMatrix))
			{
				overrideBoundsHandle.DrawHandle();
			}

			if (EditorGUI.EndChangeCheck())
			{
				tileBoundsOverride.boundsValue = new Bounds(overrideBoundsHandle.center, overrideBoundsHandle.size);
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}