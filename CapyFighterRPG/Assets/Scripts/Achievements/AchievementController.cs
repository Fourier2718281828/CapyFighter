using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    [Header("User data :")]
    [SerializeField] private AchievementData _achievementData;

    [Header("Prefab data :")]
    [SerializeField] private GameObject _icon;
    [SerializeField] private GameObject _name;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private Sprite     _emptyStarSprite;
    [SerializeField] private Sprite     _starSprite;


    private Image   _iconImage;
    private Text    _nameText;
    private Text    _descriptionText;
    private Image   _firstStarImage;
    private Image   _secondStarImage;
    private Image   _thirdStarImage;


    private void Awake()
    {
        _iconImage = _icon.GetComponent<Image>();
        _nameText = _name.GetComponent<Text>();
        _descriptionText = _description.GetComponent<Text>();
        _firstStarImage = _firstStar.GetComponent<Image>();
        _secondStarImage = _secondStar.GetComponent<Image>();
        _thirdStarImage = _thirdStar.GetComponent<Image>();
    }

    public void Init(AchievementData data)
    {
        _achievementData = data;
        AttachAllUIElements();
    }

    private void AttachAllUIElements()
    {
        _iconImage.sprite = _achievementData.IconImage;
        _nameText.text = _achievementData.Name;
        _descriptionText.text = _achievementData.FirstStarDescription;

        SetFirstStarImageFulfilled(true);
        SetSecondStarImageFulfilled(true);
        SetThirdStarImageFulfilled(true);


        //AttachAllStarUIDependings();
    }

    //private void AttachAllStarUIDependings()
    //{

    //}

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
