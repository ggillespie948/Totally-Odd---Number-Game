﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI_Dialogue_Call : MonoBehaviour {

    [Header("General Attributes (optional)")]
	public TextMeshProUGUI panelTitleText;
	public TextMeshProUGUI playerNameTxt;
	public TextMeshProUGUI playerScoreTxt;
	public TextMeshProUGUI playerErrorsTxt;
	public TextMeshProUGUI playerBestScoreTxt;

	public TextMeshProUGUI tilesPlayedTxt;
	public TextMeshProUGUI tilesSwapsTxt;

	public TextMeshProUGUI objective1Txt;
	public TextMeshProUGUI objective2Txt;
	public TextMeshProUGUI objective3Txt;

	[Header("VS Attributes (optional)")]
	public TextMeshProUGUI summaryText;

	[Header("World Texts (optional)")]
	public TextMeshProUGUI beginnerText;
	public TextMeshProUGUI intermediateText;
	public TextMeshProUGUI advancedText;
	public TextMeshProUGUI masterText;

    [Header("Target Mode Attributes (optional)")]
	public starFxController targetStarController;
	public TextMeshProUGUI targetScoreText;

	[Header("Objective Panel Attributes")]

	private int playerCounter;

	public bool isOpen = false;

	public GameObject objectivePanel;
	public GameObject playerPanel;

	public GameObject objectiveStar1;
	public GameObject objectiveStar2;
	public GameObject objectiveStar3;

	public GameObject slimLeftButton;
	public GameObject slimRightButton;
	public GameObject leftButton;
	public GameObject rightButton;
	


	public void Open() {
		isOpen=true;
        gameObject.GetComponentInChildren<GUI_Dialogue>().Open();
    }

	public void Close() {
		isOpen = false;
        gameObject.GetComponentInChildren<GUI_Dialogue>().Close();
    }

	public void NextPlayer(List<int> playerScores, List<int> playerBestScores, List<int> playerErrors, int targetScore)
	{
		if(GameMaster.instance.soloPlay)
			return;
		GUI_Controller.instance.StopAllCoroutines();

		playerCounter++;
		panelTitleText.text="Player Scores";
		Debug.LogWarning("PC: " + playerCounter);
		if(playerCounter==1)
		{
			playerPanel.SetActive(true);
			InitDialogue(playerScores, playerBestScores, playerErrors,targetScore,1);
			objectivePanel.SetActive(false);
			
			return;
		} else if(playerCounter==0)
		{
			playerPanel.SetActive(false);
			objectivePanel.SetActive(true);
			return;
		}

		if(playerCounter > GameMaster.instance.playerScores.Count)
		{
			Debug.LogWarning("PC Reset");
			playerCounter=0;
			playerPanel.SetActive(false);
			objectivePanel.SetActive(true);
			panelTitleText.text="Missions";

			
			 
			return;

		}

		

		

		bool bestTurnScore = true;
		 foreach(int score in playerBestScores)
		{
			if(score> playerBestScores[playerCounter-1])
				bestTurnScore = false;
		} 
		bool bestScore = true;
		 foreach(int score in playerScores)
		{
			if(score> playerScores[playerCounter-1])
				bestScore = false;
		}
		bool bestTilesplayed = true;
		foreach(int score in GameMaster.instance.PlayerStatistics.playedTiles)
		{
			if(score> GameMaster.instance.PlayerStatistics.playedTiles[playerCounter-1])
				bestTilesplayed = false;
		} 
		bool leastSwaps = true;
		foreach(int score in GameMaster.instance.playerSwaps)
		{
			if(score< GameMaster.instance.playerSwaps[playerCounter-1])
				leastSwaps = false;
		} 

		if(bestTurnScore)
		{
			playerBestScoreTxt.color = Color.green;
		}
		else
		{
			playerBestScoreTxt.color = Color.white;
			playerBestScoreTxt.fontStyle = FontStyles.Normal;
		}

		if(leastSwaps)
		{
			tilesSwapsTxt.color = Color.green;
		}
		else
		{
			tilesSwapsTxt.color = Color.white;
			tilesSwapsTxt.fontStyle = FontStyles.Normal;
		}

		if(playerErrors[playerCounter-1] == 0)
		{
			playerErrorsTxt.color = Color.green;
		} else 
		{
			playerErrorsTxt.color = Color.white;
		}

		if(bestScore)
			playerScoreTxt.color = Color.green;
		else
		{
			playerScoreTxt.fontStyle = FontStyles.Normal;
			playerScoreTxt.color = Color.white;
		}

		if(bestTilesplayed)
			tilesPlayedTxt.color = Color.green;
		else
		{
			tilesPlayedTxt.fontStyle = FontStyles.Normal;
			tilesPlayedTxt.color = Color.white;
		}

		if(playerCounter==0)
		{
			return;
		}

		playerNameTxt.text = "Player " + playerCounter;

		if(playerCounter==1 && AccountInfo.Instance.Info.PlayerProfile.DisplayName!= null)
			playerNameTxt.text=AccountInfo.Instance.Info.PlayerProfile.DisplayName;

		if(bestScore)
		{
			playerNameTxt.text += " (Winner)";
		}

		if(playerScoreTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerScores[playerCounter-1], playerScoreTxt, "Score: "));}
		if(playerErrorsTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerErrors[playerCounter-1], playerErrorsTxt, "Errors: "));}
		if(playerBestScoreTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerBestScores[playerCounter-1], playerBestScoreTxt, "Best turn score: "));}
		if(tilesPlayedTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,GameMaster.instance.playerPlayedTiles[playerCounter-1], tilesPlayedTxt, "Tiles played: "));}
		if(tilesSwapsTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,GameMaster.instance.playerSwaps[playerCounter-1], tilesSwapsTxt, "Tile swaps: "));}
	}

	public void PreviousPlayer(List<int> playerScores, List<int> playerBestScores, List<int> playerErrors, int targetScore)
	{
		if(GameMaster.instance.soloPlay)
			return;
		GUI_Controller.instance.StopAllCoroutines();
		panelTitleText.text="Player Scores";
		playerCounter--;
		if(playerCounter == 0)
		{
			playerPanel.SetActive(false);
			//InitObjectivePanel();
			panelTitleText.text="Missions";
			objectivePanel.SetActive(true);
			playerCounter=playerScores.Count;
		} else if (playerCounter < 0)
		{
			playerPanel.SetActive(true);
			InitDialogue(playerScores, playerBestScores, playerErrors,targetScore, playerBestScores.Count);
			objectivePanel.SetActive(false);
		}

		

		if(playerScoreTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerScores[playerCounter-1], playerScoreTxt, "Score: "));}
		if(playerErrorsTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerErrors[playerCounter-1], playerErrorsTxt, "Errors: "));}
		if(playerBestScoreTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerBestScores[playerCounter-1], playerBestScoreTxt, "Best turn score: "));}
		if(tilesPlayedTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,GameMaster.instance.playerPlayedTiles[playerCounter-1], tilesPlayedTxt, "Tiles played: "));}
		if(tilesSwapsTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,GameMaster.instance.playerSwaps[playerCounter-1], tilesSwapsTxt, "Tile swaps: "));}

		bool bestTurnScore = true;
		 foreach(int score in playerBestScores)
		{
			if(score> playerBestScores[playerCounter-1])
				bestTurnScore = false;
		} 
		bool bestScore = true;
		 foreach(int score in playerScores)
		{
			if(score> playerScores[playerCounter-1])
				bestScore = false;
		}
		bool bestTilesplayed = true;
		foreach(int score in GameMaster.instance.PlayerStatistics.playedTiles)
		{
			if(score> GameMaster.instance.PlayerStatistics.playedTiles[playerCounter-1])
				bestTurnScore = false;
		} 

		bool leastSwaps = true;
		foreach(int score in GameMaster.instance.playerSwaps)
		{
			if(score< GameMaster.instance.playerSwaps[playerCounter-1])
				leastSwaps = false;
		} 

		if(leastSwaps)
		{
			tilesSwapsTxt.color = Color.green;
		}
		else
		{
			tilesSwapsTxt.color = Color.white;
			tilesSwapsTxt.fontStyle = FontStyles.Normal;
		}

		if(bestTurnScore)
			playerBestScoreTxt.color = Color.green;
		else
		{
			playerBestScoreTxt.color = Color.white;
			playerBestScoreTxt.fontStyle = FontStyles.Normal;
		}

		if(playerErrors[playerCounter-1] == 0)
		{
			playerErrorsTxt.color = Color.green;
		} else 
		{
			playerErrorsTxt.color = Color.white;
		}

		if(bestScore)
			playerScoreTxt.color = Color.green;
		else
		{
			playerScoreTxt.fontStyle = FontStyles.Normal;
			playerScoreTxt.color = Color.white;
		}
		if(bestTilesplayed)
			tilesPlayedTxt.color = Color.green;
		else
		{
			tilesPlayedTxt.fontStyle = FontStyles.Normal;
			tilesPlayedTxt.color = Color.white;
		}

		playerNameTxt.text = "Player " + playerCounter;

		if(playerCounter==1 && AccountInfo.Instance.Info.PlayerProfile.DisplayName!= null)
			playerNameTxt.text=AccountInfo.Instance.Info.PlayerProfile.DisplayName;

		if(bestScore)
		{
			playerNameTxt.text += " (Winner)";
		}
	}

	public void InitObjectivePanel()
	{
		
		panelTitleText.text="Missions";
		objective1Txt.text = GameMaster.instance.PlayerStatistics.GenerateObjectiveText(ApplicationModel.Objective1Code);
		objective2Txt.text = GameMaster.instance.PlayerStatistics.GenerateObjectiveText(ApplicationModel.Objective2Code);
		objective3Txt.text = GameMaster.instance.PlayerStatistics.GenerateObjectiveText(ApplicationModel.Objective3Code);

		//Generate Star Animations
		GameMaster.instance.PlayerStatistics.GenerateAllOBjectiveOutcomes();

		string starString="";

		if(targetStarController != null)
		{
			//targetStarController.Reset();
			int starCount =0;
			if(GameMaster.instance.PlayerStatistics.OBJECTIVE_1)
			{
				starCount++;
				starString+="1";
				objectiveStar1.SetActive(true);
			} else 
			{
				starString+="0";
			}
			if(GameMaster.instance.PlayerStatistics.OBJECTIVE_2)
			{
				starCount++;
				starString+="1";
				objectiveStar2.SetActive(true);
			} else 
			{
				starString+="0";
			}
			if(GameMaster.instance.PlayerStatistics.OBJECTIVE_3)
			{
				starCount++;
				starString+="1";
				objectiveStar3.SetActive(true);
			} else 
			{
				starString+="0";
			}

			//Debug.LogWarning("Star String: " + starString);
			GameMaster.instance.starCount = starCount;
		}


		
		//Update player STAR UI
		GUI_Controller.instance.StarRewardAnim(starString);

		//Update PlayFab star data based on performance
		if(ApplicationModel.LEVEL_CODE != "" && AccountInfo.playfabId != null)
		{
			Debug.Log("Star Count: " + GameMaster.instance.starCount);
			AccountInfo.Instance.UpdatePlayerStarData(ApplicationModel.WORLD_NO, ApplicationModel.LEVEL_NO, ApplicationModel.LEVEL_CODE, starString);
		} else 
		{
			Debug.Log("Null level code..");
		}


	}

	public void InitDialogue(List<int> playerScores, List<int> playerBestScores, List<int> playerErrors, int targetScore, int pc)
	{
		playerCounter=pc;
		//playerCounter=1;

		if(playerCounter==1 && AccountInfo.Instance.Info.PlayerProfile.DisplayName!= null)
			playerNameTxt.text=AccountInfo.Instance.Info.PlayerProfile.DisplayName;

		
		if(playerScoreTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerScores[playerCounter-1], playerScoreTxt, "Score: "));}
		if(playerErrorsTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerErrors[playerCounter-1], playerErrorsTxt, "Errors: "));}
		if(playerBestScoreTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,playerBestScores[playerCounter-1], playerBestScoreTxt, "Best turn score: "));}
		if(tilesPlayedTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,GameMaster.instance.playerPlayedTiles[playerCounter-1], tilesPlayedTxt, "Tiles played: "));}
		if(tilesSwapsTxt != null) { StartCoroutine(GUI_Controller.instance.UpdateUIScore(0,GameMaster.instance.playerSwaps[playerCounter-1], tilesSwapsTxt, "Tile swaps: "));}

		bool bestTurnScore = true;
		 foreach(int score in playerBestScores)
		{
			if(score> playerBestScores[0])
				bestTurnScore = false;
		} 

		bool bestTilesplayed = true;
		foreach(int score in GameMaster.instance.PlayerStatistics.playedTiles)
		{
			if(score> GameMaster.instance.PlayerStatistics.playedTiles[0])
				bestTurnScore = false;
		} 

		bool bestScore = true;
		 foreach(int score in playerScores)
		{
			if(score> playerScores[0])
				bestScore = false;
		} 

		bool leastSwaps = true;
		foreach(int score in GameMaster.instance.playerSwaps)
		{
			if(score< GameMaster.instance.playerSwaps[playerCounter-1])
				leastSwaps = false;
		} 

		if(bestScore)
		{
			playerNameTxt.text += " (Winner)";
		}

		if(bestTurnScore)
			playerBestScoreTxt.color = Color.green;
		else
		{
			playerBestScoreTxt.color = Color.white;
			playerBestScoreTxt.fontStyle = FontStyles.Normal;
		}

		if(leastSwaps)
		{
			tilesSwapsTxt.color = Color.green;
		}
		else
		{
			tilesSwapsTxt.color = Color.white;
			tilesSwapsTxt.fontStyle = FontStyles.Normal;
		}

		if(playerErrors[0] == 0)
		{
			playerErrorsTxt.color = Color.green;
		} else 
		{
			playerErrorsTxt.color = Color.white;
		}

		if(bestScore)
			playerScoreTxt.color = Color.green;
		else
		{
			playerScoreTxt.fontStyle = FontStyles.Normal;
			playerScoreTxt.color = Color.white;
		}

		if(bestTilesplayed)
			tilesPlayedTxt.color = Color.green;
		else
		{
			tilesPlayedTxt.fontStyle = FontStyles.Normal;
			tilesPlayedTxt.color = Color.white;
		}


		

	}

	public void ShowLeftRightButtons()
	{
		if(Screen.height > 2400)
			{
				if(Screen.width < 1130)
				{
					leftButton.SetActive(false);
					rightButton.SetActive(false);
					slimLeftButton.SetActive(true);
					slimRightButton.SetActive(true);
					

				} else if(Screen.height > 2600)
				{
					leftButton.SetActive(false);
					rightButton.SetActive(false);
					slimLeftButton.SetActive(true);
					slimRightButton.SetActive(true);
					
					
				} 

			}
	}


}
