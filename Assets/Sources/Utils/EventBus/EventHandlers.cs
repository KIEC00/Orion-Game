using UnityEngine;

public interface IPauseHandler { void OnPause(); void OnResume(); }

public interface IGameBoundsChangeHandler { void OnGameBoundsChange(Rect bounds); }
public interface IGameFakeVelocityChangeHandler { void OnFakeVelocityChange(float velocity); }
