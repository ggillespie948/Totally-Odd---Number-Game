using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class TeleType : MonoBehaviour
{

    [SerializeField]
    public bool autoPlay=false;

    [SerializeField]
    public bool singleSoundFX=false;

    [SerializeField]
    public AudioSource soundFX;

    [SerializeField]
    public float scrollspeed = 0.0015f;
    
    [SerializeField]
    public GameObject activateObjectOnComplete;
    [SerializeField]
    public float activationDelay;

    [SerializeField]
    public string label01 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";
    public string label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";


    private TMP_Text m_textMeshPro;

    private bool isMenuScene;

    [Header("Optional")]
    
    [SerializeField]
    public float cpDelay;
    public BoxCollider2D boxCollider;

    public int GenerateGameOverBonuses()
    {
        int currencyTotal =0;
        if(GameMaster.instance.playerWin)
        {
            label01+="Player Win Bonus: <size=\"18\"><#ff8000>+20 coins</size></color>  \n";
            currencyTotal+=20;
        }

        if(GameMaster.instance.gridFull)
        {
            label01+="Grid Complete Bonus: <size=\"18\"><#ff8000>+20 coins</size></color> \n ";
            currencyTotal+=20;
        }

        if(!GameMaster.instance.soloPlay)
        {
            if(GameMaster.instance.PlayerStatistics.BestScore(GameMaster.instance.playerScores))
            {
                label01+="Best Turn Score: <size=\"18\"><#ff8000>+20 coins</size> </color>\n ";
                currencyTotal+=20;
            }
        }

        if(GameMaster.instance.playerErrors[0]==0)
        {
            label01+="No Error Bonus:   <size=\"18\"><#ff8000>+20 coins</size></color>\n ";
            currencyTotal+=20;
        }

        GameMaster.instance.PlayerStatistics.GenerateAllOBjectiveOutcomes();

        if(GameMaster.instance.starCount>0)
        {
            label01 += "Objective Bonus: <size=\"18\"><#ff8000>+" + 50*GameMaster.instance.starCount +" coins</size></color>\n ";
            currencyTotal+=(50*GameMaster.instance.starCount);
        } 


        autoPlay=true;
        m_textMeshPro = GetComponent<TMP_Text>();
        m_textMeshPro.text = label01;
        m_textMeshPro.enableWordWrapping = true;
        m_textMeshPro.alignment = TextAlignmentOptions.Top;
        StartCoroutine(Start());

        int res=-1;
        if(AccountInfo.playfabId != null)
        {
            if(AccountInfo.Instance.Info.UserVirtualCurrency.TryGetValue(AccountInfo.COINS_CODE, out res))
            {
                AccountInfo.AddInGameCurrency(currencyTotal);
            }

        }

        if(activateObjectOnComplete.GetComponentsInChildren<TeleType>()!= null)
            activateObjectOnComplete.GetComponentInChildren<TeleType>().label01="+"+currencyTotal;

        return currencyTotal;
    }


    void Awake()
    {
        // Get Reference to TextMeshPro Component
        m_textMeshPro = GetComponent<TMP_Text>();
        m_textMeshPro.text = label01;
        m_textMeshPro.enableWordWrapping = true;
        m_textMeshPro.alignment = TextAlignmentOptions.Top;

        Scene scene = SceneManager.GetActiveScene();
		if(scene.name != "Main")
		{
            isMenuScene=true;
        }

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        scrollspeed=0.001f;
    }


    public IEnumerator Start()
    {
        if(!autoPlay)
        {
            yield return null;
        }

        soundFX.playOnAwake=true;
        soundFX.enabled=true;
        
        // Force and update of the mesh to get valid information.
        if(m_textMeshPro!=null)
            m_textMeshPro.ForceMeshUpdate();

        
        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;
        int visibleCount = 0;

        WaitForSeconds delay = new WaitForSeconds(scrollspeed);

        while (autoPlay)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);

            m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
            if(m_textMeshPro.text.ToCharArray().Length > visibleCount && m_textMeshPro.text[visibleCount]==' ' && !singleSoundFX)
            {
                //soundFX.enabled=false;
            } else{
                soundFX.enabled=true;
            }

            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {
                if(!singleSoundFX)
                    soundFX.enabled=false;

                if(activateObjectOnComplete != null)
                {
                    yield return new WaitForSeconds(activationDelay);
                    activateObjectOnComplete.SetActive(true);
                    
                }

                yield return new WaitForSeconds(cpDelay);
                // if(cp!= null)
                //     cp.fadeOut();

                

                autoPlay=false;
                yield return null;

            }

            counter += 1;

            yield return delay;
        }

        if(boxCollider!=null )
        {
            if(Tutorial_Controller.instance.tutorialIndex>0)
            {
                if(Tutorial_Controller.instance.tutorialDialogueInstructions[Tutorial_Controller.instance.tutorialIndex-1] != "[enableInput]" && Tutorial_Controller.instance.tutorialDialogueInstructions[Tutorial_Controller.instance.tutorialIndex-1] != "[enableInputNext]")
                {
                    boxCollider.enabled=true;
                }
            } else 
            {
                boxCollider.enabled=true;
            }
        }

        




    }

}
