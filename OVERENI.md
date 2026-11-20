# Ověření tabulky

Ověření k ticketu #16, user story US10. Připravený turnaj je v souboru `testy/overeni_turnaj.txt`. Má 5 hráčů, 3 kola, dvě remízy a tři volné losy.

## Bodování

Podle SPECIFIKACE.md dostane hráč za výhru 2 body, za remízu 1 bod, za prohru 0 bodů a za volný los 2 body. Volný los se nezapočítává do odehraných zápasů. Při shodě bodů rozhoduje přezdívka podle abecedy.

## Průběh turnaje

| Kolo | Zápas 1 | Zápas 2 | Volný los |
| --- | --- | --- | --- |
| 1 | anna – boris, vyhrála anna | cyril – david, remíza | eva |
| 2 | anna – cyril, vyhrál cyril | boris – eva, remíza | david |
| 3 | anna – eva, vyhrála eva | boris – david, vyhrál boris | cyril |

Každý hráč je v každém kole právě jednou, buď v zápasu, nebo s volným losem. V souboru jsou všechna kola uzavřená, všechny zápasy mají výsledek a hráč s volným losem se určí podle chybějící přezdívky v dvojicích daného kola.

## Ruční výpočet

| Hráč | 1. kolo | 2. kolo | 3. kolo | Součet bodů | Odehrané zápasy |
| --- | --- | --- | --- | --- | --- |
| anna | výhra, 2 | prohra, 0 | prohra, 0 | 2 + 0 + 0 = 2 | 1 + 1 + 1 = 3 |
| boris | prohra, 0 | remíza, 1 | výhra, 2 | 0 + 1 + 2 = 3 | 1 + 1 + 1 = 3 |
| cyril | remíza, 1 | výhra, 2 | volný los, 2 | 1 + 2 + 2 = 5 | 1 + 1 + 0 = 2 |
| david | remíza, 1 | volný los, 2 | prohra, 0 | 1 + 2 + 0 = 3 | 1 + 0 + 1 = 2 |
| eva | volný los, 2 | remíza, 1 | výhra, 2 | 2 + 1 + 2 = 5 | 0 + 1 + 1 = 2 |

Celkem se odehrálo 6 zápasů, takže součet sloupce Odehráno má být 12. V každém kole se rozdělí 6 bodů včetně volného losu, celkem tedy 18 bodů. Ruční výpočet obě kontroly splňuje.

Očekávané pořadí je cyril, eva, boris, david, anna. Cyril a Eva mají po 5 bodech, Boris a David po 3 bodech. V obou případech odpovídá pořadí přezdívkám.

## Spuštění ověření

Z kořene repozitáře spusťte následující příkaz s .NET 10 SDK.

~~~sh
dotnet run --project testy/OvereniTabulky/OvereniTabulky.csproj
~~~

Ověřovací program sestaví připravený turnaj z testovacího souboru přes veřejné metody tříd. Zavolá přímo `Tabulka.Spocitej` a porovná všechny řádky s ručním výpočtem včetně pořadí. Při neshodě nebo chybě skončí s návratovým kódem 1. Soubor se při sestavení kopíruje k ověřovacímu programu, takže se při každém spuštění používají aktuální testovací data.

Tento test ověřuje výpočet tabulky. Soubor čte samostatný ověřovací program. Kontrola načítání přes `Uloziste.Nacti` patří do #14.

## Zaznamenaný běh

Ověření proběhlo 20. 11. 2026 na větvi založené na `dev` (commit `84c632c`) s .NET SDK 10.0.404.

Skutečný výstup ověřovacího programu byl následující.

~~~text
Pořadí | Přezdívka | Body | Odehráno
1 | cyril | 5 | 2
2 | eva | 5 | 2
3 | boris | 3 | 3
4 | david | 3 | 2
5 | anna | 2 | 3
Ověření prošlo. Body, počty zápasů i pořadí odpovídají ručnímu výpočtu.
~~~

Výstup se shoduje s ručním výpočtem pro všech 5 hráčů.
