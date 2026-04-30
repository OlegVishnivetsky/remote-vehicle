using UnityEngine;

namespace _Project._Code.Services.Input
{
    public interface IInputService
    {
        Vector2 Input { get; }
        Vector2 Look { get; }
        
        void Enable();
        void Disable();
    }
}