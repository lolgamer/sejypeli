using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class pong : PhysicsGame
{
    Vector nopeusYlos = new Vector(0, 200);
    Vector nopeusAlas = new Vector(0, -200);


    PhysicsObject pallo;

    PhysicsObject maila1;
    PhysicsObject maila2;


    public override void Begin()
    {
        LuoKentta();
        asetaOhjaimet();
        AloitaPeli();
    }
    void asetaOhjaimet ()
    {
        Keyboard.Listen(Key.A, ButtonState.Down, asetaNopeus, "pelaaja 1: liikuta mailaa ylös", maila1, nopeusYlos);
        Keyboard.Listen(Key.A, ButtonState.Released, asetaNopeus, null, maila1, Vector.Zero);
        Keyboard.Listen(Key.D, ButtonState.Down, asetaNopeus, "pelaaja 1: liikuta mailaa alas", maila1, nopeusAlas);
        Keyboard.Listen(Key.D, ButtonState.Released, asetaNopeus, null, maila1, Vector.Zero);


        Keyboard.Listen(Key.Up, ButtonState.Down, asetaNopeus, "pelaaja 2: liikuta mailaa ylös", maila2, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, asetaNopeus, null, maila2, Vector.Zero);
        Keyboard.Listen(Key.Down, ButtonState.Down, asetaNopeus, "pelaaja 2: liikuta mailaa alas", maila2, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, asetaNopeus, null, maila2, Vector.Zero);

        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

    }
    void asetaNopeus(PhysicsObject maila, Vector nopeus)
    {
        maila.Velocity = nopeus;
    }

    void LuoKentta()
    {
        pallo = new PhysicsObject(40.0, 40.0);
        pallo.Shape = Shape.Circle;
        pallo.X = -200.0;
        pallo.Y = 0.0;
        pallo.Restitution = 1.0;
        Add(pallo);

        maila1 =  luomaila(Level.Left + 20.0, 0.0);
        maila2 = luomaila(Level.Right - 20.0, 0.0);

        Level.CreateBorders(1.0, false);
        Level.BackgroundColor = Color.Green;

        Camera.ZoomToLevel();
   
      
   }

    PhysicsObject luomaila(double x, double y)

    {
        PhysicsObject maila = PhysicsObject.CreateStaticObject(20.0, 100.0);
        maila.Shape = Shape.Rectangle;
        maila.X = x;
        maila.Y = y;
        maila.Restitution = 1.0;
        Add(maila);
        return maila;

    }

    void AloitaPeli()
    {
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi);
    }

}
