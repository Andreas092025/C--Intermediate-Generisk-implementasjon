using DispatchGame.Core;
using DispatchGame.Models;

namespace generic_implementation_tet;

/* 
Her teseter jeg koden min der jeg har brukt Container<T> og IStorable<T>.
Sjekker jeg at jeg kan finne heltene, fjerne dem, sortere dem basert på PowerLevel (Synkende)
og JSON håndtering (lagring og henting).
I dette eksemplet bruker jeg "TestHero1", "TestHero2", "Role1" og "Role2" for testdata. 

Jeg bruker AAA-mønsteret (Arrange, Act, Assert) for å strukturere testene mine.
*/
public class UnitTest1
{

        [Fact]
        public void TestAddHero() // Tester tillegg, søk og sortering av helter
        {
        // Arrange
        var container = new Container<Hero>();
        var hero1 = new Hero("TestHero1", "Role1", 50);
        var hero2 = new Hero("TestHero2", "Role2", 70);
        container.Add(hero1);
        container.Add(hero2);
        // Act
        var foundHero = container.FindByName("TestHero2");
        container.SortByPowerLevel();
        // Assert
        Assert.NotNull(foundHero);
        Assert.Equal("TestHero2", foundHero?.Name);
        Assert.Equal(2, container.Count);
        Assert.Equal(hero2, container.Get(0)); // skal være den med høyest PowerLevel etter sortering
        Assert.Equal(hero1, container.Get(1)); // skal være den med lavest PowerLevel etter sortering
        }

        [Fact]
        public void TestRemoveHero() // Tester fjerning av helt
        {   
        // Arrange
        IStorable<Hero> container = new Container<Hero>();
        var hero1 = new Hero("TestHero1", "Role1", 50);
        container.Add(hero1);
        // Act
        var foundBefore = container.FindByName("TestHero1");
        container.Remove(hero1);
        var foundAfter = container.FindByName("TestHero1");
        // Assert
        Assert.NotNull(foundBefore);
        Assert.Null(foundAfter);
        Assert.Equal(0, container.Count); 
        }

        [Fact]
        public void TestJSONSaving() // Tester lagring og henting av helter i JSON-format
        {
        // Arrange
        IStorable<Hero> container = new Container<Hero>();
        var hero1 = new Hero("TestHero1", "Role1", 50);
        var hero2 = new Hero("TestHero2", "Role2", 70);
        container.Add(hero1);
        container.Add(hero2);
        var filePath = "test_heroes.json";
        // Act
        container.SaveToJson(filePath);
        var newContainer = new Container<Hero>();
        newContainer.LoadFromJson(filePath);
        // Assert
        Assert.Equal(2, newContainer.Count);
        var loadedHero1 = newContainer.FindByName("TestHero1");
        var loadedHero2 = newContainer.FindByName("TestHero2");
        Assert.NotNull(loadedHero1);
        Assert.NotNull(loadedHero2);
        Assert.Equal(50, loadedHero1?.PowerLevel);
        Assert.Equal(70, loadedHero2?.PowerLevel);
        // Cleanup
        if (File.Exists(filePath))
            {
            File.Delete(filePath);
            }
        }

        [Fact]
        public void TestSortSynkende() // Tester sortering av helter basert på PowerLevel (Synkende)
        {
        // Arrange
        IStorable<Hero> container = new Container<Hero>();
        container.Add(new Hero("Weakling", "Test", 10));
        container.Add(new Hero("Medium", "Test", 50));
        container.Add(new Hero("Strong", "Test", 100));
        // Act
        container.SortByPowerLevel();
        // Assert
        Assert.Equal("Strong", container.Get(0).Name);
        Assert.Equal("Medium", container.Get(1).Name);
        Assert.Equal("Weakling", container.Get(2).Name);
        }

        [Fact]
        public void TestFindNonExistentHero() // Tester søk etter en ikke-eksisterende helt
        {
        // Arrange
        IStorable<Hero> container = new Container<Hero>();
        container.Add(new Hero("ExistingHero", "Test", 30));
        // Act
        var foundHero = container.FindByName("NonExistentHero");
        // Assert
        Assert.Null(foundHero);
        }

        [Fact]
        public void TestFindExistentHero() // Tester søk etter en eksisterende helt
        {
        // Arrange
        IStorable<Hero> container = new Container<Hero>();
        var hero = new Hero("ExistingHero", "Test", 30);
        container.Add(hero);
        // Act
        var foundHero = container.FindByName("ExistingHero");
        // Assert
        Assert.NotNull(foundHero);
        Assert.Equal(hero, foundHero);
        }

        [Fact]
        public void Save_Then_TryLoad_ReturnsSavedHero() 
        // Litt mer dedikert testing av lagring og henting av en enkel liste med helter i JSON-format
        {
        // Arrange
        var container = new Container<Hero>();
        var hero = new Hero("TestHero", "TestRole", 80);
        container.Add(hero);
        var filePath = "Test_hero_test.json";
        // Act
        container.SaveToJson(filePath);
        var newContainer = new Container<Hero>();
        newContainer.LoadFromJson(filePath);
        var loadedHero = newContainer.FindByName("Hero");
        // Assert
        Assert.NotNull(loadedHero);
        Assert.Equal(hero.Name, loadedHero?.Name);
        Assert.Equal(hero.Role, loadedHero?.Role);
        Assert.Equal(hero.PowerLevel, loadedHero?.PowerLevel);
        // Cleanup
        if (File.Exists(filePath))
            {
            File.Delete(filePath);
            }
        }


}
