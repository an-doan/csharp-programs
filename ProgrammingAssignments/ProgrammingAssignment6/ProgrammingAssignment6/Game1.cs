//modified by An Doan

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaCards;

namespace ProgrammingAssignment6
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        // max valid blockjuck score for a hand
        const int MaxHandValue = 21;

        // deck and hands
        Deck deck;
        List<Card> dealerHand = new List<Card>();
        List<Card> playerHand = new List<Card>();

        // hand placement
        const int TopCardOffset = 100;
        const int HorizontalCardOffset = 150;
        const int VerticalCardSpacing = 125;

        // messages
        SpriteFont messageFont;
        const string ScoreMessagePrefix = "Score: ";
        Message playerScoreMessage;
        Message dealerScoreMessage;
        Message winnerMessage;
		List<Message> messages = new List<Message>();

        // message placement
        const int ScoreMessageTopOffset = 25;
        const int HorizontalMessageOffset = HorizontalCardOffset;
        Vector2 winnerMessageLocation = new Vector2(WindowWidth / 2,
            WindowHeight / 2);

        // menu buttons
        Texture2D quitButtonSprite;
        List<MenuButton> menuButtons = new List<MenuButton>();

        // menu button placement
        const int TopMenuButtonOffset = TopCardOffset;
        const int QuitMenuButtonOffset = WindowHeight - TopCardOffset;
        const int HorizontalMenuButtonOffset = WindowWidth / 2;
        const int VerticalMenuButtonSpacing = 125;

        // use to detect hand over when player and dealer didn't hit
        bool playerHit = false;
        bool dealerHit = false;

        // game state tracking
        static GameState currentState = GameState.WaitingForPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set resolution and show mouse
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.PreferredBackBufferWidth = WindowWidth;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create and shuffle deck
            deck = new Deck(Content, 0, 0);
            deck.Shuffle();

            // first player card, flip over to face up
            //created a DealCard method to set the x and y of the card, and to flip over if needed
            Card firstPlayerCard = DealCard(HorizontalCardOffset, TopCardOffset, true);
            
            //add to player hand
            playerHand.Add(firstPlayerCard);

            // first dealer card, this card is faced down
            Card firstDealerCard = DealCard(WindowWidth - HorizontalCardOffset, TopCardOffset, false);

            //add to dealer hand
            dealerHand.Add(firstDealerCard);

            // second player card, facing up
            Card secondPlayerCard = DealCard(HorizontalCardOffset, TopCardOffset + VerticalCardSpacing, true);
            
            //add to player hand
            playerHand.Add(secondPlayerCard);

            // second dealer card, facing up
            Card secondDealerCard = DealCard(WindowWidth - HorizontalCardOffset, TopCardOffset + VerticalCardSpacing, true);
            
            //add to dealer hand
            dealerHand.Add(secondDealerCard);

            // load sprite font, create message for player score and add to list
            messageFont = Content.Load<SpriteFont>(@"fonts\Arial24");
            playerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString(),
                messageFont,
                new Vector2(HorizontalMessageOffset, ScoreMessageTopOffset));
            messages.Add(playerScoreMessage);

            // load quit, hit, and stand button sprites for later use
			quitButtonSprite = Content.Load<Texture2D>(@"graphics\quitbutton");

            // create hit button and add to list
            MenuButton hitButton = new MenuButton(Content.Load<Texture2D>(@"graphics\hitbutton"),
                new Vector2(HorizontalMenuButtonOffset, TopMenuButtonOffset), GameState.PlayerHitting);

            menuButtons.Add(hitButton);

            // create stand button and add to list            
            MenuButton standButton = new MenuButton(Content.Load<Texture2D>(@"graphics\standbutton"),
                new Vector2(HorizontalMenuButtonOffset, TopMenuButtonOffset + VerticalMenuButtonSpacing), GameState.WaitingForDealer);

            menuButtons.Add(standButton);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //get mouse state
            MouseState mouse = Mouse.GetState();

            // update menu buttons as appropriate
            if(currentState == GameState.WaitingForPlayer || currentState == GameState.DisplayingHandResults)
            {
                foreach (MenuButton menuButton in menuButtons)
                {
                    menuButton.Update(mouse);
                }
            }

            
            // game state-specific processing
            switch (currentState)
            {
                case GameState.PlayerHitting:
                    //add a card to the hand
                    playerHand.Add(DealCard(HorizontalCardOffset, TopCardOffset + VerticalCardSpacing * playerHand.Count, true));

                    //update score for player and bool
                    playerScoreMessage.Text = ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString();
                    playerHit = true;

                    //change current state to waiting for dealer
                    ChangeState(GameState.WaitingForDealer);
                    return;
                case GameState.WaitingForDealer:
                    //based on score, the dealer hits (score is 16 or less) or stands (score is 17 or more)
                    if (GetBlockjuckScore(dealerHand) <= 16)
                    {
                        //dealer hits
                        ChangeState(GameState.DealerHitting);
                    }
                    if (GetBlockjuckScore(dealerHand) >= 17)
                    {
                        //dealer stands and game state is checking hand over
                        ChangeState(GameState.CheckingHandOver);
                    }
                    return;
                case GameState.DealerHitting:
                    //add card to hand for dealer
                    dealerHand.Add(DealCard(WindowWidth - HorizontalCardOffset, TopCardOffset + VerticalCardSpacing * dealerHand.Count, true));

                    //update bool
                    dealerHit = true;

                    //change state to checking hand over
                    ChangeState(GameState.CheckingHandOver);
                    return;
                case GameState.CheckingHandOver:
                    //create values for whether or not player/dealer busted
                    bool hasPlayerBusted = GetBlockjuckScore(playerHand) > MaxHandValue;
                    bool hasDealerBusted = GetBlockjuckScore(dealerHand) > MaxHandValue;

                    //create values for player/dealer scores
                    int playerScore = GetBlockjuckScore(playerHand);
                    int dealerScore = GetBlockjuckScore(dealerHand);

                    //if player or dealer goes over max score or both have stood
                    if (hasPlayerBusted || hasDealerBusted || (!playerHit && !dealerHit))
                    {
                        //create string for winner message text
                        string winnerMessageText;

                        //if dealer hasn't busted or has a higher score, dealer wins
                        if ((!hasDealerBusted && hasPlayerBusted) || (dealerScore > playerScore && !hasDealerBusted))
                        {
                            winnerMessageText = "Dealer Won!";
                        }
                        else
                        {
                            //if player hasn't busted or has a higher score, player wins 
                            if ((!hasPlayerBusted && hasDealerBusted) || (playerScore > dealerScore && !hasPlayerBusted))
                            {
                                winnerMessageText = "You Won!";
                            }
                            else
                            {
                                // both players busted or have the same score, it's a tie
                                winnerMessageText = "It's a Tie!";
                            }
                        }
                        

                        //add new message for stating winner
                        messages.Add(new Message(winnerMessageText, messageFont, winnerMessageLocation));

                        //flip dealers first card and display score for dealer
                        dealerHand[0].FlipOver();

                        dealerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(dealerHand).ToString(), messageFont,
                            new Vector2(WindowWidth - HorizontalMessageOffset, ScoreMessageTopOffset));
                        messages.Add(dealerScoreMessage);

                        //remove hit and stand buttons 
                        for (int i = menuButtons.Count - 1; i >= 0; i--)
                        {
                                menuButtons.RemoveAt(i);
                        }

                        //add quit button
                        menuButtons.Add(new MenuButton(quitButtonSprite, new Vector2(HorizontalMenuButtonOffset, QuitMenuButtonOffset),
                            GameState.Exiting));

                        //transition to display hand results
                        ChangeState(GameState.DisplayingHandResults);
                    }
                    else
                    {
                        //reset booleans for hitting
                        playerHit = false;
                        dealerHit = false;

                        //player can play again if no one busted
                        ChangeState(GameState.WaitingForPlayer);
                    }
                    return;
                case GameState.Exiting:
                    //exit game
                    Exit();
                    return;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);
						
            spriteBatch.Begin();

            // draw hands
            foreach (Card playerCard in playerHand)
            {
                playerCard.Draw(spriteBatch);
            }

            foreach (Card dealerCard in dealerHand)
            {
                dealerCard.Draw(spriteBatch);
            }

            // draw messages
            foreach (Message message in messages)
            {
                message.Draw(spriteBatch);
            }

            // draw menu buttons
            foreach (MenuButton menuButton in menuButtons)
            {
                menuButton.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Calculates the Blockjuck score for the given hand
        /// </summary>
        /// <param name="hand">the hand</param>
        /// <returns>the Blockjuck score for the hand</returns>
        private int GetBlockjuckScore(List<Card> hand)
        {
            // add up score excluding Aces
            int numAces = 0;
            int score = 0;
            foreach (Card card in hand)
            {
                if (card.Rank != Rank.Ace)
                {
                    score += GetBlockjuckCardValue(card);
                }
                else
                {
                    numAces++;
                }
            }

            // if more than one ace, only one should ever be counted as 11
            if (numAces > 1)
            {
                // make all but the first ace count as 1
                score += numAces - 1;
                numAces = 1;
            }

            // if there's an Ace, score it the best way possible
            if (numAces > 0)
            {
                if (score + 11 <= MaxHandValue)
                {
                    // counting Ace as 11 doesn't bust
                    score += 11;
                }
                else
                {
                    // count Ace as 1
                    score++;
                }
            }

            return score;
        }

        /// <summary>
        /// deals a card for the player or dealer
        /// </summary>
        /// <param name="x">the x location for the card</param>
        /// <param name="y">the y location for the card</param>
        /// <param name="faceUp">whether or not the card should be facing up</param>
        /// <returns>a card from the deck wit the specified parameters</returns>
        private Card DealCard(int x, int y, bool faceUp)
        {
            //take top card of deck
            Card card = deck.TakeTopCard();

            //if faceUp is true, flip card over
            if(faceUp)
            {
                card.FlipOver();
            }

            //setting x and y of card
            card.X = x;
            card.Y = y;

            return card;
        }

        /// <summary>
        /// Gets the Blockjuck value for the given card
        /// </summary>
        /// <param name="card">the card</param>
        /// <returns>the Blockjuck value for the card</returns>
        private int GetBlockjuckCardValue(Card card)
        {
            switch (card.Rank)
            {
                case Rank.Ace:
                    return 11;
                case Rank.King:
                case Rank.Queen:
                case Rank.Jack:
                case Rank.Ten:
                    return 10;
                case Rank.Nine:
                    return 9;
                case Rank.Eight:
                    return 8;
                case Rank.Seven:
                    return 7;
                case Rank.Six:
                    return 6;
                case Rank.Five:
                    return 5;
                case Rank.Four:
                    return 4;
                case Rank.Three:
                    return 3;
                case Rank.Two:
                    return 2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            currentState = newState;
        }
    }
}
