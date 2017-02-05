using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using RPG.TileEngine;
using RPG.Globals;
using RPG.Entities;

namespace RPG
{
    static class Program
    {
        static Player Player = new Player("Oliver");
        static UserInterface UserInterface;
        static TileMapEngine MapEngine;
        static RenderWindow GameWindow;
        static ContextSettings Settings;
        static VideoMode VideoMode;
        static Clock Clock = new Clock();
        
        static void Main()
        {
            #region settings
            Color windowColor = new Color(0, 255, 255);
            Settings = new ContextSettings();
            Settings.AntialiasingLevel = 8;
            VideoMode = new VideoMode(1280, 720);
         
            GameWindow = new RenderWindow(VideoMode, "Shieet...", Styles.Default, Settings);
            GameWindow.SetFramerateLimit(30);

            #endregion
            #region eventhandlers
            GameWindow.Closed += new EventHandler(OnClose);
            GameWindow.KeyPressed += app_KeyPressed;
            GameWindow.Resized += app_Resized;
            GameWindow.MouseButtonPressed += app_MouseButtonPressed;
            GameWindow.MouseWheelMoved += app_MouseWheelMoved;
            #endregion

            Player.LoadPlayer();
            UserInterface = new UserInterface(Player);
            MapEngine = new TileMapEngine();
            MapEngine.SetMap(Maps.GetMapByName("Forest"));

            Text fpsText = new Text("", Globals.Objects.GlobalFont, 21) { Position = new Vector2f(0, 0), Color = Color.Red, Style = Text.Styles.Bold };
            float lastTime = 0f;
            int fps = 0;

            while (GameWindow.IsOpen)
            {
                #region Initialization
                GameWindow.DispatchEvents();
                GameWindow.Clear(windowColor);
                float frameTimer = Clock.ElapsedTime.AsSeconds();
                fps = (int)(1.0f / frameTimer - lastTime);
                lastTime = frameTimer;
                Clock.Restart();
                #endregion

                #region Draw Background
                GameWindow.Draw(MapEngine);
                #endregion

                #region Draw Target-Ring
                if (Player.CurrentTarget != null)
                {
                    Objects.TargetRing.Position = new Vector2f(Player.CurrentTarget.GetPosition().X  + 3, Player.CurrentTarget.GetPosition().Y  + 12) ;
                    GameWindow.Draw(Objects.TargetRing);
                }
                #endregion

                #region Draw NPCs
                DrawRange(MapEngine.CurrentMap.NpcCollection.Select(x => x.Sprite ).ToArray());
                #endregion

                GameWindow.SetView(UserInterface.View);

                #region UI Drawing
                #region debug
                fpsText.DisplayedString = string.Format("FPS: {0}\nX: {1}\nY: {2}\nTarget: {3}", fps, Player.GetPosition().X, Player.GetPosition().Y, Player.CurrentTargetName());
                GameWindow.Draw(fpsText);
                #endregion
                #region Draw UI Spell bar
                DrawRange(UserInterface.SpellBar.SpellBarDrawables.ToArray());
                #endregion
                #region ToolTips
                DrawToolTip();
                #endregion
                #region Inventory Drawing
                if (UserInterface.bInventory)
                    GameWindow.Draw(UserInterface.Inventory);
                #endregion
                #endregion

                #region Player Regeneration
                if (!Player.IsDead)
                {
                    if (Player.Mana < Player.MaxMana)
                        Player.Mana += (Player.ManaRegen * frameTimer);
                    if (Player.Health < Player.MaxHealth)
                        Player.Health += (Player.HealthRegen * frameTimer);
                }
                #endregion

                GameWindow.SetView(Player.View);

                #region Draw Combat Bars
                if (Player.InCombat)
                {
                    // Player Bars
                    DrawRange(UserInterface.StatusBarStructure(Player.View.Center, Funcs.GetPercentage(Player.Health, Player.MaxHealth), Funcs.GetPercentage(Player.Mana, Player.MaxMana)));
                    // All Npcs on map bars, add a check if they're in range.
                    foreach (Entities.NPC npc in MapEngine.CurrentMap.NpcCollection)
                    {
                        if (npc.IsDead)
                            continue;

                        var y = GameWindow.MapCoordsToPixel(npc.Sprite.Position, GameWindow.DefaultView);
                        DrawRange(UserInterface.StatusBarStructure(new Vector2f(y.X, y.Y), Funcs.GetPercentage(npc.Health, npc.MaxHealth), Funcs.GetPercentage(75, 100))); //TODO replace with actual mana 
                    }
                }
                #endregion 
                #region Draw Player Sprite
                GameWindow.Draw(Player.Sprite);
                #endregion

                #region Particles
                for (int i = 0; i < ParticleFactory.GetParticles().Length; i++)
                {
                    SpellParticle currentParticle = ParticleFactory.GetParticle(i);
                    currentParticle.Update(frameTimer);
                    GameWindow.Draw(currentParticle);
                }
                #endregion
                
                CheckInput();  
                GameWindow.Display();
            } 
        }

