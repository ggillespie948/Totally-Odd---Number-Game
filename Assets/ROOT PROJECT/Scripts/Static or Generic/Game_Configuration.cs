﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Configuration : MonoBehaviour {

	[Header("GUI")]
	public int theme;
	
	[Header("Game Selection Itmes")]
	public TextMeshProUGUI titleTxt;
	public TextMeshProUGUI gridSizeTxt;
	public TextMeshProUGUI aiPlayersTxt;
	public TextMeshProUGUI humanPlayersTxt;
	public TextMeshProUGUI aiDifficultyTxt;
	public TextMeshProUGUI turnTimeTxt;
	public starFxController starFx;

	public TextMeshProUGUI objectiveText1;
	public TextMeshProUGUI objectiveText2;
	public TextMeshProUGUI objectiveText3;

	public GameObject objectivePanel;
	public GameObject configPanel;

	public GameObject objStar1;
	public GameObject objStar2;
	public GameObject objStar3;

	public  int OPPONENT_TILESKIN_1;
	public  int OPPONENT_TILESKIN_2;
	public  int OPPONENT_TILESKIN_3;
	public  int OPPONENT_TILESKIN_4;

	[Header("Game Configuration Settings")]
	public bool challengeMode;
	public bool puzzleMode;
	public string levelTitle;
	public string levelCode;
	public int levelNo;
	public int worldNo;
	public int gridSize;
	public int ai_opponents;
	public int ai_difficulty;
	public int human_players =1;
	public int turnTime;
	public int maxTile;
	public int[] startTileCounts;
	[Header("Set these three scores to a non-0 val and ai_players to 0 for target mode")]
	public int targetScore;
	public int targetScore2;
	public int targetScore3;
	public int targetTurns;

	public GameObject tile1;
	public GameObject tile2;
	public GameObject tile3;
	public GameObject tile4;
	public GameObject tile5;
	public GameObject tile6;
	public GameObject tile7;
	public GameObject tile8;
	public GameObject tile9;
	public GameObject tilePlus;

	[Header("Game Objective Items")]
	public string objective1Code;
	public string objective2Code;
	public string objective3Code;
	private List<GameObject> tiles = new List<GameObject>();
	[Header("Star Requirement (optional)")]
	public int starRequirement = 0;

	[SerializeField]
	public Image colour;

	[SerializeField]
	private bool isCustomGame;

	[SerializeField]
	private GameObject[] tilePreviews;

	[SerializeField]
	private bool isSelecitonDialog;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		tiles.Add(tile1);
		tiles.Add(tile2);
		tiles.Add(tile3);
		tiles.Add(tile4);
		tiles.Add(tile5);
		tiles.Add(tile6);
		tiles.Add(tile7);

		if(AccountInfo.playfabId != null)
		{
			if(challengeMode)
			{
				if(worldNo >= 0)
				{
					

					if(AccountInfo.worldStars[worldNo,levelNo]!=null)
					{
						if(AccountInfo.worldStars[worldNo, levelNo][0]=='1')
							if(objStar1 != null){objStar1.SetActive(true);}
						else
							if(objStar1 != null){objStar1.SetActive(false);}

						if(AccountInfo.worldStars[worldNo, levelNo][1]=='1')
							if(objStar2 != null){objStar2.SetActive(true);}
						else
							if(objStar2 != null){objStar2.SetActive(false);}

						if(AccountInfo.worldStars[worldNo, levelNo][2]=='1')
							if(objStar3 != null){objStar3.SetActive(true);}
						else
							if(objStar3 != null){objStar3.SetActive(false);}
					}
						
					

					if(starRequirement>(GUI_Controller.instance.CurrencyUI.playerStars))
					{
						//Image[] imgs = GetComponentsInChildren<Image>();
						Button[] btns = GetComponentsInChildren<Button>();
						GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<SpriteRenderer>().enabled=true;
						GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing=true;
						GetComponentInChildren<TextMeshProUGUI>().text="Requires " + starRequirement.ToString();

						foreach(Button btn in btns)
						{
							btn.interactable=false;
						}
					}
				}
			}
		}
	}

	public void CheckStarReq()
	{
		if(starRequirement>(GUI_Controller.instance.CurrencyUI.playerStars))
		{
			if(starRequirement!=0)
			{
				//Image[] imgs = GetComponentsInChildren<Image>();
				if(!isSelecitonDialog)
				{
					Button[] btns = GetComponentsInChildren<Button>();
					GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<SpriteRenderer>().enabled=true;
					GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing=true;
					GetComponentInChildren<TextMeshProUGUI>().text="Requires " + starRequirement.ToString();
					foreach(Button btn in btns)
					{
						btn.interactable=false;
					}
				}
			}


		} else{
			//temp - make this check whether a level selection is already unlocked
			//if(starRequirement!=0)
			//{
				Button[] btns = GetComponentsInChildren<Button>();
				GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<SpriteRenderer>().enabled=false;
				GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing=false;
				GetComponentInChildren<TextMeshProUGUI>().fontSize=140;
				GetComponentInChildren<TextMeshProUGUI>().text=(levelNo+1).ToString();
				foreach(Button btn in btns)
				{
					btn.interactable=true;
				}
			//}

		}

		UpdateStarParticles();
	}

	public Color threeStarColMain;
	public Color threeStarColBg;
	public Color defaultColMain;
	public Color defaultColBg;
	/// <summary>
	/// /// method which updates the star graphics / particles each time a new world is selected
	/// </summary>
	private void UpdateStarParticles()
	{
		if(AccountInfo.Instance == null)
		return;

		if(AccountInfo.worldStars[worldNo,levelNo]=="111") //&& (!isSelecitonDialog))
		{
			//if(!isSelecitonDialog)
			//{
				GetComponentInChildren<GUI_Dialogue>().bgMain.color=threeStarColMain;
				GetComponentInChildren<GUI_Dialogue>().bg.color=threeStarColBg;
			//}
		} else 
		{
			GetComponentInChildren<GUI_Dialogue>().bgMain.color=defaultColMain;
			GetComponentInChildren<GUI_Dialogue>().bg.color=defaultColBg;
		}

		if(AccountInfo.worldStars[worldNo,levelNo]==null)
			return;

		if(AccountInfo.worldStars[worldNo,levelNo].Length < 3)
			return;
		
		if(AccountInfo.worldStars[worldNo, levelNo][0]=='1')
		{
			if(objStar1 != null)
			{
				objStar1.SetActive(true);
			}
		}
		else
		{
			if(objStar1 != null)
			{
				objStar1.SetActive(false);
			}
		}

		if(AccountInfo.worldStars[worldNo, levelNo][1]=='1')
		{
			if(objStar2 != null)
			{
				objStar2.SetActive(true);
			}
		}
		else
		{
			if(objStar2 != null)
			{
				objStar2.SetActive(false);
			}
		}

		if(AccountInfo.worldStars[worldNo, levelNo][2]=='1')
		{
			if(objStar3 != null)
			{
				objStar3.SetActive(true);
			}
		}
		else
		{
			if(objStar3 != null)
			{
				objStar3.SetActive(false);
			}
		}
	}

	public void InitaliseLevelSelection()
	{
		MenuController.instance.activeGameConfig=this;
		if(objectivePanel!=null)
		objectivePanel.SetActive(false);
		if(configPanel!=null)
		configPanel.SetActive(true);
		
		gridSizeTxt.text = "Grid: "+gridSize+"x"+gridSize;

		if(challengeMode)
			if(humanPlayersTxt != null ){humanPlayersTxt.gameObject.SetActive(false);}
		

		if(humanPlayersTxt != null ){humanPlayersTxt.text="Players: " + human_players;}

		targetTurns=20;
		targetTurns = gridSize*3;
        
        if(gridSize==9)
            targetTurns=30;

		if(targetScore == 0)
		{
			aiPlayersTxt.text = "AI Opponents: " +ai_opponents;
			aiDifficultyTxt.text = "AI Difficulty: " + ai_difficulty + "/100";
			turnTimeTxt.text = "Turn Time: "+turnTime+"s";
		} else {
			aiPlayersTxt.text = "Target Mode";
			aiDifficultyTxt.text = "Turns: " +targetTurns;
			turnTimeTxt.text = "Target Timer: " + turnTime + "s";
		}

		if(ApplicationModel.TUTORIAL_MODE && ApplicationModel.RETURN_TO_WORLD ==-2)
		{
			Tutorial_Controller.instance.OnMouseDown();
		}
		
		
		if(startTileCounts[0] == 0)
			tile1.SetActive(false);
		else
			tile1.SetActive(true);

		if(startTileCounts[1] == 0)
			tile2.SetActive(false);
		else
			tile2.SetActive(true);

		if(startTileCounts[2] == 0)
			tile3.SetActive(false);
		else
			tile3.SetActive(true);

		if(startTileCounts[3] == 0)
			tile4.SetActive(false);
		else
			tile4.SetActive(true);

		if(startTileCounts[4] == 0)
			tile5.SetActive(false);
		else
			tile5.SetActive(true);

		if(startTileCounts[5] == 0)
			tile6.SetActive(false);
		else
			tile6.SetActive(true);

		if(startTileCounts[6] == 0)
			tile7.SetActive(false);
		else
			tile7.SetActive(true);

		if(startTileCounts[7] == 0)
			tile8.SetActive(false);
		else
			tile8.SetActive(true);

		if(startTileCounts[8] == 0)
			tile9.SetActive(false);
		else
			tile9.SetActive(true);

		if(startTileCounts[9] > 0)
		{
			tilePlus.SetActive(true);
		} else 
		{
			tilePlus.SetActive(false);
		}
		
	}

	public void InitaliseLevelObjectives()
	{
		objectivePanel.SetActive(true);
		configPanel.SetActive(false);
		titleTxt.text = levelTitle;
		objectiveText1.text=GenerateObjectiveText(objective1Code);
		objectiveText2.text=GenerateObjectiveText(objective2Code);
		objectiveText3.text=GenerateObjectiveText(objective3Code);

		if(AccountInfo.worldStars[worldNo, levelNo][0]=='1')
		objStar1.SetActive(true);
		else
		objStar1.SetActive(false);

		if(AccountInfo.worldStars[worldNo, levelNo][1]=='1')
		objStar2.SetActive(true);
		else
		objStar2.SetActive(false);

		if(AccountInfo.worldStars[worldNo, levelNo][2]=='1')
		objStar3.SetActive(true);
		else
		objStar3.SetActive(false);
	}

	public IEnumerator _StartLevel()
	{
		

		yield return new WaitForSeconds(3.5f);
		ApplicationModel.LEVEL_NO = levelNo;
		ApplicationModel.WORLD_NO = worldNo;
		ApplicationModel.LEVEL_CODE = levelCode;
		ApplicationModel.START_TILE_COUNTS =startTileCounts; 
		ApplicationModel.AI_PLAYERS=ai_opponents;
		ApplicationModel.HUMAN_PLAYERS=1;
		ApplicationModel.GRID_SIZE=gridSize;
		ApplicationModel.TURN_TIME=turnTime;
		ApplicationModel.TUTORIAL_MODE=false;
		ApplicationModel.HUMAN_PLAYERS=human_players;
		ApplicationModel.Objective1Code=objective1Code;
		ApplicationModel.Objective2Code=objective2Code;
		ApplicationModel.Objective3Code=objective3Code;
		ApplicationModel.MAX_TILE=maxTile;
		ApplicationModel.OPPONENT_TILESKIN_1= OPPONENT_TILESKIN_1;
		ApplicationModel.OPPONENT_TILESKIN_2= OPPONENT_TILESKIN_2;
		ApplicationModel.OPPONENT_TILESKIN_3= OPPONENT_TILESKIN_3;
		ApplicationModel.OPPONENT_TILESKIN_4= OPPONENT_TILESKIN_4;


		if(challengeMode)
			ApplicationModel.RETURN_TO_WORLD=worldNo;
		else
			ApplicationModel.RETURN_TO_WORLD=-1;

		//MenuController.instance.NavBar.gameObject.SetActive(false);
		if(isCustomGame)
		{
			ApplicationModel.MAX_TILE=maxTile+2;
			ApplicationModel.Objective1Code="BestTurnScore";
			ApplicationModel.Objective2Code="Win";
			ApplicationModel.Objective3Code="Errors.5";
			ApplicationModel.SOLO_PLAY=false;

		}

		if(targetScore ==0)
		{
			MenuController.instance.StartGameAI(ai_difficulty);
		} else {
			ApplicationModel.TARGET = targetScore;
			ApplicationModel.TARGET2 = targetScore2;
			ApplicationModel.TARGET3 = targetScore3;
			ApplicationModel.TURNS = targetTurns;
			MenuController.instance.SoloPlay();
		}
		
	}

	/// <summary>
	///  Called by success of live deduction
	/// </summary>
	public void StartLevel()
	{
		StartCoroutine(_StartLevel());
	}

	public void StartCustomGame()
	{
		MenuController.instance.activeGameConfig=this;
		MenuController.instance.CurrencyUI.DecreaseLives(3);
		ApplicationModel.CUSTOM_GAME=true;

	}

	/// <summary>
	/// this method is used to transfer the selected world's data to each of the 10 level configurations
	/// presented to the user
	/// </summary>
	/// <param name="config"></param>
	public void LoadConfiguration(Game_Configuration config)
	{
		startTileCounts=config.startTileCounts;
		levelCode = config.levelCode;
		ai_difficulty = config.ai_difficulty;
		levelTitle = config.levelTitle;
		levelNo = config.levelNo;
		worldNo = config.worldNo;
		gridSize = config.gridSize;
		human_players = config.human_players;
		ai_opponents = config.ai_opponents;
		turnTime = config.turnTime;
		maxTile = config.maxTile;
		startTileCounts = config.startTileCounts;
		targetScore = config.targetScore;
		targetScore2 = config.targetScore2;
		targetScore3 = config.targetScore3;
		targetTurns = config.targetTurns;
		objective1Code = config.objective1Code;
		objective2Code = config.objective2Code;
		objective3Code = config.objective3Code;
		starRequirement=config.starRequirement;
		OPPONENT_TILESKIN_1= config.OPPONENT_TILESKIN_1;
		OPPONENT_TILESKIN_2= config.OPPONENT_TILESKIN_2;
		OPPONENT_TILESKIN_3= config.OPPONENT_TILESKIN_3;
		OPPONENT_TILESKIN_4= config.OPPONENT_TILESKIN_4;

	}

	/// <summary>
	/// This method is called when a level icon is selected in a specific worlf of challengge mdoe
	/// This method is very similar to LoadConfiguration() except it also initialises the actual
	/// individual level selection window
	/// </summary>
	/// <param name="config"></param>
	public void SelectLevelConfiguration(Game_Configuration config)
	{
		if(challengeMode)
			MenuController.instance.NavBar.CloseAllDialogues(true);

		if(AccountInfo.worldStars != null)
		{
			if(levelCode=="B2")
			{
				if(AccountInfo.worldStars[worldNo, levelNo]!="000")
				{
					ApplicationModel.TUTORIAL_MODE=false;
				}
			}

			if(levelCode=="B1")
			{
				if(AccountInfo.worldStars[worldNo, levelNo]!="000")
				{
					ApplicationModel.TUTORIAL_MODE=false;
				}
			}

		}

		startTileCounts=config.startTileCounts;
		levelCode = config.levelCode;
		ai_difficulty = config.ai_difficulty;
		levelTitle = config.levelTitle;
		levelNo = config.levelNo;
		worldNo = config.worldNo;
		gridSize = config.gridSize;
		human_players = config.human_players;
		ai_opponents = config.ai_opponents;
		turnTime = config.turnTime;
		maxTile = config.maxTile;
		startTileCounts = config.startTileCounts;
		targetScore = config.targetScore;
		targetScore2 = config.targetScore2;
		targetScore3 = config.targetScore3;
		targetTurns = config.targetTurns;
		objective1Code = config.objective1Code;
		objective2Code = config.objective2Code;
		objective3Code = config.objective3Code;
		starRequirement=config.starRequirement;
		OPPONENT_TILESKIN_1= config.OPPONENT_TILESKIN_1;
		OPPONENT_TILESKIN_2= config.OPPONENT_TILESKIN_2;
		OPPONENT_TILESKIN_3= config.OPPONENT_TILESKIN_3;
		OPPONENT_TILESKIN_4= config.OPPONENT_TILESKIN_4;

		if(ApplicationModel.RETURN_TO_WORLD==-2)
		{
			Debug.LogError("LEVEL SELECTION TUTORIAL HOOK");
			ApplicationModel.TUTORIAL_MODE=true;
			Tutorial_Controller.instance.OnMouseDown();
		} else 
		{
			Debug.LogError("LEVEL SELECTION TUTORIAL HOOK MISSED");

		}

		gameObject.SetActive(true);
		InitaliseLevelObjectives();
		
		UpdateStarParticles();

		colour.color=MenuController.instance.NavBar.challengeModeDialogue.GetComponent<ChallengeModeController>().worldColourThemes[worldNo];

		MenuController.instance.activeGameConfig=this;

	}

	/// <summary>
	/// Method which increments the grid size of a game config dialogue
	/// </summary>
	public void IncrementGridSize()
	{
		if(gridSize <= 7)
			gridSize+=2;
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which decrements the grid size of a game config dialogue
	/// </summary>
	public void DecrementGridSize()
	{
		if(gridSize >= 7)
			gridSize-=2;
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which increments the grid size of a game config dialogue
	/// </summary>
	public void IncrementTurnTime()
	{
		turnTime+=5;
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which decrements the grid size of a game config dialogue
	/// </summary>
	public void DecrementTurnTime()
	{
		if(turnTime >= 20)
			turnTime-=5;
		InitaliseLevelSelection();
	}
	
	/// <summary>
	/// Method which decrements the number of ai_players of a game configuration dialogue
	/// </summary>
	public void IncrementAiPlayers()
	{
		if(ai_opponents <= 3)
		{
			if(human_players+ai_opponents+1 <=4)
				ai_opponents++;
		}
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which decrements the number of ai_players of a game configuration dialogue
	/// </summary>
	public void DecrementAiPlayers()
	{
		if(human_players <= 1 && ai_opponents <=1)
			return;

		if(ai_opponents >= 1)
			ai_opponents--;
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which decrements the number of players of a game configuration dialogue
	/// </summary>
	public void IncrementPlayers()
	{
		if(human_players <= 3)
		{
			human_players++;
			if(human_players+ai_opponents >4)
				ai_opponents--;
		}
		InitaliseLevelSelection();
	}

	public void DecrementPlayers()
	{
		if(ai_opponents == 0 && human_players <= 2)
			return;

		if(human_players >= 2)
		{
			human_players--;
		}
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which incrmenets the tile set of a game configuration dialogue
	/// </summary>
	public void IncrementDifficulty()
	{
		if(ai_difficulty<=90)
		{
			ai_difficulty+=10;
		}
		InitaliseLevelSelection();
	}


	/// <summary>
	///  Method whcih decrements the tile set of a game config dialogue
	/// </summary>
	public void DecrementDifficulty()
	{
		if(ai_difficulty>=20)
		{
			ai_difficulty-=10;
		}
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which incrmenets the tile set of a game configuration dialogue
	/// </summary>
	public void IncrementTiles()
	{
		if(maxTile <= 9)
			maxTile++;
		GenerateTileCounts();
		InitaliseLevelSelection();
	}


	/// <summary>
	///  Method whcih decrements the tile set of a game config dialogue
	/// </summary>
	public void DecrementTiles()
	{
		if(maxTile >= 3)
			maxTile--;
		GenerateTileCounts();
		InitaliseLevelSelection();
	}

	/// <summary>
	/// Method which generates default tile counts based on the value of maxTile
	/// Primarily used when adjusting custom-game game config values
	/// </summary>
	private void GenerateTileCounts()
	{
		for(int i=0; i<19; i++)
		{
			startTileCounts[i]=0;
		}

		for(int i=0; i<maxTile; i++)
		{
			startTileCounts[i]=30;
		}



		ApplicationModel.MAX_TILE=maxTile+2;
	}

	public void ReturnToWorldSelection()
	{
		MenuController.instance.NavBar.challengeModeDialogue.GetComponent<ChallengeModeController>().SelectWorld(worldNo);
	}

	public string GenerateObjectiveText(string objCode)
	{
		string[] ret = objCode.Split('.');

		switch(ret[0])
		{
			case "Win":
				return "Win the game";

			case "WinBy":
				return "Win by "+ret[1]+" points";

			case "Fill":
				return "Fill the game grid completely";

			case "FillWin":
				return "Fill the game grid and win";

			case "BestTurnScore":
				return "Finish with the best turn score";

			case "TurnScore":
				return "Score " + ret[1] + " or more in a single turn";

			case "TurnScoreExact":
				return "Score exactly " + ret[1] + " in a single turn";

			case "MostTiles":
				return "Play the most tiles in the game";

			case "PlayTiles":
				return "Play " + ret[1] + " or more tiles in the game";

			case "Score":
				return "Score " + ret[1] + " or more";

			case "Errors":
				return "Finish with " + ret[1] + " errors or less";

			case "ErrorsWin":
				return "Win with  " + ret[1] + " errors or less";

			case "ErrorsMore":
				return "Win with  " + ret[1] + " errors or more";

			case "Odd":
				return "Win the game with an odd score";

			case "Even":
				return "Win the game with an even score";

			case "Activate":
				return "Activate " + ret[1] + " tiles in a single turn";

			case "RunnerUp":
				return "Finish 2nd or better in the game";

			case "Swaps":
				return "Finish with " + ret[1] + " tile swaps or less";

			case "SwapsWin":
				return "Win the game with " + ret[1] + " tile swaps or less";

			case "TargetTutorial":
				return "Complete the Target Mode tutorial";

			case "TargetRules":
				return "Complete the Target Mode tutorial";
				
			case "Rules":
				return "Learn the rules of Totally Odd";

			case "Tutorial":
				return "Complete Tutorial 1";

			default:
				return "404: Object code unrecognised";

		}

	}
}

