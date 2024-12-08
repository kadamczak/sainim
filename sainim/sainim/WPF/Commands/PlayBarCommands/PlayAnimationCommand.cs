using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.ComponentModel;
using System.Windows.Threading;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class PlayAnimationCommand : CommandBase, INotifyPropertyChanged
    {
        private OriginalImageStore _originalImageStore { get; }
        private AnimationStore _animationStore { get; }
        private FrameRenderer _frameRenderer { get; }

        private bool _isPlaying = false;
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }

        private bool _isChecked = false;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private int SavedFirstFrameIndex { get; set; } = -1;
        private int SavedLastFrameIndex { get; set; } = -1;

        private DispatcherTimer timer;

        private void StartTimer()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_animationStore.GetMillisecondsBetweenFrames())};
            timer.Tick += AdvanceFrame();
            timer.Start();
        }

        private EventHandler AdvanceFrame()
        {
            return (s, e) =>
            {
                if (IsOnLastFrame())
                {
                    if(!_animationStore.Repeating)
                        StopTimer();
                    else
                        _animationStore.CurrentFrameIndex = SavedFirstFrameIndex;
                }
                else
                {
                    _animationStore.CurrentFrameIndex++;
                }
            };
        }

        private bool IsOnLastFrame() => _animationStore.CurrentFrameIndex == SavedLastFrameIndex;

        private void StopTimer()
        {
            timer?.Stop();
            IsPlaying = false;
            IsChecked = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public PlayAnimationCommand(OriginalImageStore originalImageStore, AnimationStore animationStore, FrameRenderer frameRenderer)
        {
            _originalImageStore = originalImageStore;
            _animationStore = animationStore;
            _frameRenderer = frameRenderer;

            _originalImageStore.NewImageLoaded += StopTimer;
            _animationStore.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(AnimationStore.FrameRate)) UpdateTimerInterval(); };
        }

        private void UpdateTimerInterval()
        {
            if (timer is null)
                return;

            timer.Interval = TimeSpan.FromMilliseconds(_animationStore.GetMillisecondsBetweenFrames());
        }

        public override void Execute(object? parameter)
        {
            if (IsPlaying)
            {
                StopTimer();
                return;
            }

            SavedFirstFrameIndex = _animationStore.FindFirstFullFrameIndex();
            SavedLastFrameIndex = _animationStore.FindLastFullFrameIndex();
            bool isRepeating = _animationStore.Repeating;

            // Safeguards
            if (!AnimationCanPlay(isRepeating))
            {
                IsPlaying = false;
                IsChecked = false;
                return;
            }

            IsPlaying = true;

            // Render frames that have not been rendered yet
            var enabledLayerTypes = _animationStore.SelectableLayerTypes.GetSelectedLayerTypes();
            RenderMissingFrames(enabledLayerTypes);

            // Start playing
            StartTimer();
        }

        private bool AnimationCanPlay(bool isRepeating)
        {
            if ((SavedFirstFrameIndex < 0) || (SavedFirstFrameIndex == SavedLastFrameIndex))
                return false;

            if (!isRepeating && IsOnLastFrame())
                return false;

            return true;
        }

        private void RenderMissingFrames(List<string> enabledLayerTypes)
        {
            var framesToRender = _animationStore.AnimationSequence.Skip(SavedFirstFrameIndex).Take(SavedLastFrameIndex - SavedFirstFrameIndex + 1);

            foreach (var frame in framesToRender)
            {
                if (frame is null)
                    continue;
                if (frame!.GetRenderedBitmap(enabledLayerTypes) is not null)
                    continue;

                frame!.RenderedBitmaps[enabledLayerTypes] = _frameRenderer.RenderFrame(frame!, _originalImageStore.CurrentImage!, enabledLayerTypes);
            }
        }
    }
}