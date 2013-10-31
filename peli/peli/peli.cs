using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class peli : PhysicsGame
{
    PlatformCharacter pelaaja;

    void LuoKentta()
    {
        //1. Luetaan kuva uuteen ColorTileMappiin, kuvan nimen perässä ei .png-päätettä.
        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("testikennta1");

        //2. Kerrotaan mitä aliohjelmaa kutsutaan, kun tietyn värinen pikseli tulee vastaan kuvatiedostossa.
        ruudut.SetTileMethod(Color.FromHexCode("00FF00"), LuoPelaaja);
        ruudut.SetTileMethod(Color.Black, LuoTaso);
        // ruudut.SetTileMethod(Color.Yellow, LuoTahti);
       // ruudut.Optimize(Color.Black);
        //3. Execute luo kentän
        //   Parametreina leveys ja korkeus
        ruudut.Execute(20, 20);
    }

    void LuoPelaaja(Vector paikka, double leveys, double korkeus)
    {
        
        pelaaja = new PlatformCharacter(leveys/2, korkeus);
        pelaaja.Position = paikka;
        pelaaja.Weapon = new AssaultRifle(20, 10);
        Add(pelaaja);

    }

    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject maa = PhysicsObject.CreateStaticObject(korkeus, leveys);
        maa.Position = paikka;
        maa.CollisionIgnoreGroup = 1;
        Add(maa);

    }
    void liikuta(Direction suunta)
    {
        if (suunta==Direction.Left)
            pelaaja.Walk(-200);
        if (suunta == Direction.Right)
            pelaaja.Walk(200);
    }
    void hyppaa(double korkeus)
    {
        pelaaja.Jump(korkeus);
    }
    void ammu()
    {
        pelaaja.Weapon.Shoot();
    }

    

    public override  void  Begin()
    {
        LuoKentta();
        Gravity = new Vector(0, -100);

        Camera.Follow(pelaaja);
        Camera.ZoomFactor = 2;
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down,
            liikuta, "pelaaja liikkuu", Direction.Left);
        Keyboard.Listen(Key.Right, ButtonState.Down,
            liikuta, "pelaaja liikkuu", Direction.Right);
        Keyboard.Listen(Key.Up, ButtonState.Down,
            hyppaa, "pelaaja hyppaa", 400.0);
        Keyboard.Listen(Key.Space, ButtonState.Pressed,
            ammu, "ammu aseella");
    }
}