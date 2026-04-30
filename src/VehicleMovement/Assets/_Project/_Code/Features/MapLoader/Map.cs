using UnityEngine;

namespace _Project._Code.Features.MapLoader
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        public Transform SpawnPoint => _spawnPoint;
    }
}