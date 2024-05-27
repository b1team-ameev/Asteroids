using HGS.Tools.States;
using UnityEditor;
using UnityEngine;

namespace HGS.Asteroids.Editor {

    [CustomEditor(typeof(StateMachine), true)]
    public class StateMachineEditor: UnityEditor.Editor
    {   
        public override void OnInspectorGUI() {

            StateMachine obj  = target as StateMachine;
            if (obj == null || obj.gameObject == null ) {

                return;

            }

            base.OnInspectorGUI();

            if (!Application.isPlaying) {

                return;

            }

            using (var verticalArea = new EditorGUILayout.VerticalScope()) {

                // EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Current State", obj?.CurrentState?.ToString());
                EditorGUILayout.LabelField("Current State - IsReady", obj?.CurrentState?.IsReady.ToString());
                EditorGUILayout.LabelField("Current State - IsExited", obj?.CurrentState?.IsExited.ToString());
                EditorGUILayout.LabelField("Current State - IsUseTransitions", (obj?.CurrentState as State<StateMachine>)?.IsUseTransitions.ToString());
            }

            this.Repaint();

        }

    }

}