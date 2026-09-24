using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls how much foliage is visible by enabling/disabling a percentage
/// of the child GameObjects under a single assigned "foliage parent" object.
///
/// Usage:
///   - Assign the parent object (whose children are the foliage pieces) in the Inspector.
///   - Call SetFoliageAmount(0f) to disable all foliage.
///   - Call SetFoliageAmount(0.5f) to enable roughly half of it.
///   - Call SetFoliageAmount(1f) to enable all of it.
/// </summary>
public class FoliageDensityController : MonoBehaviour
{
    [Tooltip("The parent object whose direct children are treated as the foliage objects.")]
    [SerializeField] private GameObject foliageParent;

    [Tooltip("The slider that will adjust this controller.")]
    [SerializeField] private Slider slider;

    [Tooltip("If true, children are shuffled once at Start so enabling a low percentage " +
             "doesn't always keep the same subset (e.g. just the first few children).")]
    [SerializeField] private bool randomizeOrder = true;

    [Tooltip("Seed for the shuffle so results are reproducible. Ignored if randomizeOrder is false.")]
    [SerializeField] private int randomSeed = 12345;

    private readonly List<GameObject> foliageObjects = new List<GameObject>();
    private float lastAppliedAmount = -1f;

    private void Awake()
    {
        CacheFoliageObjects();
    }

    private void Start()
    {
        UpdateFoliage();
    }

    private void OnValidate()
    {
        // Lets you drag the slider in the Inspector during play mode and see it update live.
        if (Application.isPlaying && foliageObjects.Count > 0)
        {
            UpdateFoliage();
        }
    }

    /// <summary>
    /// Gathers the direct children of foliageParent into the internal list.
    /// Call this again (e.g. via RefreshFoliageList) if children are added/removed at runtime.
    /// </summary>
    public void CacheFoliageObjects()
    {
        foliageObjects.Clear();

        if (foliageParent == null)
        {
            Debug.LogWarning($"{nameof(FoliageDensityController)}: No foliage parent assigned.", this);
            return;
        }

        foreach (Transform child in foliageParent.transform)
        {
            foliageObjects.Add(child.gameObject);
        }

        if (randomizeOrder)
        {
            ShuffleDeterministic(foliageObjects, randomSeed);
        }
    }

    /// <summary>
    /// Re-scans the foliage parent's children and re-applies the current amount.
    /// Call this if you add/remove foliage objects at runtime.
    /// </summary>
    public void RefreshFoliageList()
    {
        CacheFoliageObjects();
        lastAppliedAmount = -1f; // force re-apply
        UpdateFoliage();
    }

    /// <summary>
    /// Sets how much foliage is enabled.
    /// </summary>
    /// <param name="normalizedAmount">0 = all disabled, 0.5 = half enabled, 1 = all enabled.</param>
    public void UpdateFoliage()
    {
        float value = Mathf.Clamp01(slider.value);

        if (foliageObjects.Count == 0)
        {
            // Nothing cached yet (e.g. called before Awake) — try to cache now.
            CacheFoliageObjects();
            if (foliageObjects.Count == 0)
            {
                return;
            }
        }

        int totalCount = foliageObjects.Count;
        int enabledCount = Mathf.RoundToInt(value * totalCount);

        for (int i = 0; i < totalCount; i++)
        {
            bool shouldBeEnabled = i < enabledCount;
            GameObject obj = foliageObjects[i];
            if (obj != null && obj.activeSelf != shouldBeEnabled)
            {
                obj.SetActive(shouldBeEnabled);
            }
        }

        lastAppliedAmount = value;
    }

    /// <summary>Returns the last amount value passed to SetFoliageAmount.</summary>
    public float GetFoliageAmount() => slider.value;

    /// <summary>Total number of foliage objects currently tracked.</summary>
    public int FoliageCount => foliageObjects.Count;

    // Fisher-Yates shuffle with a fixed seed, so the "which ones turn on first"
    // order is randomized but consistent between runs.
    private static void ShuffleDeterministic(List<GameObject> list, int seed)
    {
        System.Random rng = new System.Random(seed);
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}