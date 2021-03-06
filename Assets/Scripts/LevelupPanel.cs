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

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nPjActual);
        if (Input.GetKeyDown("c") && !Singleton.menu() && !Singleton.enCombate && !Singleton.dialegs()){
            FindObjectOfType<ThirdPersonMovement>().isTalkKing();
            Singleton.setLevelupMenu(!Singleton.LevelupPanel());
            GetComponentInParent<Pause>().togglePause();
        }


        panel.SetActive(Singleton.LevelupPanel());
        pjActual = Singleton.instance().pjs[nPjActual];
        puntsText.text = pjActual.upgrade_points.ToString();
        hp.text = pjActual.hpMax.ToString();
        newHp.text = (pjActual.hpMax + 10).ToString();
        att.text = pjActual.attack.ToString();
        newAtt.text = (pjActual.attack + 5).ToString();
        spAtt.text = pjActual.special_attack.ToString();
        newSpAtt.text = (pjActual.special_attack + 5).ToString();
        def.text = pjActual.defense.ToString();
        newDef.text = (pjActual.defense + 5).ToString();
        spDef.text = pjActual.special_defense.ToString();
        newSpDef.text = (pjActual.special_defense + 5).ToString();
        vel.text = pjActual.velocity.ToString();
        newVel.text = (pjActual.velocity + 5).ToString();
        nomActual.text = pjActual.nombre.ToString() + " LV " + pjActual.level.ToString();

      
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
        pjActual.use_upgrade_point("defense");
    }

    public void investSpAtt(){
        pjActual.use_upgrade_point("special_attack");
    }

    public void investSpdef(){
        pjActual.use_upgrade_point("special_defense");
    }

    public void investVel(){
        pjActual.use_upgrade_point("velocity");
    }

}
