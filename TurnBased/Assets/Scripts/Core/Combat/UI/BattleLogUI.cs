using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private ScrollRect scrollRect;

    [Header("Colors")]
    [SerializeField] private Color colorDefault = new Color(0.78f, 0.82f, 0.86f);
    [SerializeField] private Color colorDamage  = new Color(0.88f, 0.32f, 0.32f);
    [SerializeField] private Color colorHeal    = new Color(0.32f, 0.78f, 0.48f);
    [SerializeField] private Color colorMiss    = new Color(0.48f, 0.54f, 0.62f);
    [SerializeField] private Color colorCrit    = new Color(0.96f, 0.65f, 0.14f);
    [SerializeField] private Color colorStatus  = new Color(0.61f, 0.50f, 0.83f);
    [SerializeField] private Color colorSystem  = new Color(0.29f, 0.56f, 0.85f);

    private Coroutine _scrollCoroutine;
    private bool _userScrolling    = false;
    private bool _ignoreScrollEvent = false;
    private int  _turn = 0;

    public enum LogType { Default, Damage, Heal, Miss, Crit, Status, System }

    // ─────────────────────────────────────────────────────────
    void Awake()
    {
        if (scrollRect != null)
            scrollRect.onValueChanged.AddListener(OnScrollChanged);

        // Make sure movement type is Clamped
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
    }

    // ─────────────────────────────────────────────────────────
    //  PUBLIC API
    // ─────────────────────────────────────────────────────────

    /// <summary>Raw log with explicit type for coloring.</summary>
    public void Log(string message, LogType type = LogType.Default)
    {
        Color  col    = GetColor(type);
        string hex    = ColorUtility.ToHtmlStringRGB(col);
        string prefix = GetPrefix(type);
        string line   = $"<color=#3a4a60>[T{_turn:00}]</color> <color=#{hex}>{prefix}{message}</color>";

        logText.text += line + "\n";

        if (!_userScrolling)
        {
            if (_scrollCoroutine != null) StopCoroutine(_scrollCoroutine);
            _scrollCoroutine = StartCoroutine(ScrollToBottom());
        }
    }

    // Convenience methods
    public void LogDamage(string attacker, string target, int amount, string weapon = "")
    {
        string wpn = string.IsNullOrEmpty(weapon) ? "" : $" with {weapon}";
        Log($"<b>{attacker}</b> hits <b>{target}</b>{wpn} for <b>{amount} dmg</b>", LogType.Damage);
    }

    public void LogCrit(string attacker, string target, int amount, string weapon = "")
    {
        string wpn = string.IsNullOrEmpty(weapon) ? "" : $" with {weapon}";
        Log($"<b>{attacker}</b> CRITICAL HIT <b>{target}</b>{wpn} — <b>{amount} dmg!</b>", LogType.Crit);
    }

    public void LogMiss(string attacker, string weapon = "")
    {
        string wpn = string.IsNullOrEmpty(weapon) ? "" : $" ({weapon})";
        Log($"<b>{attacker}</b>{wpn} missed!", LogType.Miss);
    }

    public void LogHeal(string caster, string target, int amount)
    {
        Log($"<b>{caster}</b> heals <b>{target}</b> for <b>+{amount} HP</b>", LogType.Heal);
    }

    public void LogStatus(string target, string status, int turns = 0)
    {
        string t = turns > 0 ? $" ({turns} turns)" : "";
        Log($"<b>{target}</b> is <b>{status}</b>{t}", LogType.Status);
    }

    public void LogSystem(string message)
    {
        Log(message, LogType.System);
    }

    public void LogSeparator()
    {
        logText.text += "<color=#1e2a3a>─────────────────────────────────</color>\n";
        if (!_userScrolling)
        {
            if (_scrollCoroutine != null) StopCoroutine(_scrollCoroutine);
            _scrollCoroutine = StartCoroutine(ScrollToBottom());
        }
    }

    /// <summary>Call at the start of each turn to increment the turn counter.</summary>
    public void NextTurn()
    {
        _turn++;
        LogSeparator();
        LogSystem($"Turn {_turn} begins");
    }

    public void Clear()
    {
        logText.text    = "";
        _turn           = 0;
        _userScrolling  = false;
        _ignoreScrollEvent = true;
        scrollRect.verticalNormalizedPosition = 0f;
        _ignoreScrollEvent = false;
    }

    // ─────────────────────────────────────────────────────────
    //  INTERNAL
    // ─────────────────────────────────────────────────────────

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
        _ignoreScrollEvent = true;
        scrollRect.verticalNormalizedPosition = 0f;
        _ignoreScrollEvent = false;
    }

    private void OnScrollChanged(Vector2 pos)
    {
        if (_ignoreScrollEvent) return;
        _userScrolling = pos.y > 0.01f;
    }

    private Color GetColor(LogType type) => type switch
    {
        LogType.Damage => colorDamage,
        LogType.Heal   => colorHeal,
        LogType.Miss   => colorMiss,
        LogType.Crit   => colorCrit,
        LogType.Status => colorStatus,
        LogType.System => colorSystem,
        _              => colorDefault
    };

    private static string GetPrefix(LogType type) => type switch
    {
        LogType.Damage => "⚔ ",
        LogType.Heal   => "+ ",
        LogType.Miss   => "✦ ",
        LogType.Crit   => "⚡ ",
        LogType.Status => "~ ",
        LogType.System => "— ",
        _              => ""
    };
}