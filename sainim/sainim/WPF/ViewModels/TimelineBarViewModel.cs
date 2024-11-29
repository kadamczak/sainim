using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;

namespace sainim.WPF.ViewModels
{
    public class TimelineBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore; // original image - readonly
        public AnimationStore AnimationStore { get; set; }       // animation sequence - editable through Timeline Bar
                                                                 // edits of animation sequence can come From Timeline Bar
                                                                 // and from other sources

        // Counts and widths
        public double TickWidth { get; } = 80;
        public double Width => AnimationStore.AvailableFrameSpaces * TickWidth;

        // Observable collections
        public ObservableCollection<string> TickLabels { get; } = [];
        public ObservableCollection<FrameViewModel> FrameViewModels { get; } = [];  // When an image is NOT loaded, this collection has 0 elements and is thus not interactable.
                                                                                    // When an image is loaded, empty spaces in the animation sequence
                                                                                    // are represented by FrameViewModels with FrameData = null and are interactable.

        // Saved to reduce repetition
        public List<FrameViewModel> EmptyInteractableFrameViewModels { get; } = [];

        public class FrameViewModel
        {
            public int IndexInAnimationSequence { get; set; }
            public Frame? FrameData { get; set; } = null;
        }

        public TimelineBarViewModel(AnimationStore animationStore, OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            AnimationStore = animationStore;
            AnimationStore.FrameSequenceModified += ReloadFrameViewModels;

            EmptyInteractableFrameViewModels = CreateFrameIndexes()
                                              .Select(index => new FrameViewModel
                                              {
                                                  IndexInAnimationSequence = index,
                                                  FrameData = null
                                              })
                                              .ToList();

            CreateTickLabels();
        }

        private void CreateTickLabels()
        {
            var tickLabels = CreateFrameIndexes().Select(n => n.ToString());
            TickLabels.AddRange(tickLabels);
        }


        private void ResetFrameViewModelsToEmptyInteractableState()
        {
            FrameViewModels.Clear();
            FrameViewModels.AddRange(EmptyInteractableFrameViewModels);
        }

        private List<int> CreateFrameIndexes() => Enumerable.Range(AnimationStore.MinFrameIndex, AnimationStore.MaxFrameIndex).ToList();

        private void ReloadFrameViewModels()
        {
            ResetFrameViewModelsToEmptyInteractableState();

            // iterate through dictionary keys 
            foreach (var (indexInSequence, frame) in AnimationStore.FrameSequence)
                FrameViewModels[indexInSequence].FrameData = frame;

            FrameViewModels[20].FrameData = AnimationStore.FrameSequence[1];
        }
    }
}