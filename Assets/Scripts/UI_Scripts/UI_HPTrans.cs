using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPTrans : MonoBehaviour
{
    private EnemyLife _life;
    [SerializeField] private Image _redImage;
    [SerializeField] private Image _whiteImage;


    public void Init(EnemyLife life)
    {
        _life = life;

        _life.OnNPCTakeDamage += ChangeUIHp;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(0f,8f,0f) + _life.transform.position;
    }

    private void ChangeUIHp(int currentLife, int Max)
    {
        _redImage.fillAmount = (float)currentLife / Max;
        ChangeWhite();
    }

    private async void ChangeWhite()
    {
        await Task.Delay(500);
        _whiteImage.fillAmount = _redImage.fillAmount;
    }
}