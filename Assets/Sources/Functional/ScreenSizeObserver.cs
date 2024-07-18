using UnityEngine;

public class ScreenSizeObserver : MonoBehaviour
{
    public Rect GameBounds
    {
        get
        {
            var camera = Camera.main;
            var cameraHeight = camera.orthographicSize * 2;
            var cameraSize = new Vector2(camera.aspect * cameraHeight, cameraHeight);
            return new Rect((Vector2)camera.transform.position - cameraSize / 2, cameraSize);
        }
    }

    private Vector2Int _screenSize;

    private void OnRectTransformDimensionsChange()
    {
        var newScreenSize = new Vector2Int(Screen.width, Screen.height);
        if (_screenSize == newScreenSize) { return; }
        _screenSize = newScreenSize;
        EventBus.Invoke<IGameBoundsChangeHandler>(obj => obj.OnGameBoundsChange(GameBounds));
    }
}

