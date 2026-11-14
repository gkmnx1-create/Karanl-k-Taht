using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KaranlikTaht.Player;
using KaranlikTaht.CameraSystem;
using KaranlikTaht.UI;

namespace KaranlikTaht.EditorTools
{
    public static class PrototypeSceneBuilder
    {
        private const string SceneFolder = "Assets/Scenes";
        private const string ScenePath = SceneFolder + "/KaranlikTaht_Prototype.unity";

        [MenuItem("Karanlik Taht/Build Mobile Prototype Scene")]
        public static void Build()
        {
            if (!AssetDatabase.IsValidFolder(SceneFolder))
                AssetDatabase.CreateFolder("Assets", "Scenes");

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // Lighting and a simple ground plane.
            GameObject lightObject = new GameObject("Directional Light");
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.15f;
            lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Prototype Ground";
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = new Vector3(8f, 1f, 8f);

            // Temporary player placeholder. Replace its visual mesh with the selected character model later.
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player (Temporary Placeholder)";
            player.transform.position = new Vector3(0f, 1.05f, 0f);
            Object.DestroyImmediate(player.GetComponent<CapsuleCollider>());
            CharacterController controller = player.AddComponent<CharacterController>();
            controller.height = 2f;
            controller.radius = 0.42f;
            controller.center = new Vector3(0f, 1f, 0f);
            ThirdPersonMotor motor = player.AddComponent<ThirdPersonMotor>();

            // Main camera and orbit-follow behaviour.
            GameObject cameraObject = new GameObject("Main Camera");
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.fieldOfView = 60f;
            camera.nearClipPlane = 0.1f;
            camera.farClipPlane = 500f;
            cameraObject.AddComponent<AudioListener>();
            ThirdPersonCameraFollow cameraFollow = cameraObject.AddComponent<ThirdPersonCameraFollow>();
            cameraObject.transform.position = new Vector3(0f, 3f, -5f);

            // Canvas and event system.
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();

            GameObject canvasObject = new GameObject("Mobile Controls Canvas",
                typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            Canvas canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            // Right half transparent drag zone. Keep it behind the run button.
            GameObject cameraArea = CreateUIObject("Camera Touch Area", canvasObject.transform);
            RectTransform cameraRect = cameraArea.GetComponent<RectTransform>();
            cameraRect.anchorMin = new Vector2(0.5f, 0f);
            cameraRect.anchorMax = new Vector2(1f, 1f);
            cameraRect.offsetMin = Vector2.zero;
            cameraRect.offsetMax = Vector2.zero;
            Image cameraImage = cameraArea.AddComponent<Image>();
            cameraImage.color = new Color(1f, 1f, 1f, 0.01f);
            cameraImage.raycastTarget = true;
            CameraTouchArea touchArea = cameraArea.AddComponent<CameraTouchArea>();

            // Left joystick.
            GameObject joystickBackground = CreateUIObject("Joystick Background", canvasObject.transform);
            RectTransform bgRect = joystickBackground.GetComponent<RectTransform>();
            bgRect.anchorMin = new Vector2(0f, 0f);
            bgRect.anchorMax = new Vector2(0f, 0f);
            bgRect.pivot = new Vector2(0.5f, 0.5f);
            bgRect.sizeDelta = new Vector2(190f, 190f);
            bgRect.anchoredPosition = new Vector2(145f, 145f);
            Image bgImage = joystickBackground.AddComponent<Image>();
            bgImage.color = new Color(0.08f, 0.08f, 0.1f, 0.48f);

            GameObject joystickHandle = CreateUIObject("Joystick Handle", joystickBackground.transform);
            RectTransform handleRect = joystickHandle.GetComponent<RectTransform>();
            handleRect.anchorMin = new Vector2(0.5f, 0.5f);
            handleRect.anchorMax = new Vector2(0.5f, 0.5f);
            handleRect.pivot = new Vector2(0.5f, 0.5f);
            handleRect.sizeDelta = new Vector2(78f, 78f);
            handleRect.anchoredPosition = Vector2.zero;
            Image handleImage = joystickHandle.AddComponent<Image>();
            handleImage.color = new Color(0.75f, 0.75f, 0.8f, 0.85f);

            MobileJoystick joystick = joystickBackground.AddComponent<MobileJoystick>();
            SetObjectReference(joystick, "background", bgRect);
            SetObjectReference(joystick, "handle", handleRect);

            // Hold-to-run button.
            GameObject runButton = CreateUIObject("Run Button", canvasObject.transform);
            RectTransform runRect = runButton.GetComponent<RectTransform>();
            runRect.anchorMin = new Vector2(1f, 0f);
            runRect.anchorMax = new Vector2(1f, 0f);
            runRect.pivot = new Vector2(1f, 0f);
            runRect.sizeDelta = new Vector2(145f, 90f);
            runRect.anchoredPosition = new Vector2(-55f, 65f);
            Image runImage = runButton.AddComponent<Image>();
            runImage.color = new Color(0.35f, 0.12f, 0.12f, 0.85f);
            RunHoldButton runHold = runButton.AddComponent<RunHoldButton>();
            runHold.SetMotor(motor);

            GameObject labelObject = CreateUIObject("Run Label", runButton.transform);
            RectTransform labelRect = labelObject.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;
            Text label = labelObject.AddComponent<Text>();
            label.text = "KOŞ";
            label.alignment = TextAnchor.MiddleCenter;
            label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            label.fontSize = 30;
            label.color = Color.white;
            label.raycastTarget = false;

            // Wire references.
            SetObjectReference(motor, "cameraTransform", cameraObject.transform);
            SetObjectReference(motor, "joystick", joystick);
            SetObjectReference(cameraFollow, "target", player.transform);
            SetObjectReference(touchArea, "cameraFollow", cameraFollow);

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.SaveAssets();
            Selection.activeGameObject = player;
            EditorUtility.DisplayDialog("Karanlık Taht",
                "Mobil prototip sahnesi oluşturuldu. Scene'i açıp Play ile Editor'de deneyin; Android testi için ayrıca Build Settings ve Android modülü gerekir.",
                "Tamam");
        }

        private static GameObject CreateUIObject(string name, Transform parent)
        {
            GameObject go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            return go;
        }

        private static void SetObjectReference(Object target, string propertyName, Object value)
        {
            SerializedObject serialized = new SerializedObject(target);
            SerializedProperty property = serialized.FindProperty(propertyName);
            if (property != null)
            {
                property.objectReferenceValue = value;
                serialized.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}