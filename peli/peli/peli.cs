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
    GameObject tahtain;

    PhysicsObject pallo = new PhysicsObject (20.0, 20.0);

    List<PlatformCharacter> pommit = new List<PlatformCharacter>();
    
    void LuoKentta()
    {

        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta1");


        ruudut.SetTileMethod(Color.FromHexCode("00FF00"), LuoPelaaja);
        ruudut.SetTileMethod(Color.FromHexCode("FF0015"), LuoVihollinen);
        ruudut.SetTileMethod(Color.Black, LuoTaso);

        ruudut.Execute(20, 20);

        LuoVihollistenAivot();
    }

    void LuoPelaaja(Vector paikka, double leveys, double korkeus)
    {
        
        pelaaja = new PlatformCharacter(leveys/2, 40);
        pelaaja.Position = paikka;
        pelaaja.Weapon = new LaserGun(20, 10);
        pelaaja.Image = LoadImage("vihollinen 1");
        Add(pelaaja);

        AddCollisionHandler(pelaaja, "maa", PelaajaTormaaMaahan);
    }

    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject maa = PhysicsObject.CreateStaticObject(korkeus, leveys);
        maa.Position = paikka;
        maa.CollisionIgnoreGroup = 100;
        maa.Color = Color.Black;
        maa.Tag = "maa";
        Add(maa);
    }

    void PelaajaTormaaMaahan(PhysicsObject maa, PhysicsObject pelaajao)
    {
        if (maa.Y - 55 > pelaajao.Y && maa.Y - 15 < pelaajao.Y)
        {
            pelaaja.Jump(200);
        }

    }


    void LuoVihollinen(Vector paikka, double leveys, double korkeus)
    {

        PlatformCharacter pommi = new PlatformCharacter(leveys / 1, 20);
        pommi.Position = paikka;
        pommi.Image = LoadImage("pommi");
        pommi.Tag = "pommi";
        Add(pommi);

        pommit.Add(pommi);
        
    }

    void LuoVihollistenAivot()
    {
        foreach (PlatformCharacter pommi in pommit)
        {
          FollowerBrain seuraajanAivot = new FollowerBrain(pelaaja);
            pommi.Brain = seuraajanAivot;
            AddCollisionHandler(pommi, pelaaja, PelaajaTormaaPommiin);
        }
    }

    void PelaajaTormaaPommiin(GameObject pommi, PlatformCharacter pelaaja)
    {
        Explosion rajahdys = new Explosion(40);
        rajahdys.Position = pommi.Position;
        Add(rajahdys);
        pelaaja.Destroy();
    }


    void tahtaa(AnalogState hiirenliike)
    {
        Vector suunta = (Mouse.PositionOnWorld - pelaaja.Weapon.AbsolutePosition) .Normalize();
        pelaaja.Weapon.Angle = suunta.Angle;
    }


    void liikuta(Direction suunta)
    {
        if (suunta==Direction.Left)
            pelaaja.Walk(-500);
        if (suunta == Direction.Right)
            pelaaja.Walk(500);
    }
    void hyppaa(double korkeus)
    {
        pelaaja.Jump(korkeus = 100);
    }
    void ammu()
    {
         pelaaja.Weapon.Shoot();
    }


    void LuoTahtain()
    {
        tahtain = new GameObject(20, 20);
        tahtain.Image = LoadImage("tähtäin1");
        Add(tahtain);
    }

    public override  void  Begin()
    {
        LuoKentta();
        LuoTahtain();

        Gravity = new Vector(0, -500);

        Camera.Follow(pelaaja);
        Camera.ZoomFactor = 5;
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.A, ButtonState.Down,
            liikuta, "pelaaja liikkuu", Direction.Left);
        Keyboard.Listen(Key.D, ButtonState.Down,
            liikuta, "pelaaja liikkuu", Direction.Right);
        Keyboard.Listen(Key.W, ButtonState.Down,
            hyppaa, "pelaaja hyppaa", 500.0);
        Mouse.Listen ( MouseButton.Left, ButtonState.Pressed,
            ammu, "ammu aseella" );
        Mouse.ListenMovement(0.1, tahtaa, "tähtää aseella");
        Mouse.ListenMovement(0.0, HiiriLiikkuu, "Hiiri liikkuu");
    }

    void HiiriLiikkuu(AnalogState analogState)
    {
        tahtain.Position = Mouse.PositionOnWorld;
    }
}