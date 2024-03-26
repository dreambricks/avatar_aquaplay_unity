using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NationPick : MonoBehaviour
{
    [SerializeField] Nation nation;
    [SerializeField] GameObject points;

    public Image[] images;
    private int selectedImageIndex = -1; 
    public float fadeDuration = 0.5f; 

    void Start()
    {
        
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; 
            if (!images[i].gameObject.GetComponent<BoxCollider2D>())
            {
                images[i].gameObject.AddComponent<BoxCollider2D>();
            }
            
            images[i].gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            images[i].gameObject.GetComponent<BoxCollider2D>().size = new Vector2(images[i].rectTransform.rect.width, images[i].rectTransform.rect.height);
            images[i].gameObject.AddComponent<ClickHandler>().OnClicked += () => OnImageClicked(index);
        }
    }

    void OnImageClicked(int index)
    {
        if (selectedImageIndex == index)
        {
            
            ExecuteAction(index);
        }
        else
        {
            
            UpdateSelection(index);
        }
    }

    void UpdateSelection(int newIndex)
    {
        
        if (selectedImageIndex >= 0 && selectedImageIndex < images.Length)
        {
            
            StartCoroutine(FadeCanvasGroup(images[selectedImageIndex].GetComponent<CanvasGroup>(), 0f));
        }

        
        selectedImageIndex = newIndex;

        
        if (selectedImageIndex >= 0 && selectedImageIndex < images.Length)
        {
            
            StartCoroutine(FadeCanvasGroup(images[selectedImageIndex].GetComponent<CanvasGroup>(), 1f));
        }
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha)
    {
        float currentAlpha = canvasGroup.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    void ExecuteAction(int index)
    {
        nation.nationName = images[index].name;
        points.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }
}

public class ClickHandler : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction OnClicked;

    private void OnMouseDown()
    {
        if (OnClicked != null)
        {
            OnClicked();
        }
    }
}
