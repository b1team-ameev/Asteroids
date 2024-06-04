using HGS.Asteroids.ECS;
using UnityEditor;
using UnityEngine;

namespace HGS.Asteroids.Editor {

    [CustomEditor(typeof(World), true)]
    public class WorldEditor: UnityEditor.Editor
    {   
        public override void OnInspectorGUI() {

            World obj = target as World;
            if (obj == null || obj.gameObject == null ) {

                return;

            }

            base.OnInspectorGUI();

            if (!Application.isPlaying) {

                return;

            }

            using (var verticalArea = new EditorGUILayout.VerticalScope()) {

                if (obj.SystemStock != null) {

                    var systems = obj.SystemStock.SystemsForEditor;

                    int count = systems != null ? systems.Count : 0;
                    EditorGUILayout.LabelField($"System Stock ({count})", EditorStyles.boldLabel);

                    if (systems != null) {

                        foreach(var system in systems) {

                            if (system != null) {

                                string name = system.GetType().Name;

                                var entities = system.EntityFilter?.Entities;
                                count = entities != null ? entities.Count : 0; 

                                EditorGUILayout.LabelField($"System - {name} ({count})");

                            }

                        }

                    }

                    EditorGUILayout.Separator();

                }

                if (obj.EntityStock != null) {

                    int count = obj.EntityStock.EntitiesForEditor != null ? obj.EntityStock.EntitiesForEditor.Count : 0;
                    EditorGUILayout.LabelField($"Entity Stock ({count})", EditorStyles.boldLabel);

                    if (obj.EntityStock.EntitiesForEditor != null) {

                        foreach(var entity in obj.EntityStock.EntitiesForEditor) {

                            if (entity != null) {

                                string name = (entity.IdObject as GameObject)?.name;

                                if (string.IsNullOrEmpty(name)) {

                                    name = "Unknown";

                                }

                                EditorGUILayout.LabelField($"Entity - {name}");

                            }

                        }

                    }

                    EditorGUILayout.Separator();

                }

            }

            this.Repaint();

        }

    }

}