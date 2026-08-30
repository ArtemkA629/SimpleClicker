using System;

public class AudioInitializer : IDisposable
{
    private readonly AudioService _audioService;
    private readonly AudioView _view;

    public AudioInitializer(AudioService audioService, AudioView view)
    {
        _audioService = audioService;
        _view = view;
    }

    public void Initialize()
    {
        _audioService.Initialize();
        
        _view.MusicToggleClicked += _audioService.ToggleMusic;
        _view.SfxToggleClicked += _audioService.ToggleSfx;
        
        _view.SetMusicToggleState(_audioService.MusicEnabled);
        _view.SetSfxToggleState(_audioService.SfxEnabled);
    }

    public void Dispose()
    {
        _view.MusicToggleClicked -= _audioService.ToggleMusic;
        _view.SfxToggleClicked -= _audioService.ToggleSfx;
    }
}
