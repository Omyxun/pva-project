# Specifikace

## Slovník pojmů

| Pojem | Význam |
|---|---|
| pořadatel | člověk, který turnaj vede a ovládá program |
| hráč | člen klubu, má jméno a přezdívku |
| přezdívka | jedinečné označení hráče, slouží jako jeho ID (nezáleží na velkých a malých písmenech) |
| turnaj | soutěž s názvem, disciplínou a pevným počtem kol |
| disciplína | hra, která se v turnaji hraje (šachy, piškvorky...), program ji jen eviduje |
| přihlášený hráč | hráč, který se turnaje účastní |
| kolo | jedna část turnaje, ve které každý hráč odehraje nejvýš jeden zápas |
| losování | fáze kola, kdy pořadatel ručně určuje dvojice |
| dvojice / zápas | dva hráči, kteří spolu v kole hrají |
| výsledek | výhra prvního hráče, výhra druhého hráče, nebo remíza |
| volný los | hráč, na kterého v kole nezbyl soupeř; dostane body jako za výhru |
| uzavřené kolo | kolo, kde mají všechny zápasy výsledek a už se nedá nic měnit |
| tabulka | přehled hráčů seřazený podle bodů |

## Bodování

| Situace | Body |
|---|---|
| výhra | 2 |
| remíza | 1 |
| prohra | 0 |
| volný los | 2 |

Proč takhle: v šachu se dává 1 bod za výhru a ½ za remízu. My máme stejný poměr, jen vynásobený dvěma, aby nebyla potřeba desetinná čísla. Volný los počítáme jako výhru, protože hráč nemůže za to, že na něj nezbyl soupeř. Volný los se ale nepočítá do odehraných zápasů.

Body jsou v programu na jednom místě (třída Bodovani), takže když se pravidla změní, mění se jen tam.

## Funkce programu

Hráči
- přidat hráče (jméno, přezdívka)
- odebrat hráče
- vypsat hráče

Turnaj
- založit turnaj (název, disciplína, počet kol)
- zrušit turnaj
- přihlásit a odhlásit hráče

Kola a zápasy
- vytvořit nové kolo
- ručně zadat dvojice
- dokončit losování (zbylý hráč dostane volný los)
- zadat nebo přepsat výsledek zápasu
- uzavřít kolo
- vypsat kolo

Ostatní
- tabulka (pořadí, hráč, body, odehrané zápasy)
- uložení a načtení turnaje
- ošetření všech vstupů z klávesnice

## Pravidla

- Přezdívka musí být jedinečná. Jméno ani přezdívka nesmí být prázdné a nesmí obsahovat středník (používáme ho v souboru).
- Hráče, který už má v turnaji zápas nebo volný los, nejde smazat. Stejně tak hráče přihlášeného v rozehraném turnaji.
- Otevřený je vždy jen jeden turnaj. Nový jde založit, až když je starý dokončený nebo zrušený.
- Turnaj musí mít aspoň jedno kolo.
- Přihlašovat a odhlašovat jde jen do začátku prvního kola.
- Nové kolo jde vytvořit, jen když je předchozí uzavřené, nejsou odehraná všechna kola a jsou přihlášení aspoň 2 hráči.
- Do dvojice jdou jen přihlášení hráči. Hráč je v kole nejvýš jednou a nemůže hrát sám se sebou.
- Při dokončení losování dostane volný los hráč, který zbyl. Když zbyl víc než jeden hráč, losování nejde dokončit.
- Výsledky jde zadávat až po dokončení losování. Dokud kolo není uzavřené, jde výsledek přepsat.
- Kolo nejde uzavřít, dokud nemají všechny zápasy výsledek.
- Tabulka počítá všechny zadané výsledky, i v kole, které ještě není uzavřené. Při shodě bodů jsou hráči seřazení podle přezdívky (rozstřel zatím neřešíme).
- Turnaj je dokončený, když jsou odehraná a uzavřená všechna kola.
