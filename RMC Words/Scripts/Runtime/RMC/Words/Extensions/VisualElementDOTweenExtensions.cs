using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;

namespace RMC.Words.Extensions
{
    /// <summary>
    /// Extension methods for UnityEngine.VisualElement.
    /// </summary>
    public static class VisualElementDOTweenExtensions
    {
        public static async void AddRemoveToClassListAsync(this VisualElement visualElement, string className,
            int millisecondsDelay, int millisecondsDuration)
        {
            
            await UniTask.Delay(millisecondsDelay);
            
            visualElement.AddToClassList(className);

            await UniTask.Delay(millisecondsDuration);

            visualElement.RemoveFromClassList(className);
        }
    }
}