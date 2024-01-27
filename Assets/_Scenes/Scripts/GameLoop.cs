﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _Scenes.Scripts
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Player player1 = null;
        [SerializeField] private Player player2 = null;
        [SerializeField] private ClickDetector clickDetector = null;
        [SerializeField] private HUD hud;
        [SerializeField] private Tile tile;

        private List<Player> players;
        
        private int currentPlayerIndex = 1;

        private float tileSize;

        private Random random = new Random();

        private bool isMovementPhase = false;
        private CharacterToken currentCharacterToken = null;

        private void Start()
        {
            players = new List<Player>{ player1, player2 };
            clickDetector.OnValidClick += OnValidClick;
            hud.getSpawnButton().onClick.AddListener(handleSpawnButtonClicked);
            
            initializeTurn();
        }

        public void OnValidClick(GameObject gameObject)
        {
            Tile tile = gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                if (isMovementPhase)
                {
                    handleMoveToTile(tile);
                    return;
                }
                
                tile.Act();
                hud.InfoPanel.ShowInfo(tile);
            }

            CharacterToken characterToken = gameObject.GetComponent<CharacterToken>();
            if (characterToken != null && characterToken.GetFaction() == getCurrentPlayer().getFaction())
            {
                isMovementPhase = true;
                currentCharacterToken = characterToken;
            }
        }

        private Player getCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }
        
        private void handleSpawnButtonClicked()
        {
            getCurrentPlayer().spawnCharacterToken();
        }

        private void handleMoveToTile(Tile tile)
        {
            if (hud.getNumberOfMoves() <= 0)
            {
                return;
            }
            
            Vector3 tokenTransform = currentCharacterToken.transform.position;
            Vector3 tileToMoveTransform = tile.transform.position;

            if (Vector3.Distance(tokenTransform, tileToMoveTransform) == 1)
            {
                currentCharacterToken.moveToDestination(tileToMoveTransform);
                hud.setNumberOfMovesText(hud.getNumberOfMoves() - 1);
            }
        }

        public void initializeTurn()
        {
            switchPlayers();
            initializeSanctionsMoves();
        }
        
        
        private void switchPlayers()
        {
            currentPlayerIndex = currentPlayerIndex == 0 ? 1 : 0;
        }

        private void initializeSanctionsMoves()
        {
            int sanctionedMoves = random.Next(1, 6);
            hud.setNumberOfMovesText(sanctionedMoves);
        }
    }
}