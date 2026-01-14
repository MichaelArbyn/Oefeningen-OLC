# HogerLAger
## Situering
Het spel hoger-lager is een eenvoudig gokspel waarbij een speler een random gekozen geheim getal moet raden. Het spel geeft hints nadat een speler probeert te raden: het geheime getal is hoger of lager dan de poging. Dit project implementeert een consoleversie van het spel in C#.


## Opdracht
Het programma is reeds geïmplementeerd, uitgezonderd de `HogerLager.RaadEens(int)` functie. Enkel die functie moet aangepast worden. Deze moet hoger teruggeven indien het geheim getal groter is dan het geraden getal, lager indien het geheim getal kleiner is dan het geraden getal, en correct indien het geraden getal gelijk is aan het geheim getal.

Voor je de methode RaadEens implementeert, moet je eerst de unit tests hiervoor ontwerpen. Omdat het geheime getal random is kan je niet voorspellen wat de uitkomst van RaadEens zal zijn, dat kunnen we enkel weten als het geheime getal gekend is voor de test. Je moet dus het startgetal vast bepalen in je testen en niet de random functie gebruiken.

Er zijn 2 versies van de testen. `HogerLagerTests` gebruikt een eigengeschreven double.
Nadat we NSubstitute hebben geleerd maak je de double met behulp van Nsubstitute in `HogerLagerTests_NSub`.  
Implementeer de RaadEens functie zodat alle testen slagen.
