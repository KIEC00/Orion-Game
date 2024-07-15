using UnityEngine;

public interface IGameBoundsChangeHandler { void OnGameBoundsChange(Rect bounds); }
public interface IGameFakeVelocityChangeHandler { void OnFakeVelocityChange(float velocity); }

public interface IGameOverPrepareHandler { void OnGameOverPrepare(); }
public interface IGameOverHandler { void OnGameOver(); }
