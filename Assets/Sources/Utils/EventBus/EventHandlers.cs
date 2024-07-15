using UnityEngine;

public interface IGameBoundsChangeHandler { void OnGameBoundsChange(Rect bounds); }
public interface IGameFakeVelocityChangeHandler { void OnFakeVelocityChange(float velocity); }

public interface ILevelStartHandler{void OnLevelStart(); }

public interface IMenuHandler { void OnMenuOpen(); void OnMenuClose(); }

public interface IGameOverPrepareHandler { void OnGameOverPrepare(); }
public interface IGameOverHandler { void OnGameOver(); }
