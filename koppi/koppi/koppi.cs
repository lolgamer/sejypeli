using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class koppi : PhysicsGame
{
    IntMeter pisteLaskuri();
    IntMeter elamat = new IntMeter(3, 0, 5);
    int level = 1;
    int omenoitaIlmassa = 1;

    public override void Begin()
    {
        luopistelaskuri();
        LuoElamalaskuri();

        UusiOmena(level);
        omenoitaIlmassa = level;


        //luo reunat
        Level.CreateLeftBorder();
        Level.CreateRightBorder();
        PhysicsObject pohja =
            Level.CreateBottomBorder();

        //elämän menetys
        AddCollisionHandler(pohja, putosimaahan);



        Gravity = new Vector(0.0, -100.0);



        IsMouseVisible = true;
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }



    void omenaaklikattu(PhysicsObject klikattuomena)
    {
        klikattuomena.Destroy();
        //jotain...
    } 

    IntMeter pistelaskuri;

    void luopistelaskuri()
    {
        pistelaskuri = new IntMeter(0);

        Label pistenaytto = new Label();
        pistenaytto.BindTo(pistelaskuri);
        pistenaytto.X = Screen.Left + 100;
        pistenaytto.Y = Screen.Top - 100;
        pistenaytto.TextColor = Color.Black;
        pistenaytto.TextColor = Color.White;

        pistenaytto.BindTo(pistelaskuri);
        Add(pistenaytto);

        pistenaytto.IntFormatString = "pisteitä: {0:D1}";
        pistelaskuri.Value += 1;
       
    }
    void putosimaahan(PhysicsObject maa,
        PhysicsObject omena)
    {


        if (omena.Color != Color.Black)
        {
            elamalaskuri.Value -= 1;
            omena.Color = Color.Black;
            omenoitaIlmassa = omenoitaIlmassa - 1;
        }
    }

     IntMeter elamalaskuri;
     void LuoElamalaskuri()
     {
         elamalaskuri = new IntMeter(3, 0, 5);
         Label elamaNaytto = new Label();
         elamaNaytto.BindTo(elamalaskuri);
         elamaNaytto.X = Screen.Right - 50.0;
         elamaNaytto.Y = Screen.Top - 50.0;
     }

     void UusiOmena()
     {
         PhysicsObject omena = new PhysicsObject(50, 50);
         omena.Shape = Shape.Circle;
         omena.Color = Color.Red;
         omena.Y = 100;
         GameObject lehti = new GameObject(10, 10);
         lehti.Shape = Shape.Heart;
         lehti.Color = Color.Green;
         Add(omena);
         lehti.Y = 30;
         omena.Add(lehti);
         Mouse.ListenOn(omena, MouseButton.Left,
             ButtonState.Pressed, omenaaklikattu,
             "omenaa on klikattu", omena);

         omena.Hit(RandomGen.NextVector(0.0, -100.0));
     }
}
