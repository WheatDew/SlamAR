using UnityEngine;

public abstract class BaseView : BaseWindow
{
    private BaseList _baseList;
    public BaseList baseList {
        get {
            if (!_baseList) {
                _baseList = GetComponentInParent<BaseList>();
            }
            return _baseList;
        }
    }
    protected BaseConfig config;
    public bool IsSelect { get; set; }

    public TextMesh titleTextMesh;
    public TextMesh introductionTextMesh;
    public GameObject backGround;

    SCButton _scButton;
    public SCButton scButton {
        get {
            _scButton = GetComponent<SCButton>();
            if (!_scButton) {
                _scButton = gameObject.AddComponent<SCButton>();
            }
            return _scButton;
        }
    }

    public override void Init() {
        base.Init();
        if (config != null) {
            config.Init();
        }
    }

    public override void Awake() {
        base.Awake();
        if (config != null) {
            config.Awake();
        }

        IsSelect = false;
        SCButtonAddListener();
    }

    public override void Start() {
        base.Start();
        if (config != null) {
            config.Start();
        }
    }

    public override void Update() {
        base.Update();
        if (config != null) {
            config.Update();
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
        if (config != null) {
            config.OnDestroy();
        }
    }

    public virtual void OnEnter() {
        if (config !=null) {
            config.OnEnter();
        }
    }

    public virtual void OnDown() {
        if (config != null) {
            config.OnDown();
        }
    }

    public virtual void OnUp() {
        if (config != null) {
            config.OnUp();
        }
    }

    public virtual void OnExit() {
        if (config != null) {
            config.OnExit();
        }
    }

    public virtual void OnClick() {
        if (config != null) {
            config.OnClick();
        }
    }

    public virtual void UpdateConfig(BaseConfig config) {
        this.config = config;
         
        if (titleTextMesh) {
            titleTextMesh.text = config == null ? "Null" : config.title;
        }
        if (introductionTextMesh) {
            introductionTextMesh.text = config == null ? "Null" : config.introduction;
        }
    }

    public void SCButtonAddListener() {
        scButton.onEnter.AddListener(OnEnter);
        scButton.onClick.AddListener(OnClick);
        scButton.onDown.AddListener(OnDown);
        scButton.onUp.AddListener(OnUp);
        scButton.onExit.AddListener(OnExit);
    }
}
