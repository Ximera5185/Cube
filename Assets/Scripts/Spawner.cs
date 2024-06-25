using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float DivisorNumber = 2;

    [SerializeField] private Cube _cube;
    [SerializeField] private float _exsplosionRadius;
    [SerializeField] private float _explosionForce;
    private ColorGenerator _colorGenerater;
    private RandomNumberGenerator _randomNamber;

    private void Awake()
    {
        _colorGenerater = new ColorGenerator();

        _randomNamber = new RandomNumberGenerator();
    }

    private void Start()
    {
        float startScaleX = 3.0f;
        float startScaleY = 3.0f;
        float startScaleZ = 3.0f;
        float separationChance = 1.0f;

        Vector3 startPositionCube = Vector3.zero;

        Vector3 startScaleCube = new Vector3(startScaleX, startScaleY, startScaleZ);

        SpawnCube(startPositionCube, startScaleCube, separationChance);
    }

    private void OnRemoved(Cube cube)
    {
        float minValueChanse = 0f;
        float maxValueChanse = 1.0f;

        Transform currentTransform = cube.transform;

        Vector3 scale = cube.transform.localScale / DivisorNumber;

        float chance = cube.SeparationChance / DivisorNumber;

        if (cube.SeparationChance >= _randomNamber.GetRandomNamber(minValueChanse, maxValueChanse))
        {
            SpawnCube(currentTransform.position, scale, chance);
        }
        else
        {
           //float radius = cube.transform.localScale * 2f;

            cube.Explode(_explosionForce, _exsplosionRadius);
        }

        cube.Removed -= OnRemoved;
    }

    private void SpawnCube(Vector3 position, Vector3 currentScale, float chanse)
    {
        int minValueCubes = 2;
        int maxValueCubes = 7;
        int quantityCubes = _randomNamber.GetRandomNamber(minValueCubes, maxValueCubes);

        for (int i = 0; i < quantityCubes; i++)
        {
            Cube cube = Instantiate(_cube, position, Quaternion.identity);

            cube.transform.localScale = currentScale;

            cube.Init(chanse, _colorGenerater.GetRandomColor());

            cube.AddForse(_explosionForce, _exsplosionRadius);
           
            cube.Removed += OnRemoved;
        }
    }
}