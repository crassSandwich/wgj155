#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Top`. Inherits from `AtomDrawer&lt;TopVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TopVariable))]
    public class TopVariableDrawer : VariableDrawer<TopVariable> { }
}
#endif