        static void DrawRange(Drawable[] objects)
        {
            foreach (Drawable item in objects)
            {
                GameWindow.Draw(item);
            }
        }
        static void CheckInput()
        {
            foreach (FloatRect collidable in MapEngine.CurrentMap.Collidables)
            {
                if (collidable.Intersects(Player.Sprite.GetGlobalBounds()))
                {
                    Player.PushAway();
                }
            }

            #region player movement

            #region Death check
            if (Player.IsDead)
                return;
            #endregion
            #region NPC collision check
            foreach (Entities.NPC npc in MapEngine.CurrentMap.NpcCollection)
            {
                if (npc.IsDead)
                    continue;

                if(Funcs.CollidesBoundry((Enumerations.Direction)Player.Direction, Player.Sprite.GetGlobalBounds(), npc.Sprite.GetGlobalBounds()))
                {
                    Player.PushAway();
                }
            }
            #endregion

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                Player.FaceDirection(Enumerations.Direction.Up);
                if (Player.Sprite.Position.Y <= 0)
                    return;

                Player.Move(Enumerations.Direction.Up);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                Player.FaceDirection(Enumerations.Direction.Down);

                if (Player.Sprite.Position.Y >= ((MapEngine.CurrentMap.Height * 32) - 32))
                    return;

                Player.Move(Enumerations.Direction.Down);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Player.FaceDirection(Enumerations.Direction.Left);

                if (Player.Sprite.Position.X <= 0)
                    return;

                Player.Move(Enumerations.Direction.Left);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Player.FaceDirection(Enumerations.Direction.Right);

                if (Player.Sprite.Position.X >= (MapEngine.CurrentMap.Width * 32) - 32)
                      return;

                Player.Move(Enumerations.Direction.Right);
            }
            #endregion
        }

        static void DrawToolTip()
        {
            var coord = GameWindow.MapPixelToCoords(Mouse.GetPosition(GameWindow));
            int spellbarHoveredId = Funcs.OnSpellBarButton(coord, UserInterface.SpellBar);

            if (spellbarHoveredId != -1)
            {
                Spell spellRef = UserInterface.SpellBar.GetSpellAtIndex(spellbarHoveredId );
                spellRef.ToolTip.SetPosition(new Vector2f(coord.X, coord.Y - 150));
                DrawRange(spellRef.ToolTip.DrawableLayers.ToArray());
            }

        }

        #region eventhandlers
        static void app_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector2f coords = GameWindow.MapPixelToCoords(Mouse.GetPosition(GameWindow));
                List<Interfaces.ITargetable> targetableObjects = MapEngine.CurrentMap.NpcCollection.Cast<Interfaces.ITargetable>().ToList();
                targetableObjects.Add(Player);

                Player.CurrentTarget = Funcs.SetTarget(coords, targetableObjects);
            }
        }
        static void app_Resized(object sender, SizeEventArgs e)
        {

            GameWindow.Close();
            GameWindow = new RenderWindow(new VideoMode(e.Width, e.Height), "Shieet...", Styles.Default, Settings);
            GameWindow.SetFramerateLimit(30);

            #region eventhandlers
            GameWindow.Closed += new EventHandler(OnClose);
            GameWindow.KeyPressed += app_KeyPressed;
            GameWindow.Resized += app_Resized;
            GameWindow.MouseButtonPressed += app_MouseButtonPressed;
            GameWindow.MouseWheelMoved += app_MouseWheelMoved;
            #endregion
        }
        static void app_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            { 
                case Keyboard.Key.Num1:
                    Spell spellAtIndex = UserInterface.SpellBar.GetSpellAtIndex(0);
                    Console.WriteLine("Casting" + spellAtIndex.Name);
                    Player.Cast(spellAtIndex);
                    break;
                case Keyboard.Key.Num2:
                    spellAtIndex = UserInterface.SpellBar.GetSpellAtIndex(1);
                    Console.WriteLine("Casting" + spellAtIndex.Name);
                    Player.Cast(spellAtIndex);
                    break;
                case Keyboard.Key.Num3:
                    spellAtIndex = UserInterface.SpellBar.GetSpellAtIndex(2);
                    Console.WriteLine("Casting" + spellAtIndex.Name);
                    Player.Cast(spellAtIndex);
                    break;
                case Keyboard.Key.Num4:
                    break;
                case Keyboard.Key.Num5:
                    break;
                case Keyboard.Key.Escape:
                    GameWindow.Close();
                    break;
            }

            if (e.Code == Keyboard.Key.Escape)
            {
                Console.WriteLine("Menu Toggled");
            }
            if (e.Code == Keyboard.Key.B)
            {
                UserInterface.bInventory = !UserInterface.bInventory;
            }
            if (e.Code == Keyboard.Key.Tilde)
            {
                Player.Health -= 20;
            }
        }
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
        static void app_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            double currentZoom = Player.View.Size.X;

            double maxZoom = (700 * 1.1),
                   minZoom = (700 * 0.6);

            if (e.Delta == -1) // Out
            {
                if (currentZoom >= maxZoom)
                    return;
                Player.View.Zoom(1.2f);
            }
            else if (e.Delta == 1) // In
            {
                if (currentZoom <= minZoom)
                    return;
                Player.View.Zoom(0.8f);
            }

        }
        #endregion
    } 
}