using System;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public Action changingSpriteAction;

    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private Sprite _sprite = null;
    [SerializeField] private int _radius = 0;

    private Vector2 _zeroPixel = new Vector2();

    private Vector2 _previousPixel = new Vector2();

    void Awake()
    {
        CreateSprite();
    }
    private void Start()
    {
        SpriteChangeEnd();
    }

    private void CreateSprite()
    {
        Texture2D newTexture = new Texture2D(_sprite.texture.width, _sprite.texture.height, TextureFormat.RGBA32, false);

        newTexture.SetPixels(_sprite.texture.GetPixels());
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

        _spriteRenderer.sprite = newSprite;
        _spriteRenderer.sprite.texture.Apply();

        _zeroPixel = new Vector2(gameObject.transform.position.x - _spriteRenderer.bounds.size.x, gameObject.transform.position.y - _spriteRenderer.bounds.size.y) / 2;
    }

    public void Draw(Vector2 point)
    {
        Vector2 pixel = new Vector2(Mathf.Abs(_zeroPixel.x - point.x), Mathf.Abs(_zeroPixel.y - point.y)) * _spriteRenderer.sprite.pixelsPerUnit;

        if (_previousPixel != new Vector2())
        {
            Vector2 direction = (_previousPixel - pixel).normalized;
            float distance = Vector2.Distance(pixel, _previousPixel);

            for (float i = 0; i < distance; i += _radius / 2)
            {
                Vector2 position = pixel + direction * i;
                DrawCircle(position);
            }
        }
        else
        {
            DrawCircle(pixel);
        }

        _spriteRenderer.sprite.texture.Apply();
        _previousPixel = pixel;
    }

    private void DrawCircle(Vector2 pixel)
    {
        for (int x = -_radius; x < _radius; x++)
        {
            for (int y = -_radius; y < _radius; y++)
            {
                Vector2 position = new Vector2(pixel.x + x, pixel.y + y);

                if (Vector2.Distance(position, new Vector2(pixel.x, pixel.y)) > _radius || position.x < 0 || position.y < 0 || position.x > _spriteRenderer.sprite.texture.width || position.y > _spriteRenderer.sprite.texture.height)
                {
                    continue;
                }
                else
                {
                    _spriteRenderer.sprite.texture.SetPixel((int)position.x, (int)position.y, new Color(0, 0, 0, 0));
                }
            }
        }
    }

    public void SpriteChangeEnd()
    {
        _previousPixel = new Vector2();
        changingSpriteAction?.Invoke();
    }
}
