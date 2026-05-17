using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        GameManager inst = GameManager.instance;
        switch (type)
        {
            case InfoType.Exp:
                float curExp = inst.exp;
                float maxExp = inst.nexExp[Mathf.Min(inst.level, inst.nexExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Health:
                float curHealth = inst.health;
                float maxHealth = inst.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", inst.kill);
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", inst.level);
                break;
            case InfoType.Time:
                float remainTime = inst.maxGameTime - inst.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
        }
    }
}
