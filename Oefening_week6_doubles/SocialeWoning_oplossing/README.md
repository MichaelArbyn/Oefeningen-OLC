# Sociale woning toewijzen
## Situatie
Een sociale woning toewijzen aan een kandidaat-huurder kan pas nadat geverifiëerd is dat de kandidaat aan de voorwaarden voldoet. Omdat we voorwaarden complex zijn en weinig gemeen hebben met het toewijzen van een woning hebben we controleren van de voorwaarden in een aparte klasse ondergebracht.

## Requirements
req01 : Een geldige kandidaat moet voldoen aan de volgende voorwaarden:
  - Leeftijd minstens 18 jaar
  - Inkomen lager dan 30000 euro
  - Geen eigendom van een eigen woning, tenzij deze woning onbewoonbaar is verklaard.
  - Geen woning in vruchtgebruik hebben gekregen  

req02 : Een woning kan toegewezen worden aan een kandidaat indien die aan alle voorwaarden voldoet en een woning beschikbaar is.  

Leeftijd en inkomen zijn altijd gekend. Huisvestingsfeiten die niet gekend zijn kunnen geen aanleiding geven tot het weigeren van een kandidaat. Bijvoorbeeld: indien niet gekend is of een kandidaat al een eigen woning heeft kan dit geen reden zijn om de kandidaat te weigeren.


## Opdracht
Gebruik TDD (uiteraard) om de requirements te implementeren. Omdat unit tests voor req02 ook test of een kandidaat in aanmerking komt en dat dus een herhaling van req01 is, zal een test double gebruikt worden.

