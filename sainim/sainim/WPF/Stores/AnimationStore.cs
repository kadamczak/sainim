using sainim.Models;
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
        private int _frameRate = 12;
        public int FrameRate
        {
            get => _frameRate;
            set
            {
                if (value < 1 || value > 60)
                    return;

                _frameRate = value;
                OnPropertyChanged(nameof(FrameRate));
            }
        }
        public double GetMillisecondsBetweenFrames() => 1000.0 / FrameRate;

        public bool Repeating { get; set; } = false;
        public SelectableLayerTypes SelectableLayerTypes { get; } = new();

        // References to data
        public ObservableCollection<Frame?> AnimationSequence { get; } = [];  // When an image is NOT loaded, this collection has 0 elements and is thus not interactable.
                                                                              // When an image is loaded, empty spaces in the animation sequence
                                                                              // are represented by null and are interactable.

        public Frame? CurrentFrame => AnimationSequence[CurrentFrameIndex];
        public int FindFirstFullFrameIndex() => AnimationSequence.ToList().FindIndex(frame => frame != null);
        public int FindLastFullFrameIndex() => AnimationSequence.ToList().FindLastIndex(frame => frame != null);

        // Saved to reduce repetition
        public List<Frame?> EmptyInteractableAnimationSequence { get; } = [];

        // Events
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event Action? AnimationDataLoaded;
        public void OnAnimationDataLoaded()
            => AnimationDataLoaded?.Invoke();

        public AnimationStore(OriginalImageStore originalImageStore)
        {
            EmptyInteractableAnimationSequence.AddRange(Enumerable.Repeat<Frame?>(null, FrameSpaceCount));

            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += LoadAnimationData;
        }

        public void LoadAnimationData()
        {
            var (backgroundElements, foregroundElements, frames) = _originalImageStore.CurrentImage!.GetBackgroundForegroundAndFrames();
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