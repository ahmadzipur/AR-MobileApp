using UnityEditor;
using UnityEngine;

public class CustomMenuObjects
{
    [MenuItem("GameObject/3D Object/Fox Model", false, 10)]
    static void CreateFoxModel()
    {
        // Pastikan path ini sesuai persis dengan lokasi prefab kamu
        string prefabPath = "Assets/Fox/Prefabs/Fox.prefab";

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab != null)
        {
            // Buat instance prefab
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            // Letakkan di depan kamera SceneView (biar langsung kelihatan)
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                Camera cam = sceneView.camera;
                if (cam != null)
                {
                    // Munculkan 1.5 meter di depan kamera SceneView
                    // Tentukan posisi di depan kamera
                    Vector3 spawnPos = cam.transform.position + cam.transform.forward * 1.5f;

                    // Pastikan model tetap tegak lurus di sumbu Y (tidak ikut kemiringan kamera)
                    spawnPos.y = cam.transform.position.y - 0.2f; // sedikit di bawah pandangan kamera

                    instance.transform.position = spawnPos;

                    // Biar model menghadap kamera
                    Vector3 lookAt = cam.transform.position;
                    lookAt.y = instance.transform.position.y;
                    instance.transform.LookAt(lookAt);
                    instance.transform.Rotate(0, 180, 0); // sesuaikan arah depan model

                }
            }

            Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
            Selection.activeObject = instance;
        }
        else
        {
            Debug.LogError("❌ Prefab not found at: " + prefabPath);
        }
    }
}
