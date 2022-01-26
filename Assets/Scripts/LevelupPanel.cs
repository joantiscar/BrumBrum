using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelupPanel : MonoBehaviour
{

    public GameObject panel;
    public TextMeshProUGUI puntsText;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI att;
    public TextMeshProUGUI def;
    public TextMeshProUGUI spAtt;
    public TextMeshProUGUI spDef;
    public TextMeshProUGUI vel;
    public TextMeshProUGUI nomActual;
    public TextMeshProUGUI newHp;
    public TextMeshProUGUI newAtt;
    public TextMeshProUGUI newDef;
    public TextMeshProUGUI newSpAtt;
    public TextMeshProUGUI newSpDef;
    public TextMeshProUGUI newVel;
    public Button nextButton;
    public Button prevButton;
    public Button upgradeHp;
    public Button upgradeAtt;
    public Button upgradeDef;
    public Button upgradeSpAtt;
    public Button upgradeSpDef;
    public Button upgradeVel;
    Character pjActual;
    int nPjActual = 0;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    public void pre(){
        nPjActual--;
        if (nPjActual < 0) nPjActual = 4;
    }
    public void next(){
        nPjActual++;
        if (nPjActual > 4) nPjActual = 0;
    }

    public void close(){
        Singleton.setLevelupMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(nPjActual);
        if (Input.GetKeyDown("c") && !Singleton.menu() && !Singleton.enCombate ){
            Singleton.setLevelupMenu(!Singleton.LevelupPanel());
        }


        panel.SetActive(Singleton.LevelupPanel());
        pjActual = Singleton.instance().pjs[nPjActual];
        puntsText.text = "Puntos actuales: " + pjActual.upgrade_points.ToString();
        hp.text = "HP: " + pjActual.hpMax.ToString();
        newHp.text = (pjActual.hpMax + 5).ToString();
        att.text = "Ataque: " + pjActual.attack.ToString();
        newAtt.text = (pjActual.attack + 1).ToString();
        spAtt.text = "Ataque esp: " + pjActual.special_attack.ToString();
        newSpAtt.text = (pjActual.special_attack + 1).ToString();
        def.text = "Defensa: " + pjActual.defense.ToString();
        newDef.text = (pjActual.defense + 1).ToString();
        spDef.text = "Defensa esp: " + pjActual.special_defense.ToString();
        newSpDef.text = (pjActual.special_defense + 1).ToString();
        vel.text = "Velocidad: " + pjActual.velocity.ToString();
        newVel.text = (pjActual.velocity + 1).ToString();
        nomActual.text = pjActual.nombre.ToString();

      
        newHp.gameObject.SetActive(pjActual.upgrade_points > 0);
        newAtt.gameObject.SetActive(pjActual.upgrade_points > 0);
        newDef.gameObject.SetActive(pjActual.upgrade_points > 0);
        newSpAtt.gameObject.SetActive(pjActual.upgrade_points > 0);
        newSpDef.gameObject.SetActive(pjActual.upgrade_points > 0);
        newVel.gameObject.SetActive(pjActual.upgrade_points > 0);
        upgradeHp.gameObject.SetActive(pjActual.upgrade_points > 0);
        upgradeAtt.gameObject.SetActive(pjActual.upgrade_points > 0);
        upgradeDef.gameObject.SetActive(pjActual.upgrade_points > 0);
        upgradeSpAtt.gameObject.SetActive(pjActual.upgrade_points > 0);
        upgradeSpDef.gameObject.SetActive(pjActual.upgrade_points > 0);
        upgradeVel.gameObject.SetActive(pjActual.upgrade_points > 0);

    }

    public void investHp(){
        pjActual.use_upgrade_point("hp");
    }

    public void investAtt(){
        pjActual.use_upgrade_point("attack");
    }

    public void investDef(){
        pjActual.use_upgrade_point("special_attack");
    }

    public void investSpAtt(){
        pjActual.use_upgrade_point("defense");
    }

    public void investSpdef(){
        pjActual.use_upgrade_point("special_defense");
    }

    public void investVel(){
        pjActual.use_upgrade_point("velocity");
    }

}
