using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(FlowFieldGenerator)), CanEditMultipleObjects]
public class FlowFieldGeneratorEditor : Editor {
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    // [MenuItem("Window/UI Toolkit/FlowFieldGeneratorEditor")]
    // public static void ShowExample()
    // {
    //     FlowFieldGeneratorEditor wnd = GetWindow<FlowFieldGeneratorEditor>();
    //     wnd.titleContent = new GUIContent("FlowFieldGeneratorEditor");
    // }

    public override VisualElement CreateInspectorGUI() {
        VisualElement root = new VisualElement();

        m_VisualTreeAsset.CloneTree(root);

        VisualElement inspectorFoldout = root.Q("Default_Inspector");

        // Attach a default Inspector to the Foldout.
        InspectorElement.FillDefaultInspector(inspectorFoldout, serializedObject, this);

        Button button = new Button();
        button.text = "Generate";

        FlowFieldGenerator flowFieldGenerator = target as FlowFieldGenerator;
        if (flowFieldGenerator) {
            button.clicked += () =>
            {
                flowFieldGenerator.CreateFlowField();
            };
        }
        root.Add(button);

        return root;
    }
}
         
         
                                                             