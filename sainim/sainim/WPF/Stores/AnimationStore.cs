using sainim.Models;
using sainim.Models.Enums;
using sainim.Models.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace sainim.WPF.Stores
{
    public class AnimationStore : INotifyPropertyChanged
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
                OnPropertyChanged(nameof(CurrentFrameIndex));
            }
        }

        // Play settings
        public int FrameRate { get; set; } = 12;
        public bool Repeating { get; set; } = false;
        public SelectableLayerTypes SelectableLayerTypes { get; } = new();

        // References to data
        public ObservableCollection<Frame?> AnimationSequence { get; } = [];  // When an image is NOT loaded, this collection has 0 elements and is thus not interactable.
                                                                              // When an image is loaded, empty spaces in the animation sequence
                                                                              // are represented by null and are interactable.

        public Frame? CurrentFrame => AnimationSequence[CurrentFrameIndex];

        // Saved to reduce repetition
        public List<Frame?> EmptyInteractableAnimationSequence { get; } = [];

        // Events
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event Action? AnimationDataLoaded;
        public void OnAnimationDataLoaded() => AnimationDataLoaded?.Invoke();

        public AnimationStore(OriginalImageStore originalImageStore)
        {
            EmptyInteractableAnimationSequence.AddRange(Enumerable.Repeat<Frame?>(null, FrameSpaceCount));

            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += LoadAnimationData;
        }

        public void LoadAnimationData()
        {
            var backgroundElements = _originalImageStore.CurrentImage!.GetElementsInPlacement(Placement.Background);
            var foregroundElements = _originalImageStore.CurrentImage!.GetElementsInPlacement(Placement.Foreground);
            var frames = _originalImageStore.CurrentImage!.Frames;
            SelectableLayerTypes.UpdateLayerTypeCollection(backgroundElements, foregroundElements, frames);
            LoadDefaultAnimationSequence();
            OnAnimationDataLoaded();
        }

        public void LoadDefaultAnimationSequence()
        {
            ResetAnimationSequenceToEmptyInteractableState();

            foreach (var (frame, i) in _originalImageStore.CurrentImage!.Frames.WithIndex())
                AnimationSequence[i] = frame;
        }

        private void ResetAnimationSequenceToEmptyInteractableState()
        {
            AnimationSequence.Clear();
            AnimationSequence.AddRange(EmptyInteractableAnimationSequence);
        }
    }
}