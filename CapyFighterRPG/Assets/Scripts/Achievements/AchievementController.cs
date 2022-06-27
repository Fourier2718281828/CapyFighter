using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    [Header("User data :")]
    [SerializeField] private Achievement _achievement;

    [Header("Prefab data :")]
    [SerializeField] private GameObject _name;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private Sprite     _emptyStarSprite;
    [SerializeField] private Sprite     _starSprite;


    private Text    _nameText;
    private Text    _descriptionText;
    private Image   _firstStarImage;
    private Image   _secondStarImage;
    private Image   _thirdStarImage;


    private void Awake()
    {
        _nameText = _name.GetComponent<Text>();
        _descriptionText = _description.GetComponent<Text>();
        _firstStarImage = _firstStar.GetComponent<Image>();
        _secondStarImage = _secondStar.GetComponent<Image>();
        _thirdStarImage = _thirdStar.GetComponent<Image>();
    }

    public void Init(Achievement data)
    {
        _achievement = data;
        AttachAllUIElements();
    }

    private void AttachAllUIElements()
    {
        _nameText.text = _achievement.Name;
        _descriptionText.text = _achievement.FirstStarDescription;

        SetFirstStarImageFulfilled(_achievement.IsFirstStarFulfilled());   
        SetSecondStarImageFulfilled(_achievement.IsSecondStarFulfilled());
        SetThirdStarImageFulfilled(_achievement.IsThirdStarFulfilled());
    }

    private void SetFirstStarImageFulfilled(bool active)
    {
        if(active)
            _firstStarImage.sprite = _starSprite;
        else
            _firstStarImage.sprite = _emptyStarSprite;
    }

    private void SetSecondStarImageFulfilled(bool active)
    {
        if (active)
            _secondStarImage.sprite = _starSprite;
        else
            _secondStarImage.sprite = _emptyStarSprite;
    }

    private void SetThirdStarImageFulfilled(bool active)
    {
        if (active)
            _thirdStarImage.sprite = _starSprite;
        else
            _thirdStarImage.sprite = _emptyStarSprite;
    }
}
