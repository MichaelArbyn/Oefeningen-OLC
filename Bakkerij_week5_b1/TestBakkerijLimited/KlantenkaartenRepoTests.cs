using NUnit.Framework;

using BakkerijLimited.Domain;
using BakkerijLimited.Models;

namespace TestBakkerijLimited;

public class KlantenkaartenRepoTests
{
    private KlantenkaartenRepo _klantenKaartenRepo;

    [SetUp]
    public void Setup()
    {
        // Arrange
        _klantenKaartenRepo = new KlantenkaartenRepo();
    }

    [Test]
    // REQ0006 – Een nieuwe klantenkaart aanmaken en toevoegen aan de repo
    // Klant mag niet null zijn
    public void AddKlantenkaart_NullKlant_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _klantenKaartenRepo.AddKlantenkaart(null!));
    }

    [Test]
    // REQ0006 – Een nieuwe klantenkaart aanmaken en toevoegen aan de repo
    public void AddKlantenkaart_ValidKlant_ReturnsKlantenkaartWithAssignedNumber()
    {
        // Arrange
        Klant klant = new Klant("Lucie");

        // Act
        Klantenkaart result = _klantenKaartenRepo.AddKlantenkaart(klant);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Klant, Is.SameAs(klant));
        Assert.Greater(result.KlantenkaartNummer, 0);
    }

    [Test]
    // REQ0006 – Een nieuwe klantenkaart aanmaken en toevoegen aan de repo
    // Klantenkaartnummers moeten uniek zijn
    public void AddKlantenkaart_TwoDifferentKlanten_ReturnsDifferentNumbers()
    {
        // Arrange
        Klant klant1 = new Klant("Victor");
        Klant klant2 = new Klant("Imran");

        // Act
        Klantenkaart kaart1 = _klantenKaartenRepo.AddKlantenkaart(klant1);
        Klantenkaart kaart2 = _klantenKaartenRepo.AddKlantenkaart(klant2);

        // Assert
        Assert.IsNotNull(kaart1);
        Assert.IsNotNull(kaart2);
        Assert.That(kaart1.Klant, Is.SameAs(klant1));
        Assert.That(kaart2.Klant, Is.SameAs(klant2));
        Assert.That(kaart2.KlantenkaartNummer, Is.Not.EqualTo(kaart1.KlantenkaartNummer), 
            "Klantenkaartnummers moeten uniek zijn");
    }

    [Test]
    // REQ0004 – ZoekOpNummer moet de juiste klantenkaart teruggeven als die bestaat
    public void ZoekOpNummer_ExistingNumber_ReturnsKlantenkaart()
    {
        // Arrange
        Klant klant = new Klant("Sofia");
        Klantenkaart gemaakteKaart = _klantenKaartenRepo.AddKlantenkaart(klant);

        // Act
        Klantenkaart? gevonden = _klantenKaartenRepo.ZoekOpNummer(gemaakteKaart.KlantenkaartNummer);

        // Assert
        Assert.IsNotNull(gevonden);
        Assert.That(gevonden, Is.SameAs(gemaakteKaart));
        Assert.That(gevonden!.KlantenkaartNummer, Is.EqualTo(gemaakteKaart.KlantenkaartNummer));
    }

    [Test]
    // REQ0004 – ZoekOpNummer moet null teruggeven wanneer geen kaart met dat nummer bestaat
    public void ZoekOpNummer_NonExistingNumber_ReturnsNull()
    {
        // Arrange
        Klant klant = new Klant("Youssef"); 
        Klantenkaart gemaakteKaart = _klantenKaartenRepo.AddKlantenkaart(klant);

        // Act
        // nummer dat niet bestaat
        Klantenkaart? gevonden = _klantenKaartenRepo.ZoekOpNummer(gemaakteKaart.KlantenkaartNummer + 1); 

        // Assert
        Assert.IsNull(gevonden);
    }

    [Test]
    // REQ0004 – Arrange met minstens 3 klantenkaarten; zoek een kaart die niet de eerste of laatste is
    public void ZoekOpNummer_MiddleCardAmongThree_ReturnsCorrectKlantenkaart()
    {
        // Arrange
        Klant klant1 = new Klant("Sofia");
        Klant klant2 = new Klant("Mohamed");
        Klant klant3 = new Klant("Fatima");

        Klantenkaart kaart1 = _klantenKaartenRepo.AddKlantenkaart(klant1);
        Klantenkaart kaart2 = _klantenKaartenRepo.AddKlantenkaart(klant2);
        Klantenkaart kaart3 = _klantenKaartenRepo.AddKlantenkaart(klant3);

        // Act: zoek de middelste kaart (kaart2)
        Klantenkaart? gevonden = _klantenKaartenRepo.ZoekOpNummer(kaart2.KlantenkaartNummer);

        // Assert
        Assert.IsNotNull(gevonden);
        Assert.That(gevonden, Is.SameAs(kaart2));
        // Verify it's neither the first nor the last
        Assert.That(gevonden!.KlantenkaartNummer, Is.Not.EqualTo(kaart1.KlantenkaartNummer));
        Assert.That(gevonden.KlantenkaartNummer, Is.Not.EqualTo(kaart3.KlantenkaartNummer));
    }
}
