using UnityEngine;

public interface IGameBoundsChangeHandler { void OnGameBoundsChange(Rect bounds); }
public interface IGameFakeVelocityChangeHandler { void OnFakeVelocityChange(float velocity); }

public interface ILevelStartHandler { void OnLevelStart(); }

public interface IScoreAddHandler { void OnScoreAdd(int count); }

public interface IMenuHandler { void OnMenuOpen(); void OnMenuClose(); }

public interface IGameOverPrepareHandler { void OnGameOverPrepare(); }
public interface IGameOverHandler { void OnGameOver(); }

public interface IRestartHandler { void OnRestart(); }
