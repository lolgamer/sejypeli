using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class FysiikkaPeli1 : PhysicsGame
{
    public override void Begin()
    {
        //PhysicsObject hahmo = new PhysicsObject(40, 20);
        //hahmo.Shape = Shape.Rectangle;
        //hahmo.Mass = 10.0;
        //Add(hahmo);
        luokentta()

        // TODO: Kirjoita ohjelmakoodisi tähän

        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    void luokentta()
    {

        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("taso 1");
        //2. Kerrotaan mitä aliohjelmaa kutsutaan, kun tietyn värinen pikseli tulee vastaan kuvatiedostossa.
        ruudut.SetTileMethod(Color.Green, LuoPelaaja);
        ruudut.SetTileMethod(Color.Black, LuoTaso);
        ruudut.SetTileMethod(Color.Yellow, LuoTahti);

        //3. Execute luo kentän
        //   Parametreina leveys ja korkeus
        ruudut.Execute(20, 20);
    }

    void LuoPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja = new PlatformCharacter(10, 10);
        pelaaja.Position = paikka;
        AddCollisionHandler(pelaaja, "tahti", TormaaTahteen);
        Add(pelaaja);
    }

    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Image = groundImage;
        taso.CollisionIgnoreGroup = 1;
        Add(taso);
    }

    void LuoTahti(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject tahti = new PhysicsObject(5, 5);
        tahti.IgnoresCollisionResponse = true;
        tahti.Position = paikka;
        tahti.Image = tahdenKuva;
        tahti.Tag = "tahti";
        Add(tahti, 1);
    }





}
