using UnityEngine;

public enum SoundType
{
    Music,
    Interact,
    Equip,
    Button,
    Inventory,
    Spawn,
    Slice,
    ShakePowder,
    ShakeFries,
    Transfer,
    Plant,
    PlantReady,
    Harvest,
    Transact,
    HappyCustomer,
    NeutralCustomer,
    MadCustomer,
    Collect,
    Fry,
    Swipe
}

[System.Serializable]
public class SoundData
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Single Channel Sounds")]
    public SoundData music;
    public SoundData interact;
    public SoundData equip;
    public SoundData button;
    public SoundData inventory;
    public SoundData spawn;
    public SoundData slice;
    public SoundData shakePowder;
    public SoundData shakeFries;
    public SoundData transfer;
    public SoundData plant;
    public SoundData plantReady;
    public SoundData harvest;
    public SoundData transact;
    public SoundData happyCustomer;
    public SoundData neutralCustomer;
    public SoundData madCustomer;
    public SoundData swipe;

    [Header("Multiple Channel Sounds")]
    public SoundData collect;
    public SoundData fry;

    [Header("Channel Settings")]
    public int multiChannelCount = 5;

    // Single channel AudioSources
    private AudioSource musicSource;
    private AudioSource interactSource;
    private AudioSource equipSource;
    private AudioSource buttonSource;
    private AudioSource inventorySource;
    private AudioSource spawnSource;
    private AudioSource sliceSource;
    private AudioSource shakePowderSource;
    private AudioSource shakeFriesSource;
    private AudioSource transferSource;
    private AudioSource plantSource;
    private AudioSource plantReadySource;
    private AudioSource harvestSource;
    private AudioSource transactSource;
    private AudioSource happyCustomerSource;
    private AudioSource neutralCustomerSource;
    private AudioSource madCustomerSource;
    private AudioSource swipeSource;

    // Multiple channel AudioSources
    private AudioSource[] collectSources;
    private AudioSource[] frySources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CreateAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateAudioSources()
    {
        // Create single-channel audio sources
        musicSource = CreateSingleAudioSource("Music", music, true);
        interactSource = CreateSingleAudioSource("Interact", interact);
        equipSource = CreateSingleAudioSource("Equip", equip);
        buttonSource = CreateSingleAudioSource("Button", button);
        inventorySource = CreateSingleAudioSource("Inventory", inventory);
        spawnSource = CreateSingleAudioSource("Spawn", spawn);
        sliceSource = CreateSingleAudioSource("Slice", slice);
        shakePowderSource = CreateSingleAudioSource("ShakePowder", shakePowder);
        shakeFriesSource = CreateSingleAudioSource("ShakeFries", shakeFries);
        transferSource = CreateSingleAudioSource("Transfer", transfer);
        plantSource = CreateSingleAudioSource("Plant", plant);
        plantReadySource = CreateSingleAudioSource("PlantReady", plantReady);
        harvestSource = CreateSingleAudioSource("Harvest", harvest);
        transactSource = CreateSingleAudioSource("Transact", transact);
        happyCustomerSource = CreateSingleAudioSource("HappyCustomer", happyCustomer);
        neutralCustomerSource = CreateSingleAudioSource("NeutralCustomer", neutralCustomer);
        madCustomerSource = CreateSingleAudioSource("MadCustomer", madCustomer);
        swipeSource = CreateSingleAudioSource("Swipe", swipe);

        // Create multi-channel audio sources
        collectSources = CreateAudioSourceArray("Collect", collect, multiChannelCount);
        frySources = CreateAudioSourceArray("Fry", fry, multiChannelCount, true);
    }

    private AudioSource[] CreateAudioSourceArray(string baseName, SoundData soundData, int count, bool loop = false)
    {
        AudioSource[] sources = new AudioSource[count];
        for (int i = 0; i < count; i++)
        {
            GameObject audioObj = new GameObject($"{baseName}_{i}");
            audioObj.transform.SetParent(transform);
            sources[i] = audioObj.AddComponent<AudioSource>();
            sources[i].clip = soundData.clip;
            sources[i].volume = soundData.volume;
            sources[i].playOnAwake = false;
            sources[i].loop = loop;
        }
        return sources;
    }

    private AudioSource CreateSingleAudioSource(string name, SoundData soundData, bool loop = false)
    {
        GameObject audioObj = new GameObject(name);
        audioObj.transform.SetParent(transform);
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = soundData.clip;
        source.volume = soundData.volume;
        source.playOnAwake = false;
        source.loop = loop;
        return source;
    }

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            // = = = = = = = = Single Channel = = = = = = = = 
            case SoundType.Music:
                if (!musicSource.isPlaying) musicSource.Play();
                break;
            case SoundType.Interact:
                interactSource.Play();
                break;
            case SoundType.Equip:
                equipSource.Play();
                break;
            case SoundType.Button:
                buttonSource.Play();
                break;
            case SoundType.Inventory:
                inventorySource.Play();
                break;
            case SoundType.Spawn:
                spawnSource.Play();
                break;
            case SoundType.Slice:
                sliceSource.Play();
                break;
            case SoundType.ShakePowder:
                shakePowderSource.Play();
                break;
            case SoundType.ShakeFries:
                shakeFriesSource.Play();
                break;
            case SoundType.Transfer:
                transferSource.Play();
                break;
            case SoundType.Plant:
                plantSource.Play();
                break;
            case SoundType.PlantReady:
                plantReadySource.Play();
                break;
            case SoundType.Harvest:
                harvestSource.Play();
                break;
            case SoundType.Transact:
                transactSource.Play();
                break;
            case SoundType.HappyCustomer:
                happyCustomerSource.Play();
                break;
            case SoundType.NeutralCustomer:
                neutralCustomerSource.Play();
                break;
            case SoundType.MadCustomer:
                madCustomerSource.Play();
                break;
            case SoundType.Swipe:
                swipeSource.Play();
                break;

            // = = = = = = = = Multiple Channel = = = = = = = = 
            case SoundType.Collect:
                AudioPlay(collectSources);
                break;
            case SoundType.Fry:
                AudioPlay(frySources);
                break;
        }
    }

    public void StopSound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Music:
                musicSource.Stop();
                break;
            case SoundType.Fry:
                foreach (var source in frySources)
                {
                    source.Stop();
                }
                break;
        }
    }

    void AudioPlay(AudioSource[] audioSources)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].Play();
                return;
            }
        }
        audioSources[0].Play();
    }
}