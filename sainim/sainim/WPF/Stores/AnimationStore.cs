using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.ViewModels.Elements;
using System.Collections.ObjectModel;

namespace sainim.WPF.Stores
{
    public class AnimationStore
    {
        private readonly OriginalImageStore _originalImageStore;

        // Frame numbering
        public int MaxFrameIndex { get; } = 255;
        public int FrameSpaceCount => MaxFrameIndex + 1;

        private int _currentFrameIndex = 0;
        public int CurrentFrameIndex
        {
            get => _currentFrameIndex;
            set
            {
                if (value < 0 || value > MaxFrameIndex)
                    throw new ArgumentException($"Attempted to set {nameof(CurrentFrameIndex)} outside possible range of 0 to {nameof(MaxFrameIndex)}.");

                _currentFrameIndex = value;
                OnCurrentFrameIndexChanged();
            }
        }

        // Play settings
        public int FrameRate { get; set; } = 12;
        public bool Repeating { get; set; } = false;
        public ObservableCollection<SelectableOption> SelectableLayerTypes { get; } = [];
        public List<string> GetSelectedLayerTypes() => SelectableLayerTypes.Where(layerType => layerType.IsSelected)
                                                                         .Select(layerType => layerType.Name)
                                                                         .ToList();

        // References to data
        public ObservableCollection<Frame?> AnimationSequence { get; } = [];  // When an image is NOT loaded, this collection has 0 elements and is thus not interactable.
                                                                              // When an image is loaded, empty spaces in the animation sequence
                                                                              // are represented by null and are interactable.

        public Frame? CurrentFrame => AnimationSequence[CurrentFrameIndex];

        // Saved to reduce repetition
        public List<Frame?> EmptyInteractableAnimationSequence { get; } = [];

        // Events
        public event Action FrameSequenceModified;
        public void OnFrameSequenceModified() => FrameSequenceModified?.Invoke();

        public event Action CurrentFrameIndexChanged;
        public void OnCurrentFrameIndexChanged() => CurrentFrameIndexChanged?.Invoke();


        public AnimationStore(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += LoadDefaultAnimationSequence;

            EmptyInteractableAnimationSequence.AddRange(Enumerable.Repeat<Frame?>(null, FrameSpaceCount));
        }

        private void ResetAnimationSequenceToEmptyInteractableState()
        {
            AnimationSequence.Clear();
            AnimationSequence.AddRange(EmptyInteractableAnimationSequence);
        }

        public void LoadDefaultAnimationSequence()
        {
            ResetAnimationSequenceToEmptyInteractableState();

            foreach (var (frame, i) in _originalImageStore.CurrentImage!.Frames.WithIndex())
                AnimationSequence[i] = frame;

            OnFrameSequenceModified();
            CurrentFrameIndex = 0;
        }
    }
}