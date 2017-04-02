﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventObjectController : MonoBehaviour
{

    public GameController gameController;
    public GameEvent eventModel;
    public bool areOptionsShown;
    public Texture[] allTextures;

    private string txtTooltip;
    private string txtButton;
    public Texture2D tooltipTexture;
    private GUIStyle tooltipStyle = new GUIStyle();
    public bool eventHoverCheck;
    public bool active;
    private int taal;

    private void Start()
    {
        eventHoverCheck = false;
        tooltipStyle.normal.background = tooltipTexture;
        taal = gameController.game.language;
        areOptionsShown = false;
        active = false;
    }
    void Update()
    {

    }

    public void Init(GameController gameController, GameEvent eventModel)
    {
        this.gameController = gameController;
        this.eventModel = eventModel;

        gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(eventModel.name);
        transform.position = eventModel.region.eventPositions[Random.Range(0, 4)];
    }

    // hover over event
    public void OnMouseOver()
    {
        // Click on event
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (!areOptionsShown)
            {
                ShowOptions();
            }
            else
            {
                HideOptions();
            }
        }
    }

    public void OnMouseEnter()
    {
        eventHoverCheck = true;
    }

    private void OnMouseExit()
    {
        eventHoverCheck = false;
    }

    private void OnGUI()
    {
        Rect lblReqt;
        Rect btnRect;

        lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);
        btnRect = GUILayoutUtility.GetRect(new GUIContent(txtButton), tooltipStyle);

        Vector3 v = eventModel.region.eventPositions[0];

        if (eventHoverCheck || areOptionsShown)
        { 
            txtTooltip = eventModel.name + "\n" + eventModel.description[taal];

            lblReqt.x = v.x;
            lblReqt.y = v.y + 40;

            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (areOptionsShown)
        {
            for (int i = 0; i < eventModel.choices.GetUpperBound(1) + 1; i++)
            {
                btnRect.x = v.x;

                if (i == 0)
                    btnRect.y = v.y + 80;
                if (i == 1)
                    btnRect.y = v.y + 170;
                if (i == 2)
                    btnRect.y = v.y + 260;

                string[] tip = { eventModel.choices[taal, i] + "\nKosten: " + eventModel.eventChoiceMoneyCost[i] +
                        "\nDuur: " + eventModel.eventDuration[i] + "\nConsequenties: "
                , eventModel.choices[taal, i] + "\nCost: " + eventModel.eventChoiceMoneyCost[i] +
                        "\nDuration: " + eventModel.eventDuration[i] + "\nConsequences: "  };

                txtButton = tip[taal];

                    txtButton += getConsequences(eventModel.consequences[i]);

                    if (GUI.Button(btnRect, "<color=#ccac6f>" + txtButton + "</color>", tooltipStyle))
                        ChooseOption(i);
            }
        }
    }

    string getConsequences(RegionStatistics s)
    {
        string consequences = "";

        if (s.donations != 0)
        {
            string[] a = { "Consequenties: \nDonaties: " + s.donations, "Consequentes: \nDonations: " + s.donations };
            consequences = a[taal];// "Consequenties: \nDonaties: " + s.donations;
        }
        if (s.ecoAwareness != 0)
        {
            string[] b = { " Milieubewustheid: " + s.ecoAwareness, " Eco awareness: " + s.ecoAwareness };
            consequences += b[taal];// " Milieubewustheid: " + s.ecoAwareness;
        }
        if (s.happiness != 0)
        {
            string[] c = { " Tevredenheid: " + s.happiness, " Happiness: " + s.happiness };
            consequences += c[taal];// " Tevredenheid: " + s.happiness;
        }
        if (s.income != 0)
        {
            string[] d = { " Inkomen: " + s.income, " Income: " + s.income };
            consequences += d[taal];// " Inkomen: " + s.income;
        }
        if (s.prosperity != 0)
        {
            string[] e = { " Welvaart: " + s.prosperity, " Prosperity: " + s.prosperity };
            consequences += e[taal];//" Welvaart: " + s.prosperity;
        }
        if (s.pollution.airPollutionIncrease != 0)
        {
            string[] f = { " Luchtvervuiling: " + s.pollution.airPollutionIncrease, " Air pollution: " + s.pollution.airPollutionIncrease };
            consequences += f[taal];// " Luchtvervuiling: " + s.pollution.airPollutionIncrease;
        }
        if (s.pollution.naturePollutionIncrease != 0)
        {
            string[] g = { " Natuurvervuiling: " + s.pollution.naturePollutionIncrease, " Nature pollution: " + s.pollution.naturePollutionIncrease };
            consequences += g[taal];// " Natuurvervuiling: " + s.pollution.naturePollutionIncrease;
        }
        if (s.pollution.waterPollutionIncrease != 0)
        {
            string[] h = { " Watervervuiling: " + s.pollution.waterPollutionIncrease, " Water pollution: " + s.pollution.waterPollutionIncrease };
            consequences += h[taal];// " Watervervuiling: " + s.pollution.waterPollutionIncrease;
        }

        return consequences;
    }

    void ShowOptions()
    {
        Debug.Log("Showing Options");
        areOptionsShown = true;
    }

    void HideOptions()
    {
        Debug.Log("Hiding options!");
        areOptionsShown = false;
    }

    void ChooseOption(int option)
    {
        Debug.Log("Chosen option: " + option);
        eventModel.SetPickedChoice(option, gameController.game);
        areOptionsShown = false;
        eventHoverCheck = false;
    }

    Texture SelectTexture(string description)
    {
        switch (description)
        {

            case "Earthquake":
                return allTextures[1];

            case "Flood":
                return allTextures[2];

            case "ForestFire":
                return allTextures[3];

            case "AirPollutionConcern":
                return allTextures[4];

            case "NaturePollutionConcern":
                return allTextures[4];

            case "WaterPollutionConcern":
                return allTextures[4];

            default: return allTextures[3];
        }
    }
}