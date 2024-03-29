using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using FGECore;
using FGECore.CoreSystems;
using FGECore.MathHelpers;
using FGEGraphics.ClientSystem;
using FGEGraphics.ClientSystem.EntitySystem;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace TestGame.MainGame
{
    /*
     * Hey! Read the information in GameProgram.cs
     * 
     * 
     * 
     * First steps to take:
     * 
     * - Create some form of camera controller - perhaps a first person view, or a free cam.
     * 
     * - Load in any assets you may have available.
     * 
     * - Get the world you want here to start appearing, then add ways to control it!
    */

    /// <summary>
    /// The game starting point.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The game client window.
        /// </summary>
        public GameClientWindow Window;

        /// <summary>
        /// The primary game engine.
        /// </summary>
        public GameEngine3D Engine;

        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            // Create a window, 3D mode by default.
            Window = new GameClientWindow();
            // Add an event for when the window is set up but not loaded yet.
            Window.OnWindowSetUp += WindowSetup;
            string gameDataPath = "C:\\Users\\ruben\\source\\repos\\TestGame\\TestGame\\MainGame\\data";
            string modPath = "C:\\Users\\ruben\\source\\repos\\TestGame\\TestGame\\MainGame\\mods";
            string savePath = "C:\\Users\\ruben\\source\\repos\\TestGame\\TestGame\\MainGame\\saves";
            // Set up the location for the game data (TODO: Make this not use my own file path...)
            Window.Files.Init(gameDataPath, modPath, savePath);
            // Add an event to listen for when the window loads.
            Window.OnWindowLoad += WindowLoad;
            // Start the client systems.
            Window.Start();
        }

        /// <summary>
        /// Fires when the window is set up but not loaded.
        /// </summary>
        public void WindowSetup()
        {
            // Grab the engine reference.
            Engine = Window.Engine3D;
            // Set whether shadows should be cast from things in fast/foward mode.
            Engine.Forward_Shadows = true;
            // Set whether to use the fast/forward mode, or the slow/deferred mode of rendering.
            Engine.MainView.Config.ForwardMode = true;
            // Configure any other options core here...
        }

        /// <summary>
        /// Fires when the window loads.
        /// </summary>
        public void WindowLoad()
        {
            // Track the escape key press for quick-closing.
            Window.Window.KeyDown += Window_KeyDown;
            // Spawn a big box.
            Engine.SpawnEntity(new EntitySimple3DRenderableModelProperty()
            {
                // Make it a box (cube) model
                EntityModel = Engine.Models.Cube,
                // Set its scale (how big it is)
                Scale = new Location(20, 20, 2),
                // Give it a light red color.
                Color = new Color4F(1f, 0.5f, 0.5f, 1f),
                // Give it a plain white texture
                DiffuseTexture = Engine.Textures.White
            })
            // Set its position (where it is in the world)
            .SetPosition(new Location(-5, 0, -10));
            // Spawn a small box.
            Engine.SpawnEntity(new EntitySimple3DRenderableModelProperty()
            {
                // Make it a box (cube) model
                EntityModel = Engine.Models.Cube,
                // Set its scale (how big it is)
                Scale = new Location(2, 2, 2),
                // Set its color to white
                Color = Color4F.White,
                // Give it a plain white texture
                DiffuseTexture = Engine.Textures.White
            })
            // Set its position (where it is in the world)
            .SetPosition(new Location(-5, 0, -5));
            // Spawn a light
            Engine.SpawnEntity(new EntityPointLight3DProperty()
            {
                // Set it directly above the box
                LightPosition = new Location(-5, 0, 5),
                // Set its brightness distance
                LightStrength = 25f,
                // Set its color to light cyan
                LightColor = new Color3F(0.5f, 1.0f, 1.0f)
            });
            // Set a camera position
            Engine.MainCamera.Position = new Location(3, 3, 3);
            // Set the camera to face the box. Don't forget to keep camera direction normalized!
            Engine.MainCamera.Direction = new Location(-1, 0, -1).Normalize();
            // Spawn or configure additional objects here...
        }

        /// <summary>
        /// Handles escape key pressing to exit.
        /// </summary>
        /// <param name="e">Event data.</param>
        private void Window_KeyDown(KeyboardKeyEventArgs e)
        {
            // If the key pressed is Escape
            if (e.Key == Keys.Escape)
            {
                // Close the main engine window! This will cause the entire game to shut down!
                Window.Window.Close();
            }
        }
    }
}
