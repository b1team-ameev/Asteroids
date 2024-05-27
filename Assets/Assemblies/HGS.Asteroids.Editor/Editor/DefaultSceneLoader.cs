using UnityEditor;
using UnityEditor.SceneManagement;

namespace HGS.Asteroids.Editor {

    [InitializeOnLoad]
    public static class DefaultSceneLoader
    {
        static DefaultSceneLoader(){
            
            // EditorApplication.playModeStateChanged += LoadDefaultScene;

        }

        static void LoadDefaultScene(PlayModeStateChange state){

            if (state == PlayModeStateChange.ExitingEditMode) {

                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            }

            //Debug.Log(EditorSceneManager.GetActiveScene().buildIndex);

            if (state == PlayModeStateChange.EnteredPlayMode && EditorSceneManager.GetActiveScene().buildIndex > 0) {

                EditorSceneManager.LoadScene(0);

            }

        }
    }

}